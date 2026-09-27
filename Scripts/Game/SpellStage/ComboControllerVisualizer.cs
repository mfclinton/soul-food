using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboControllerVisualizer : MonoBehaviour
{
    [SerializeField] private Transform comboActionUIContainer;
    
    private void Awake()
    {
        ComboController comboController = FindAnyObjectByType<ComboController>();
        comboController.OnComboStateChanged += OnComboStateChanged;
    }

    private void OnComboStateChanged(ComboState comboState)
    {
        List<ComboAction> comboActions = comboState.ComboBuffer;
        foreach(Transform child in comboActionUIContainer)
            Destroy(child.gameObject);
        
        foreach(ComboAction comboAction in comboActions)
        {
            GameObject newImageObject = new GameObject("ComboActionImage");
            newImageObject.transform.SetParent(comboActionUIContainer.transform, false);
            Image comboActionImage = newImageObject.AddComponent<Image>();
            
            comboActionImage.sprite = DataManager.Instance.comboActionDataDictionary[comboAction].icon;
        }
    }
}
