using UnityEngine;

public class LeverEnemySpawner : MonoBehaviour
{
   
    

    public GameObject[] enemiesToActivate;
    bool hasSpawned = false;

    public void ActivateEnemies()
    {
        if (hasSpawned) return;

        foreach (GameObject enemy in enemiesToActivate)
        {
            enemy.SetActive(true);
        }

        hasSpawned = true;
    }
}
