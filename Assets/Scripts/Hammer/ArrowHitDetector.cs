using UnityEngine;

public class ArrowHitDetector : MonoBehaviour
{
    [Header("Force Settings")]
    public float forceMultiplier = 10f;
    public float minImpactVelocity = 0.2f;
    
    [Header("Damage Settings")]
    public float baseDamage = 50f;
    public float velocityDamageMultiplier = 20f;
    public float maxDamage = 150f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Hitbox") && !other.CompareTag("Enemy"))
            return;

        RagdollActivator ragdoll = other.GetComponentInParent<RagdollActivator>();
        if (ragdoll == null || ragdoll.IsDead())
            return;

        Rigidbody hitBody = other.attachedRigidbody;
        Rigidbody incomingRigidbody = GetComponentInParent<Rigidbody>();

        if (incomingRigidbody == null)
            return;

        Vector3 velocity = incomingRigidbody.linearVelocity;
        float currentVelocity = velocity.magnitude;

        if (currentVelocity < minImpactVelocity)
            return;

        float velocityDamage = currentVelocity * velocityDamageMultiplier;
        float totalDamage = Mathf.Clamp(baseDamage + velocityDamage, baseDamage, maxDamage);

        Debug.Log($"ARROW DAMAGE: {totalDamage:F2} | Health before: {ragdoll.GetCurrentHealth():F2}");
        
        ragdoll.TakeDamage(totalDamage);

        Debug.Log($"Health after: {ragdoll.GetCurrentHealth():F2}");

        if (ragdoll.IsDead() && hitBody != null)
        {
            Vector3 forceDirection = hitBody.transform.position - transform.position;
            forceDirection.Normalize();

            float impactForce = currentVelocity * forceMultiplier;
            hitBody.AddForce(forceDirection * impactForce, ForceMode.Impulse);
        }
    }
}