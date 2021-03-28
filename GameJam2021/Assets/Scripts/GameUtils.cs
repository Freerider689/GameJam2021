using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameUtils
{
    public static Transform GetTopLevelTransform(Transform transform)
    {
        while (transform.parent != null)
        {
            transform = transform.parent;
        }
        return transform;
    }

    public static void PauseGame()
    {
        Time.timeScale = 0;
    }   
    
    public static void ResumeGame()
    {
        Time.timeScale = 0;
    }
}
