using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;

    Vector3 SpawnPos;

    public int enemiesPerWave = 10;
    public int totalWaves = 5;
    public int currentWave = 0;

    private List<RagdollActivator> currentWaveEnemies = new();
    
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
            for (int i = 0; i < enemiesPerWave; i++)
            {
                Vector3 offset = new Vector3(Random.Range(0, 10), 0, Random.Range(0, 10));
                Vector3 spawnPos = spawnPoint.position + offset;

                GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
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
        currentWave++;
        return true;
    }
}
