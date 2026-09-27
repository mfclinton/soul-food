using System.Collections.Generic;
using UnityEngine;

public class ComboState
{
    // Properties
    private int maxComboLength;
    
    // Public Accessibles
    public bool ComboBufferFull => maxComboLength <= ComboBuffer.Count;
    
    // Internal Variables
    public List<ComboAction> ComboBuffer { get; private set; }
    
    public ComboState(int maxComboLength = 3)
    {
        this.maxComboLength = maxComboLength;
        ComboBuffer = new List<ComboAction>();
    }

    #region Combo Buffer Methods

    public bool AddToComboBuffer(ComboAction action)
    {
        if (ComboBufferFull)
        {
            Debug.LogWarning("Combo Buffer is full!");
            return false;
        }
        
        ComboBuffer.Add(action);
        return true;
    }
    
    public void ClearComboBuffer()
    {
        ComboBuffer.Clear();
    }

    #endregion

    #region Helpers

    public bool CompareComboBuffer(ComboAction[] requiredComboActions)
    {
        if (ComboBuffer.Count != requiredComboActions.Length)
            return false;

        for (int i = 0; i < ComboBuffer.Count; i++)
        {
            if (ComboBuffer[i] != requiredComboActions[i])
                return false;
        }

        return true;
    }

    #endregion
}
