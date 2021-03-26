using System;
using System.Collections;
using System.Collections.Generic;
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

    private HashSet<StatusEffectEnum> m_currentStatusEffects;

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
        m_currentStatusEffects = new HashSet<StatusEffectEnum>();
        speed = _baseSpeed;
        armor = _baseArmor;
    }


    public void registerHit()
    {
        Debug.Log("Captain I'm hit!");
    }

    public IEnumerator setFrozen(float speedModifier, int armorModifier, float effectDuration)
    {
        Debug.Log("Freezer");
        if (!m_currentStatusEffects.Add(StatusEffectEnum.FROZEN)) yield break;
        speed += speedModifier;
        armor += armorModifier;
        yield return new WaitForSeconds(effectDuration);
        speed -= speedModifier;
        armor -= armorModifier;
        m_currentStatusEffects.Remove(StatusEffectEnum.FROZEN);
    }

    public IEnumerator setSlowed(float speedModifier, float effectDuration)
    {
        if (!m_currentStatusEffects.Add(StatusEffectEnum.SLOWED)) yield break;
        speed += speedModifier;
        yield return new WaitForSeconds(effectDuration);
        speed -= speedModifier;
        m_currentStatusEffects.Remove(StatusEffectEnum.SLOWED);
    }

    public IEnumerator setStun(float speedModifier, float effectDuration)
    {
        if (!m_currentStatusEffects.Add(StatusEffectEnum.STUN)) yield break;
        speed += speedModifier;
        yield return new WaitForSeconds(effectDuration);
        speed -= speedModifier;
        m_currentStatusEffects.Remove(StatusEffectEnum.STUN);
    }

    public IEnumerator setPoison(float healthDamageAtInterval, float effectDuration)
    {
        if (!m_currentStatusEffects.Add(StatusEffectEnum.POISONED)) yield break;
        this.healthDamageAtInterval += healthDamageAtInterval;
        yield return new WaitForSeconds(effectDuration);
        this.healthDamageAtInterval -= healthDamageAtInterval;
        m_currentStatusEffects.Remove(StatusEffectEnum.POISONED);
    }

    public IEnumerator setBurn(float effectDuration)
    {
        if (!m_currentStatusEffects.Add(StatusEffectEnum.BURN)) yield break;
        this.healthDamageAtInterval += healthDamageAtInterval;
        yield return new WaitForSeconds(effectDuration);
        this.healthDamageAtInterval -= healthDamageAtInterval;
        m_currentStatusEffects.Remove(StatusEffectEnum.BURN);
    }

    public IEnumerator setHealing(float healthGainAtInterval, float effectDuration)
    {
        if (!m_currentStatusEffects.Add(StatusEffectEnum.HEALING)) yield break;
        this.healthGainAtInterval += healthGainAtInterval;
        yield return new WaitForSeconds(effectDuration);
        this.healthGainAtInterval -= healthGainAtInterval;
        m_currentStatusEffects.Remove(StatusEffectEnum.HEALING);
    }

    [System.Serializable]
    public enum StatusEffectEnum
    {
        FROZEN,
        STUN,
        SLOWED,
        POISONED,
        BURN,
        HEALING
    };
}