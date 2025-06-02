using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;

    public int enemisPerWave = 10;
    public int totalWaves = 5;
    public int currentWave = 0;

    private List<RagdollActivator> currentWaveEnemies = new List<RagdollActivator>();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private void Update()
    {
        SpawnEnemies();
    }
    private IEnumerator SpawnEnemies()
    {
        while (currentWave < totalWaves)
        {
            currentWaveEnemies.Clear();
            for (int i = 0; i < enemisPerWave; i++)
            {
                GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
                RagdollActivator controller = enemy.GetComponentInChildren<RagdollActivator>();

                if (controller == null)
                {
                    Debug.LogError("Enemy missing RagdollActivator!");
                }
                else
                {
                    currentWaveEnemies.Add(controller);
                    Debug.Log($"Spawned enemy {i + 1}");
                }
            }
            currentWave++;

            yield return new WaitUntil(() => AllEnemiesRagdolled());

            yield return new WaitForSeconds(2f);

        }
    }
    private bool AllEnemiesRagdolled()
    {
        foreach(var enemy in  currentWaveEnemies)
        {
            if (enemy != null && !enemy.isDead)
                return false;
        }
        return true;
    }
}
