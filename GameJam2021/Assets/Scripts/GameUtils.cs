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
}
