using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = System.Numerics.Vector3;

public class UISmoothList : MonoBehaviour
{
    [Header("Parent References")]
    [SerializeField] private Transform regularParent;
    [SerializeField] private Transform layoutGroupParent;
    
    [Header("Spawn / Death Settings")]
    [SerializeField] private Transform spawnPoint;

    // Internal References
    private List<KeyValuePair<GameObject, GameObject>> uiElements = new List<KeyValuePair<GameObject, GameObject>>();
    
    // Create a copy of a UI element with all UIMask elements invisible
    public GameObject Add(GameObject element)
    {
        GameObject newElement = Instantiate(element, spawnPoint.position, Quaternion.identity, regularParent);
        GameObject newLayoutElement = Instantiate(element, spawnPoint.position, Quaternion.identity, layoutGroupParent);
        MakeAllInvisible(newLayoutElement);

        UIFollowElement followComponent = newElement.AddComponent<UIFollowElement>();
        followComponent.Configure(newLayoutElement.transform);

        uiElements.Add(new KeyValuePair<GameObject, GameObject>(newElement, newLayoutElement));
        
        return newElement;
    }

    // Remove a UI element from the list
    public void Remove(GameObject element)
    {
        int index = uiElements.FindIndex(pair => pair.Key == element);
        if (index != -1)
        {
            Destroy(uiElements[index].Key);
            Destroy(uiElements[index].Value);

            uiElements.RemoveAt(index);
        }
    }
    
    private void MakeAllInvisible(GameObject element)
    {
        MaskableGraphic[] maskableGraphics = element.GetComponents<MaskableGraphic>();
        foreach (var graphic in maskableGraphics)
            graphic.color = new Color();

        foreach (var graphic in element.GetComponentsInChildren<MaskableGraphic>())
            graphic.color = new Color();

    }
}