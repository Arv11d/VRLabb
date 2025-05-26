using UnityEngine;

public class HammerTrigger : MonoBehaviour
{
    public float hitForce = 0f;

    private void OnTriggerEnter(Collider other)
    {
        // Try to get the RagdollActivator
        RagdollActivator ragdoll = other.GetComponentInParent<RagdollActivator>();
        if (ragdoll != null)
        {
            ragdoll.SetRagdoll(true);

            // Apply force to the hit Rigidbody (if it exists)
            Rigidbody hitBody = other.attachedRigidbody;
            if (hitBody != null)
            {
                Vector3 forceDirection = hitBody.transform.position - transform.position;
                forceDirection += Vector3.up * 0.5f;
                forceDirection.Normalize();
                hitBody.AddForce(forceDirection * hitForce, ForceMode.Impulse);
            }
        }
    }
}
