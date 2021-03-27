using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BaseEnemyBehaviour : MonoBehaviour
{
    private static readonly float _baseSpeed = 2.0f;
    private static readonly int _baseArmor = 5;

    public int health = 10;
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
    }

    public void revertBackToNormal()
    {
        speed = _baseSpeed;
        armor = _baseArmor;
    }


    public void registerHit()
    {
        Debug.Log("Captain I'm hit!");
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
}