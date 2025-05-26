using UnityEngine;
using UnityEngine.AI;

public class FollowPlayerAgent : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;
    public Animator animator;

    private bool isRagdolled = false;
    private bool playerInTrigger = false;
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
        if (isRagdolled || agent == null || !agent.isOnNavMesh || player == null) return;

        // Check if we're in attack animation
        bool isAttacking = false;
        if (animator != null)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            isAttacking = stateInfo.IsName("MeleeAttack_OneHanden");
        }

        // Always update destination so the agent can rotate toward player
        Vector3 target = player.position;
        target.y = transform.position.y;
        agent.SetDestination(target);

        if (isAttacking || playerInTrigger)
        {
            // Stop movement, but allow rotation
            agent.speed = 0f;
            animator.SetFloat("Speed", 0f);
        }
        else
        {
            if (agent.speed == 0f)
                agent.speed = originalSpeed;

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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
        }
    }
}
