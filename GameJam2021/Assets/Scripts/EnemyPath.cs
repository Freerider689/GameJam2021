using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EnemyPath : MonoBehaviour
{
    public Color gizmosColor = Color.red;
    public bool alwaysDrawGizmos = true;

    public int WaypointsCount => m_WayPoints != null ? m_WayPoints.Count : 0;

    private List<PathWaypoint> m_WayPoints = new List<PathWaypoint>();
    private UnityTimer m_RefreshWaypointsTimer;

    private void OnEnable()
    {
        CreateRefreshWaypointsTimer();
        RefreshWaypoints();
    }

    private void CreateRefreshWaypointsTimer()
    {
        m_RefreshWaypointsTimer = new UnityTimer(RefreshWaypoints, 0.2f);
    }

    private void Update()
    {
        if (Application.isEditor)
        {
            if (m_RefreshWaypointsTimer == null) CreateRefreshWaypointsTimer();
            else m_RefreshWaypointsTimer.Update(Time.deltaTime);
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

    public void RefreshWaypoints()
    {
        m_WayPoints = new List<PathWaypoint>(GetComponentsInChildren<PathWaypoint>());
    }

    public PathWaypoint GetNextWaypoint(PathWaypoint currentWaypoint = null)
    {
        if (m_WayPoints == null || m_WayPoints.Count == 0) return null;
        if (currentWaypoint == null) return m_WayPoints[0];
        for (int i = 0; i < m_WayPoints.Count; i++)
        {
            if (m_WayPoints[i] == currentWaypoint)
            {
                if (i + 1 == m_WayPoints.Count) return null;
                return m_WayPoints[i + 1];
            }
        }
        return null;
    }

    private void DrawGizmos()
    {
        Gizmos.color = gizmosColor;
        for (int i = 0; i < m_WayPoints.Count; i++)
        {
            PathWaypoint first = m_WayPoints[i];
            Gizmos.DrawSphere(first.gameObject.transform.position, 1f);

            if (i < m_WayPoints.Count - 1)
            {
                PathWaypoint second = m_WayPoints[i + 1];
                Gizmos.DrawSphere(second.gameObject.transform.position, 1f);
                Gizmos.DrawLine(first.gameObject.transform.position, second.gameObject.transform.position);
            }
        }
    }
}
