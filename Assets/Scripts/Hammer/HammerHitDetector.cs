using UnityEngine;

public class HammerHitDetector: MonoBehaviour
{
    public float hitForce = 200f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Hitbox"))
            return;
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
                forceDirection.Normalize();

                hitBody.AddForce(forceDirection * hitForce, ForceMode.Impulse);
            }
        }
    }
}
