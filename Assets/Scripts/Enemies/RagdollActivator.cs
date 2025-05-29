using UnityEngine;
using UnityEngine.AI;

public class RagdollActivator : MonoBehaviour
{
    public Animator animator;
    private Rigidbody[] allRigidbodies;
    private Collider[] allColliders;
    public AudioClip hitSound;
    private NavMeshAgent agent;
    private bool AlreadyHit = false;

    // Add this to disable custom AI scripts
    public MonoBehaviour[] scriptsToDisable;

    void Awake()
    {
        allRigidbodies = GetComponentsInChildren<Rigidbody>();
        allColliders = GetComponentsInChildren<Collider>();
        agent = GetComponent<NavMeshAgent>();

        SetRagdoll(false); // Start with ragdoll off
    }

    public void SetRagdoll(bool state)
    {
        animator.enabled = !state;

        foreach (var rb in allRigidbodies)
            rb.isKinematic = !state;

        foreach (var col in allColliders)
        {
            if (col.gameObject != this.gameObject)
                col.enabled = state;
        }

        if (agent != null)
            agent.enabled = !state;

        // Disable any movement/AI scripts (e.g., FollowPlayer)
        if (scriptsToDisable != null)
        {
            foreach (var script in scriptsToDisable)
                script.enabled = !state;
        }

        if (state && hitSound != null && !AlreadyHit)
        {
            AudioSource.PlayClipAtPoint(hitSound, transform.position);
            AlreadyHit = true;
        }
    }
}
