using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityTimer
{
    public float TimeBetweenTwoActions { get; }

    private Action m_Action;
    private float m_TimeSinceLastAction = 0f;

    public UnityTimer(Action action, float timeBetweenAction)
    {
        m_Action = action;
        TimeBetweenTwoActions = timeBetweenAction;
    }


    public void Update(float deltaTime)
    {
        m_TimeSinceLastAction += deltaTime;
        if (m_TimeSinceLastAction >= TimeBetweenTwoActions)
        {
            m_TimeSinceLastAction -= TimeBetweenTwoActions;
            m_Action.Invoke();
        }
    }
}
