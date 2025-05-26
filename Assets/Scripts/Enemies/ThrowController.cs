using UnityEngine;

public class ThrowController : MonoBehaviour
{
    public GameObject bottlePrefab;       // Prefab to spawn
    public Transform handTransform;       // Where to spawn the new bottle
    public Transform throwOrigin;         // Direction to throw
    public float throwForce = 10f;
    public float respawnDelay = 1.5f;

    private GameObject currentBottle;

    void Start()
    {
        SpawnBottle();
    }

    public void DetachAndThrow()
    {
        if (currentBottle == null) return;

        currentBottle.transform.parent = null;

        // Enable physics and throw
        Rigidbody rb = currentBottle.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.AddForce(throwOrigin.forward * throwForce, ForceMode.Impulse);
        }

        Destroy(currentBottle, 5f);
        Invoke(nameof(SpawnBottle), respawnDelay);
        currentBottle = null;
    }


    void SpawnBottle()
    {
        currentBottle = Instantiate(bottlePrefab, handTransform.position, handTransform.rotation, handTransform);

        // Make sure it doesn't fall
        Rigidbody rb = currentBottle.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }
}
