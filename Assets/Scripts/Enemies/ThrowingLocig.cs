using UnityEngine;

public class ThrowingLogic : MonoBehaviour
{
    [Header("Throw Settings")]
    public GameObject throwablePrefab;       // Assign your prefab here
    public Transform throwSpawnPoint;        // Empty child transform where the prefab spawns
    public float throwForce = 10f;

    public void DetachAndThrow()
    {
        Debug.Log("DetachAndThrow called on ThrowingLogic!");

        if (throwablePrefab == null || throwSpawnPoint == null)
        {
            Debug.LogWarning("ThrowablePrefab or ThrowSpawnPoint not assigned!");
            return;
        }

        GameObject projectile = Instantiate(throwablePrefab, throwSpawnPoint.position, throwSpawnPoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(throwSpawnPoint.forward * throwForce, ForceMode.VelocityChange);
        }
        else
        {
            Debug.LogWarning("Throwable prefab missing Rigidbody!");
        }
    }
}
