using UnityEngine;

public class HammerHitDetector : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object we hit has a RagdollActivator
        RagdollActivator ragdoll = collision.collider.GetComponentInParent<RagdollActivator>();

        if (ragdoll != null)
        {
            ragdoll.SetRagdoll(true);
        }
    }
}
