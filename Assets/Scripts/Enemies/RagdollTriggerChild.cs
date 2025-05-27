using UnityEngine;

public class RagdollTriggerChild : MonoBehaviour
{
    public RagdollActivator ragdollActivator;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Hammer") || !other.CompareTag("Weapon")) return; // Or use Player or whatever hits

        if (ragdollActivator != null)
        {
            ragdollActivator.SetRagdoll(true);
        }
    }
}
