using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    private Transform _target;

    public float speed;

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

}
