using System;
using System.Collections;
using UnityEngine;

public class BaseEnemyBehaviour : MonoBehaviour
{
    private static readonly float _baseHealth = 10.0f;
    private static readonly float _baseSpeed = 1.0f;
    private static readonly int _baseArmor = 5;

    public EnemyPath path;

    private PathWaypoint m_CurrentWaypoint;

    public float health = _baseHealth;
    public int armor = _baseArmor;
    public float speed = _baseSpeed;
    public float healthDamageAtInterval = 0.0f;
    public float healthGainAtInterval = 0.0f;
    public int valueForKill = 1;

    void Update()
    {
        RaycastHit hit;
        if ((Physics.Raycast(transform.localPosition, -Vector3.up, out hit, Mathf.Infinity)))
        {
            transform.localPosition = hit.point;
        }

        if (path != null)
        {
            UpdatePathMovement();
        }
    }

    public void revertBackToNormal()
    {
        speed = _baseSpeed;
        armor = _baseArmor;
    }


    public void registerHit(float damage)
    {
        Debug.Log($"Captain I'm hit with damage {damage}! {health}/{10.0f}");
        health -= damage;

        if (health <= 0)
        {
            Debug.Log("Mein Leben #_#");
            Destroy(gameObject);
            return;
        }
    }

    IEnumerator statusTimer(float time, Action callback)
    {
        float timer = 0;
        while (timer < time)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        callback();
    }

    public IEnumerator setFrozen(float speedModifier, int armorModifier, int effectDuration)
    {
        Debug.Log($"Enemy frozen for {effectDuration}s");
        speed += speedModifier;
        armor += armorModifier;

        yield return StartCoroutine(statusTimer(effectDuration, () =>
        {
            Debug.Log("Enemy unfrozen");
            speed -= speedModifier;
            armor -= armorModifier;
        }));
    }

    public IEnumerator setSlowed(float speedModifier, float effectDuration)
    {
        Debug.Log("Enemy slowed");
        speed += speedModifier;

        yield return StartCoroutine(statusTimer(effectDuration, () =>
        {
            Debug.Log("Enemy speed normal");
            speed -= speedModifier;
        }));
    }

    public IEnumerator setStun(float effectDuration)
    {
        Debug.Log("Enemy stunned");
        speed = 0.0f;

        yield return StartCoroutine(statusTimer(effectDuration, () =>
        {
            Debug.Log("Enemy no longer stunned");
            speed = _baseSpeed;
        }));
    }

    public IEnumerator setPoison(float speedModifier, float damageAtTick, float effectDuration)
    {
        Debug.Log("Enemy poisoned");
        speed += speedModifier;
        this.healthDamageAtInterval += damageAtTick;

        yield return StartCoroutine(statusTimer(effectDuration, () =>
        {
            Debug.Log("Enemy no longer poisoned");
            this.healthDamageAtInterval -= damageAtTick;
            speed -= speedModifier;
        }));
    }

    public IEnumerator setBurn(float damageAtTick, float effectDuration)
    {
        Debug.Log("Enemy burning");
        this.healthDamageAtInterval += damageAtTick;

        yield return StartCoroutine(statusTimer(effectDuration, () =>
        {
            Debug.Log("Enemy no longer burning");
            this.healthDamageAtInterval -= damageAtTick;
        }));
    }

    public IEnumerator setHealing(float healthGainAtInterval, float effectDuration)
    {
        Debug.Log("Enemy healing");
        this.healthGainAtInterval += healthGainAtInterval;

        yield return StartCoroutine(statusTimer(effectDuration, () =>
        {
            Debug.Log("Enemy no longer healing"); 
            this.healthGainAtInterval -= healthGainAtInterval;
        }));
    }

    private void UpdatePathMovement()
    {
        if (m_CurrentWaypoint == null)
        {
            m_CurrentWaypoint = path.GetNextWaypoint();
            if (m_CurrentWaypoint == null) return;
        }

        Vector3 movement = Vector3.MoveTowards(transform.localPosition, m_CurrentWaypoint.gameObject.transform.position, Time.deltaTime * speed);
        transform.localPosition = movement;

        Vector3 objectPositionNoY = new Vector3(transform.position.x, 0f, transform.position.z);
        Vector3 waypointPositionNoY = new Vector3(m_CurrentWaypoint.transform.position.x, 0f, m_CurrentWaypoint.transform.position.z);

        float delta = Vector3.Distance(objectPositionNoY, waypointPositionNoY);
        if (delta < 0.1f)
        {
            m_CurrentWaypoint = path.GetNextWaypoint(m_CurrentWaypoint);
        }
    }
}