using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    private Transform _target;

    public float speed;
    public float targetSpeedModifier = 0.0f;
    public int targetArmorModifier = 0;

    private DamageTypeEnum damageType = DamageTypeEnum.NORMAL;

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
        if (_target.gameObject.tag == "Enemy")
        {
            var myScript = _target.gameObject.GetComponent<BaseEnemyBehaviour>();

            switch (damageType)
            {
                case DamageTypeEnum.ICE:
                    StartCoroutine(myScript.setFrozen(-1.0f, 5, 2));
                    break;             
                case DamageTypeEnum.STUN:
                    StartCoroutine(myScript.setStun(0.2f));
                    break;
                case DamageTypeEnum.SLOW:
                    StartCoroutine(myScript.setSlowed(-1.0f, 0.2f));
                    break;
                case DamageTypeEnum.FIRE:
                    StartCoroutine(myScript.setBurn(-5, 0.2f));
                    break;
                case DamageTypeEnum.POISON:
                    StartCoroutine(myScript.setPoison(-1.0f, -5, 0.2f));
                    break;
                default:
                    myScript.registerHit();
                    break;
            }
        }

        Destroy(gameObject);
    }


    public void setDamageType(DamageTypeEnum newDamageType)
    {
        this.damageType = newDamageType;
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
