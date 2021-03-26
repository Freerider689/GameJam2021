using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehaviour : MonoBehaviour
{
    public float delayAndSpawnRate = 2.0f;
    public float timeUntilSpawnRateIncrease = 30.0f;

    [SerializeField] private List<GameObject> m_EnemyPrefabs;

    void Start()
    {
        StartCoroutine(SpawnObject(delayAndSpawnRate));
    }

    private void SpawnRandomEnemyAtPosition(Vector3 spawnPoint)
    {
        if (m_EnemyPrefabs.Count > 0)
        {
            GameObject enemy = Instantiate(m_EnemyPrefabs[0], spawnPoint, Quaternion.identity);
        }
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

            if (spawnRateCountdown < 0 && delayAndSpawnRate > 1)
            {
                spawnRateCountdown += timeUntilSpawnRateIncrease;
                delayAndSpawnRate -= 0.1f;
            }
        }
    }
}