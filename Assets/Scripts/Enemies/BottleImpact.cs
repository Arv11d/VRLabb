using UnityEngine;

public class BottleImpact : MonoBehaviour
{
    public GameObject splashEffectPrefab;
    public float destroyDelay = 0.1f;
    public AudioSource impactAudioSource; // Assign this in the inspector

    void OnCollisionEnter(Collision collision)
    {
        string otherTag = collision.gameObject.tag;

        // Only destroy if it's NOT plagueDoctor AND NOT another Bottle
        if (otherTag != "PlagueDoctor" && otherTag != "Bottle")
        {
            Debug.Log($"Bottle hit {otherTag}!");

            // Play impact sound
            if (impactAudioSource != null && !impactAudioSource.isPlaying)
            {
                impactAudioSource.Play();
            }

            // Create splash effect at contact point
            if (splashEffectPrefab != null && collision.contactCount > 0)
            {
                ContactPoint contact = collision.GetContact(0);
                Vector3 hitPosition = contact.point;
                Quaternion hitRotation = Quaternion.LookRotation(contact.normal);

                Instantiate(splashEffectPrefab, hitPosition, hitRotation);
            }

            Destroy(gameObject, destroyDelay);
        }
    }
}
