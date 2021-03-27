using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    private static readonly int _baseHealth = 1_000;
    public int health = _baseHealth;
    public int money = 10;
    public int level = 1;

    private void OnTriggerEnter(Collider other)
    {
        GameObject topLevelObject = GameUtils.GetTopLevelTransform(other.gameObject.transform).gameObject;
        if (topLevelObject.tag.ToLowerInvariant().Contains("enemy"))
        {
            Destroy(topLevelObject);

            if (health > 0)
            {
                health--;
                Debug.Log($"O NO AN ENEMY TOUCHED YOUR BASE {health}/{_baseHealth}");
            }
            else
            {
                Debug.Log("GAME OVER, get lost noob!");
            }
        }
    }

    public void addMoney(int amount)
    {
        money += amount;
    }
}