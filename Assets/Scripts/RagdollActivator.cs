using UnityEngine;

public class RagdollActivator : MonoBehaviour
{
    public Animator animator;
    private Rigidbody[] allRigidbodies;
    private Collider[] allColliders;

    void Awake()
    {
        allRigidbodies = GetComponentsInChildren<Rigidbody>();
        allColliders = GetComponentsInChildren<Collider>();

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
    }
}
