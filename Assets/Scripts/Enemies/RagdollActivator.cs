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

    public MonoBehaviour[] scriptsToDisable;

    public GameManager gameManager;
    public int pointsToAdd = 1;

    void Awake()
    {
        allRigidbodies = GetComponentsInChildren<Rigidbody>();
        allColliders = GetComponentsInChildren<Collider>();
        agent = GetComponent<NavMeshAgent>();

        SetRagdoll(false);
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

        if (scriptsToDisable != null)
        {
            foreach (var script in scriptsToDisable)
                script.enabled = !state;
        }

        if (state && !AlreadyHit)
        {
            AlreadyHit = true;

            if (hitSound != null)
                AudioSource.PlayClipAtPoint(hitSound, transform.position);

            if (gameManager != null)
                gameManager.AddPoints(pointsToAdd);
            else
                Debug.LogWarning("GameManager not assigned in RagdollActivator!");
        }
    }
}
