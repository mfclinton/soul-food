using System;
using UnityEngine;

public class StageElement : MonoBehaviour
{
    // Properties
    [SerializeField] private Stage[] assignedStages;
    
    // Events
    public Action<bool> OnStateUpdated;
    
    // Internal Variables
    private bool isActive;
    
    #region Unity Functions
    
    private void OnEnable()
    {
        foreach (Stage stage in assignedStages)
        {
            stage.OnStageEnter += OnStageEnter;
            stage.OnStageExit += OnStageExit;
        }
    }
    
    private void OnDisable()
    {
        foreach (Stage stage in assignedStages)
        {
            stage.OnStageEnter -= OnStageEnter;
            stage.OnStageExit -= OnStageExit;
        }
    }
    
    #endregion
    
    #region Event Functions
    
    private void OnStageEnter()
    {
        UpdateState(true);
    }
    
    private void OnStageExit()
    {
        UpdateState(false);
    }
    
    #endregion
    
    // Update State
    public void UpdateState(bool state)
    {
        if(state == isActive)
            return;
        
        isActive = state;
        OnStateUpdated?.Invoke(isActive);
    }
}
