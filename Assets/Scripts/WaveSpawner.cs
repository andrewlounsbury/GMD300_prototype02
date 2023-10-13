using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Transform[] SpawnPoints;
    public Transform Player;
    public GameObject EnemyPrefab;
    public float TimeBetweenWaves = 10f;
    public int TotalWaves = 5;
    public int EnemiesPerWave = 10;

    private float countdown = 0;
    private int waveNumber = 0;
    private List<GameObject> spawnedEnemies = new();


    void Update()
    {
        int enemiesKilled = 0;

        foreach (GameObject enemy in spawnedEnemies)
        {
            if(enemy == null)
            {
                enemiesKilled++;
            }
        }

        if (countdown <= 0f && waveNumber < TotalWaves)
        {
            StartCoroutine(SpawnWave());
            countdown = TimeBetweenWaves;
        }

        if (enemiesKilled == spawnedEnemies.Count)
        {
            
            countdown -= Time.deltaTime;
        }
    }

    IEnumerator SpawnWave()
    {
        waveNumber++;
        spawnedEnemies.Clear();

        for (int i = 0; i < EnemiesPerWave; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1f);
        }
    }

    void SpawnEnemy()
    {
        int spawnPointIndex = Random.Range(0, SpawnPoints.Length);
        Transform spawnPoint = SpawnPoints[spawnPointIndex];
        GameObject spawnedEnemy = Instantiate(EnemyPrefab, spawnPoint.position, spawnPoint.rotation);
        spawnedEnemy.GetComponent<MoveToDestination>().Destination = Player;
        spawnedEnemies.Add(spawnedEnemy);
    }
}