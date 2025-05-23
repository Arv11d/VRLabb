using UnityEngine;
using UnityEngine.AI;

public class FollowPlayerAgent : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;
    public Animator animator;

    private bool isRagdolled = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = Camera.main.transform;
    }

    void Update()
    {
        if (isRagdolled) return;

        if (player != null && agent != null && agent.isOnNavMesh)
        {
            Vector3 target = player.position;
            target.y = transform.position.y;

            agent.SetDestination(target);

            float speed = agent.velocity.magnitude;
            animator.SetFloat("Speed", speed);
        }
    }

    public void DisableMovement()
    {
        isRagdolled = true;

        if (agent != null && agent.isActiveAndEnabled)
        {
            agent.ResetPath(); // Stop movement
            agent.enabled = false; // Disable completely
        }

        if (animator != null)
            animator.enabled = false;
    }

}
