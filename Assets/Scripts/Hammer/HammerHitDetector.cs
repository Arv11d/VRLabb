using UnityEngine;

public class HammerHitDetector : MonoBehaviour
{
    [Header("Force Settings")]
    public float hitForce = 200f;
    
    [Header("Damage Settings")]
    public float baseDamage = 25f;
    public float minVelocityForDamage = 2f;
    public float velocityDamageMultiplier = 10f;
    public float maxDamage = 100f;
    
    private VelocityTracker velocityTracker;

    void Start()
    {
        velocityTracker = GetComponent<VelocityTracker>();
        if (velocityTracker == null)
            velocityTracker = GetComponentInParent<VelocityTracker>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Hitbox") && !other.CompareTag("Enemy"))
            return;

        RagdollActivator ragdoll = other.GetComponentInParent<RagdollActivator>();
        if (ragdoll == null || ragdoll.IsDead())
            return;

        float currentVelocity = 0f;
        if (velocityTracker != null)
            currentVelocity = velocityTracker.CurrentVelocity.magnitude;

        if (currentVelocity < minVelocityForDamage)
            return;

        float velocityDamage = currentVelocity * velocityDamageMultiplier;
        float totalDamage = Mathf.Clamp(baseDamage + velocityDamage, baseDamage, maxDamage);

        Debug.Log($"DAMAGE: {totalDamage:F2} | Health before: {ragdoll.GetCurrentHealth():F2}");
        
        ragdoll.TakeDamage(totalDamage);

        Debug.Log($"Health after: {ragdoll.GetCurrentHealth():F2}");

        if (ragdoll.IsDead())
        {
            Rigidbody hitBody = other.attachedRigidbody;
            if (hitBody != null)
            {
                Vector3 forceDirection = hitBody.transform.position - transform.position;
                forceDirection.Normalize();

                float scaledForce = hitForce * Mathf.Clamp(currentVelocity / 5f, 0.5f, 2f);
                hitBody.AddForce(forceDirection * scaledForce, ForceMode.Impulse);
            }
        }
    }
}