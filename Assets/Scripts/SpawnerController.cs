using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class EnemyWaveSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public Transform spawnPointsRoot;
    public Transform enemiesParent;

    private List<Transform> spawnPoints = new();

    public float spawnTimer = 30f;
    public int spawnPerWave = 5;
    public int waveCount = 0;

    public void Start()
    {
        foreach (Transform child in spawnPointsRoot)
        {
            spawnPoints.Add(child);
        }

        StartCoroutine(SpawnWaves());
    }

    public IEnumerator SpawnWaves()
    {
        while (true)
        {
            SpawnWave();
            yield return new WaitForSeconds(spawnTimer);
        }
    }

    public void SpawnWave()
    {
        List<Transform> chosenSpawns = spawnPoints.OrderBy(x => Random.value).Take(spawnPerWave).ToList();

        foreach (Transform spawnPoint in chosenSpawns)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            enemy.transform.parent = enemiesParent;
        }

        waveCount++;
    }
}