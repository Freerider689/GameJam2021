using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyBehaviour : MonoBehaviour
{
    public int health = 10;
    public int value = 1;

    public float speed = 1.0f;

    void Start()
    {
    }

    void Update()
    {
        RaycastHit hit;
        if ((Physics.Raycast(transform.localPosition, -Vector3.up, out hit, Mathf.Infinity)))
        {
            transform.localPosition = hit.point;
        }
    }
}