using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyBehaviour : MonoBehaviour
{
    public int health = 10;
    public int value = 1;

    public EnemyPath path;

    public float speed = 1.0f;

    private PathWaypoint m_CurrentWaypoint;

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

        if (path != null)
        {
            UpdatePathMovement();
        }
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