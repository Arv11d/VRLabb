using UnityEngine;
using UnityEngine.AI;

public class FollowPlayerAgent : MonoBehaviour
{
    [Header("References")]
    public Animator animator;
    private NavMeshAgent agent;
    private Transform player;

    [Header("Attack Settings")]
    public string attackStateName = "MeleeAttack_OneHanden";
    public float attackRange = 2f;

    [Header("Retreat Settings")]
    public float retreatDistance = 4f;
    public float retreatDuration = 1.5f;
    public float retreatSpeed = 3f;
    public float attackToRetreatDelay = 0.5f;

    private bool isRagdolled = false;
    private float originalSpeed;

    private float lastAttackTime = 0f;
    public float attackCooldown = 1.5f;

    private bool isRetreating = false;
    private float retreatStartTime = 0f;
    private Vector3 retreatDirection;
    
    private bool waitingToRetreat = false;
    private float retreatDelayStartTime = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = Camera.main.transform;

        if (agent != null)
            originalSpeed = agent.speed;
    }

    void Update()
    {
        if (isRagdolled || agent == null || !agent.isOnNavMesh || player == null)
            return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Determine if in attack animation
        bool isAttacking = false;
        if (animator != null)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            isAttacking = stateInfo.IsName(attackStateName);
            
            // Check if attack animation just finished
            if (!isAttacking && Time.time - lastAttackTime < 0.1f && Time.time - lastAttackTime > 0f)
            {
                StartRetreatDelay();
            }
        }

        // Handle retreat delay
        if (waitingToRetreat)
        {
            if (Time.time > retreatDelayStartTime + attackToRetreatDelay)
            {
                waitingToRetreat = false;
                StartRetreat();
            }
            else
            {
                // Stay still during delay
                agent.speed = 0f;
                agent.SetDestination(transform.position);
                animator.SetFloat("Speed", 0f);
                return;
            }
        }

        // Always rotate towards player (except when retreating or waiting to retreat)
        if (!isRetreating && !waitingToRetreat)
        {
            Vector3 direction = player.position - transform.position;
            direction.y = 0f;
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            }
        }

        // Handle retreat behavior
        if (isRetreating)
        {
            HandleRetreat();
            return;
        }

        if (distanceToPlayer <= attackRange)
        {
            agent.speed = 0f;
            agent.SetDestination(transform.position);
            animator.SetFloat("Speed", 0f);

            if (!isAttacking && Time.time > lastAttackTime + attackCooldown)
            {
                animator.SetTrigger("Attack");
                lastAttackTime = Time.time;
            }
        }
        else
        {
            agent.speed = originalSpeed;
            agent.SetDestination(player.position);

            float speed = agent.velocity.magnitude;
            animator.SetFloat("Speed", speed);
        }
    }

    private void StartRetreatDelay()
    {
        waitingToRetreat = true;
        retreatDelayStartTime = Time.time;
    }

    private void StartRetreat()
    {
        isRetreating = true;
        retreatStartTime = Time.time;
        
        // Calculate retreat direction (opposite to player)
        retreatDirection = (transform.position - player.position).normalized;
        retreatDirection.y = 0f;
        
        // Set retreat destination
        Vector3 retreatTarget = transform.position + retreatDirection * retreatDistance;
        
        // Check if retreat target is valid on NavMesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(retreatTarget, out hit, retreatDistance, NavMesh.AllAreas))
        {
            agent.speed = retreatSpeed;
            agent.SetDestination(hit.position);
        }
        else
        {
            // If retreat position is invalid, just move backward a bit
            Vector3 fallbackTarget = transform.position + retreatDirection * 1f;
            if (NavMesh.SamplePosition(fallbackTarget, out hit, 2f, NavMesh.AllAreas))
            {
                agent.speed = retreatSpeed;
                agent.SetDestination(hit.position);
            }
        }
    }

    private void HandleRetreat()
    {
        // Check if retreat duration has elapsed
        if (Time.time > retreatStartTime + retreatDuration)
        {
            isRetreating = false;
            return;
        }

        // Rotate to face away from player while retreating
        if (retreatDirection != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(retreatDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 3f);
        }

        // Update animator with movement speed
        float speed = agent.velocity.magnitude;
        animator.SetFloat("Speed", speed);

        // Check if agent has reached retreat destination or is stuck
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            isRetreating = false;
        }
    }

    public void DisableMovement()
    {
        isRagdolled = true;

        if (agent != null && agent.isActiveAndEnabled)
        {
            agent.ResetPath();
            agent.enabled = false;
        }

        if (animator != null)
            animator.enabled = false;
    }
}