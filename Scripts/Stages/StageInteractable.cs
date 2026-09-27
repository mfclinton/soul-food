using System;
using UnityEngine;

public class StageInteractable : StageElement
{
    // Events
    public Action OnInteract;
    
    public void Interact()
    {
        OnInteract?.Invoke();
    }

    #region Editor Functions

    private void OnValidate()
    {
        SetToInteractableLayer();
    }
    
    private void SetToInteractableLayer()
    {
        int layer = LayerMask.NameToLayer("Interactable");
        if (layer == -1)
        {
            Debug.LogError("Interactable layer not found. Please make sure it exists in the project settings.");
            return;
        }

        gameObject.layer = layer;
    }

    #endregion
}
