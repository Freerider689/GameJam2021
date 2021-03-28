using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    [HideInInspector] public Transform _target;
    public float range;
    public float damage = 1.0f;
    public float fireRate;
    public int cost = 10;
    [HideInInspector] public float timeTillNextShot = 0f;

    public string enemyTag = "Enemy";

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    private void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = 2000f;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.localPosition, enemy.transform.localPosition);
            if (distanceToEnemy < shortestDistance)
            {
                // Test line of sight
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            _target = nearestEnemy.transform;
        }
        else
        {
            _target = null;
        }
    }

    private void Update()
    {
        if (Physics.Raycast(transform.localPosition,
            Vector3.down,
            out var hit,
            Mathf.Infinity))
        {
            float y = hit.point.y + (0.5f * transform.localScale.y);
            transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
        }

        if (_target == null)
            return;

        transform.LookAt(_target);

        if (timeTillNextShot <= 0)
        {
            Attack();
            timeTillNextShot = 1 / fireRate;
        }

        timeTillNextShot -= Time.deltaTime;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.localPosition, range);
    }

    public abstract void Attack();
}