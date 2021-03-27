using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class TowerSelection : MonoBehaviour
{
    private static readonly string BUTTON_TOWER_1 = "Tower1";
    private static readonly string BUTTON_TOWER_2 = "Tower2";

    public UnityEngine.UIElements.UIDocument uIDocument;
    public UnityEngine.UIElements.Button buttonSelectTower1;
    public UnityEngine.UIElements.Button buttonSelectTower2;

    public GameObject tower1Prefab;
    public GameObject tower2Prefab;

    private bool dragging = false;
    private Vector3 startDist;
    private GameObject selectedGameObject;

    public void Start()
    {
        var elementsInDOM = new List<UnityEngine.UIElements.VisualElement>();
        this.getAllContainedElements(uIDocument.rootVisualElement, ref elementsInDOM);

        for (int i = 0; i < elementsInDOM.Count; i++)
        {
            if (BUTTON_TOWER_1 == elementsInDOM[i].name)
            {
                buttonSelectTower1 = (UnityEngine.UIElements.Button) elementsInDOM[i];
            }
            else if (BUTTON_TOWER_2 == elementsInDOM[i].name)
            {
                buttonSelectTower2 = (UnityEngine.UIElements.Button) elementsInDOM[i];
            }
        }

        buttonSelectTower1.clicked += ButtonSelectTower1Clicked;
        buttonSelectTower2.clicked += ButtonSelectTower2Clicked;
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

    private void ButtonSelectTower1Clicked()
    {
        Debug.Log("Tower 1 selected");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        selectedGameObject = Instantiate(tower1Prefab, ray.origin, Quaternion.identity);
        dragging = true;

    }

    private void ButtonSelectTower2Clicked()
    {
        Debug.Log("Tower 2 selected");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        selectedGameObject = Instantiate(tower2Prefab, ray.origin, Quaternion.identity);
        dragging = true;
    }

    private void releaseSelection()
    {
        dragging = false;
        selectedGameObject = null;
    }

    void Update()
    {
        Debug.Log($"SelectedObject {selectedGameObject} | Dragging {dragging}");
        if (selectedGameObject != null && dragging)
        {
            float y = selectedGameObject.transform.localPosition.y;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit))
            {
                Vector3 rayPoint = hit.point;
                selectedGameObject.transform.position =
                    new Vector3(startDist.x + rayPoint.x, y, startDist.z + rayPoint.z);
            }
        }

        if (Input.GetMouseButton(0))
        {
            releaseSelection();
        }
    }
}