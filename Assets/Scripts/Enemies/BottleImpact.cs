using UnityEngine;

public class BottleImpact : MonoBehaviour
{
    public GameObject splashEffectPrefab;
    public float destroyDelay = 0.1f;

    void OnCollisionEnter(Collision collision)
    {
        // Debug to check it's firing at all
        Debug.Log("Bottle hit something!");

        if (splashEffectPrefab != null)
        {
            ContactPoint contact = collision.contacts[0];
            Quaternion rot = Quaternion.LookRotation(contact.normal);
            Instantiate(splashEffectPrefab, contact.point, rot);
        }

        Destroy(gameObject, destroyDelay);
    }

}
