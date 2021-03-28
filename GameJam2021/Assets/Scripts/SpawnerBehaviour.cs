using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehaviour : MonoBehaviour
{
    public float delayAndSpawnRate = 2.0f;
    public float timeUntilSpawnRateIncreases = 30.0f;

    public Color gizmosColor = Color.blue;
    public bool alwaysDrawGizmos = true;

    public EnemyPath path;

    [SerializeField] private List<GameObject> m_EnemyPrefabs;

    void Start()
    {
        StartCoroutine(SpawnObject(delayAndSpawnRate));
    }

    private void SpawnRandomEnemyAtPosition(Vector3 spawnPoint)
    {
        if (m_EnemyPrefabs.Count > 0)
        {
            GameObject enemyObject = Instantiate(m_EnemyPrefabs[0], spawnPoint, Quaternion.identity);
            BaseEnemyBehaviour enemy = enemyObject.GetComponent<BaseEnemyBehaviour>();
            enemy.path = path;
        }
    }

    IEnumerator SpawnObject(float firstDelay)
    {
        float spawnRateCountdown = timeUntilSpawnRateIncreases;
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

            if (spawnRateCountdown <= 0 && (delayAndSpawnRate - 0.01f) > 0)
            {
                spawnRateCountdown += timeUntilSpawnRateIncreases;
                delayAndSpawnRate -= 0.01f;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (alwaysDrawGizmos)
        {
            DrawGizmos();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!alwaysDrawGizmos)
        {
            DrawGizmos();
        }
    }

    private void DrawGizmos()
    {
        Gizmos.color = gizmosColor;
        Gizmos.DrawSphere(transform.position, 1f);
    }
}