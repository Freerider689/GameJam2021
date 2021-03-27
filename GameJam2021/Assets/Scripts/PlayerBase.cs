using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject topLevelObject = GameUtils.GetTopLevelTransform(other.gameObject.transform).gameObject;
        if (topLevelObject.tag.ToLowerInvariant().Contains("enemy"))
        {
            Destroy(topLevelObject);
            Debug.Log("O NO AN ENEMY TOUCHED YOUR BASE");
        }
    }
}
