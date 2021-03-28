using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    private static readonly int _baseHealth = 1_000;

    public UnityEngine.UIElements.UIDocument uIDocument;
    public UnityEngine.UIElements.Label waveCounterLabel;
    public UnityEngine.UIElements.Label healthLabel;
    public UnityEngine.UIElements.Label moneyLabel;
    public int health = _baseHealth;
    public int money = 50;
    public int level = 1;

    void Start()
    {
        var elementsInDOM = new List<UnityEngine.UIElements.VisualElement>();
        this.getAllContainedElements(uIDocument.rootVisualElement, ref elementsInDOM);

        for (int i = 0; i < elementsInDOM.Count; i++)
        {
            if ("WaveCounter" == elementsInDOM[i].name)
            {
                waveCounterLabel = (UnityEngine.UIElements.Label) elementsInDOM[i];
            }
            else if ("Health" == elementsInDOM[i].name)
            {
                healthLabel = (UnityEngine.UIElements.Label) elementsInDOM[i];
            }
            else if ("Money" == elementsInDOM[i].name)
            {
                moneyLabel = (UnityEngine.UIElements.Label) elementsInDOM[i];
            }
        }

    }

    void FixedUpdate()
    {
        UpdateLabels();
    }

    void UpdateLabels()
    {
        waveCounterLabel.text = $"Wave  {level}";
        healthLabel.text = $"{health}/{_baseHealth}";
        moneyLabel.text = $"{money}$";
    }

    public void getAllContainedElements(UnityEngine.UIElements.VisualElement parentElement,
        ref List<UnityEngine.UIElements.VisualElement> allElements)
    {
        allElements.Add(parentElement);

        for (int i = 0; i < parentElement.childCount; i++)
        {
            getAllContainedElements(parentElement[i], ref allElements);
        }
    }

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