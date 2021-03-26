using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehaviour : MonoBehaviour
{
    private float delayAndSpawnRate = 2.0f;
    private float timeUntilSpawnRateIncrease = 30.0f;

    [SerializeField] private List<GameObject> m_EnemyPrefabs;

    private void SpawnRandomEnemyAtPosition(Vector3 spawnPoint)
    {
        if (m_EnemyPrefabs.Count > 0)
        {
            GameObject enemy = Instantiate(m_EnemyPrefabs[0], spawnPoint, Quaternion.identity);
        }
    }

    void Start()
    {
        StartCoroutine(SpawnObject(delayAndSpawnRate));
    }

    IEnumerator SpawnObject(float firstDelay)
    {
        float spawnRateCountdown = timeUntilSpawnRateIncrease;
        float spawnCountdown = firstDelay;
        while (true)
        {
            yield return null;
            spawnRateCountdown -= Time.deltaTime;
            spawnCountdown -= Time.deltaTime;

            if (spawnCountdown < 0)
            {
                spawnCountdown += delayAndSpawnRate;
                SpawnRandomEnemyAtPosition(transform.localPosition);
            }

            // Should the spawn rate increase?
            if (spawnRateCountdown < 0 && delayAndSpawnRate > 1)
            {
                spawnRateCountdown += timeUntilSpawnRateIncrease;
                delayAndSpawnRate -= 0.1f;
            }
        }
    }
}