using UnityEngine;

public class RagdollTriggerChild : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
            Debug.Log("Ragdoll should start");
    }
}
