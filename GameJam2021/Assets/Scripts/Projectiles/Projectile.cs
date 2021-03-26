using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    private Transform _target;

    public float speed;
    public DamageTypeEnum damageType;
    public float targetSpeedModifier = 0.0f;
    public int targetArmorModifier = 0;

    public void Seek(Transform target)
    {
        _target = target;
    }

    private void Update()
    {
        if (_target==null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = _target.localPosition - transform.localPosition;
        direction.y += _target.localScale.y;

        float distanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    public void HitTarget()
    {
        Destroy(gameObject);
    }

    public void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collision detected");
        if (other.gameObject.tag == "Enemy")
        {
            var myScript = other.gameObject.GetComponent<BaseEnemyBehaviour>();
            myScript.registerHit();
        }

    }

    [System.Serializable]
    public enum DamageTypeEnum
    {
        NORMAL,
        ICE,
        STUN,
        SLOW,
        POISON,
        FIRE
    };

}
