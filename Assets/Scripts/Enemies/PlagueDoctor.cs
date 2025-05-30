using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float throwRange = 10f;
    public GameObject projectilePrefab;
    public Transform throwPoint;
    public float throwForce = 10f;

    private Animator animator;
    private NavMeshAgent agent;
    private bool isThrowing = false;
    private bool isRagdoll = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (isRagdoll) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= throwRange)
        {
            agent.isStopped = true;

            if (!isThrowing)
            {
                transform.LookAt(player); // Face the player
                isThrowing = true;
                animator.SetTrigger("Throw");
            }
        }
        else
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
    }

    // Called via animation event
    public void ThrowObject()
    {
        GameObject obj = Instantiate(projectilePrefab, throwPoint.position, throwPoint.rotation);
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        rb.linearVelocity = throwPoint.forward * throwForce;

        isThrowing = false;
    }

    // Trigger ragdoll
    private void OnTriggerEnter(Collider other)
    {
        if (isRagdoll) return;

        if (other.CompareTag("Weapon"))
        {
            EnableRagdoll();
        }
    }

    void EnableRagdoll()
    {
        isRagdoll = true;
        animator.enabled = false;
        agent.enabled = false;

        foreach (var rb in GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = false;
        }

        foreach (var col in GetComponentsInChildren<Collider>())
        {
            col.enabled = true;
        }

        GetComponent<Collider>().enabled = false; // disable main collider
    }
}
