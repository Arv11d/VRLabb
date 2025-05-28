using UnityEngine;

public class ArrowHitDetector : MonoBehaviour
{
    public float forceMultiplier = 10f; // Tune this to get the right knockback amount
    public float minImpactVelocity = 0.2f; // Minimum velocity to register a valid hit

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Hitbox"))
            return;

        // Try to get the RagdollActivator
        RagdollActivator ragdoll = other.GetComponentInParent<RagdollActivator>();
        if (ragdoll == null)
            return;

        ragdoll.SetRagdoll(true);

        Rigidbody hitBody = other.attachedRigidbody;
        Rigidbody incomingRigidbody = GetComponentInParent<Rigidbody>(); // The object doing the hitting

        if (hitBody != null && incomingRigidbody != null)
        {
            Vector3 velocity = incomingRigidbody.linearVelocity;

            if (velocity.magnitude > minImpactVelocity)
            {
                Vector3 forceDirection = hitBody.transform.position - transform.position;
                forceDirection.Normalize();

                float impactForce = velocity.magnitude * forceMultiplier;
                hitBody.AddForce(forceDirection * impactForce, ForceMode.Impulse);
            }
        }
    }
}
