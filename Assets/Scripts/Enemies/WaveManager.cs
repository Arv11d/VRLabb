using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public int enemyLimit = 10;

    private int enemyCounter = 0;
    private GameObject currentEnemy;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    private IEnumerator SpawnEnemies()
    {
        while (enemyCounter < enemyLimit)
        {
            
            currentEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            enemyCounter++;

            RagdollActivator controller = currentEnemy.GetComponent<RagdollActivator>();

            while (controller != null && !controller.isDead)
            {
                yield return null;
            }
            yield return new WaitForSeconds(1);
        }
    }
}
