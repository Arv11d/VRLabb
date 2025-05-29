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

    private bool isRagdolled = false;
    private float originalSpeed;

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
        }

        // Always rotate towards player
        Vector3 direction = player.position - transform.position;
        direction.y = 0f;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

        if (distanceToPlayer <= attackRange)
        {
            agent.speed = 0f;
            agent.SetDestination(transform.position); 
            animator.SetFloat("Speed", 0f);

            if (!isAttacking)
            {
                animator.SetTrigger("Attack");  
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
