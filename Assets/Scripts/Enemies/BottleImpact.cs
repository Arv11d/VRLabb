using UnityEngine;

public class BottleImpact : MonoBehaviour
{
    public GameObject splashEffectPrefab;
    public float destroyDelay = 0.1f;

    void OnTriggerEnter(Collider collider)
    {
        string otherTag = collider.gameObject.tag;

        // Only destroy if it's NOT plagueDoctor AND NOT another Bottle
        if (otherTag != "PlagueDoctor" && otherTag != "Bottle")
        {
            Debug.Log($"Bottle hit {otherTag}!");

            if (splashEffectPrefab != null)
            {
                Vector3 effectPosition = transform.position;
                Quaternion rot = Quaternion.identity;

                Instantiate(splashEffectPrefab, effectPosition, rot);
            }

            Destroy(gameObject, destroyDelay);
        }
    }
}
