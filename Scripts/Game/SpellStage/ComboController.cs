using System;
using System.Collections.Generic;
using UnityEngine;

public class ComboController : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private int maxComboLength = 3;
    
    // Events
    public Action<ComboState> OnComboStateChanged;
    public Action<ComboState> OnComboCompleted;
    public Action OnComboStageCompleted;
    public Action OnComboCancelled;
    
    // Internal Variables
    private ComboState comboState;
    
    // Internal References
    private MonsterController monsterController; // TODO: find a better decoupled way to do this
    
    private void Awake()
    {
        monsterController = FindAnyObjectByType<MonsterController>();
        
        comboState = new ComboState(maxComboLength);
        
        PlayerController playerController = FindAnyObjectByType<PlayerController>();
        playerController.OnComboAction += OnComboAction;
        playerController.OnCastAction += OnCastAction;
        playerController.OnSwapToCombatModeAction += OnSwapToCombatModeAction;
        
        StageController stageController = FindAnyObjectByType<StageController>();
        stageController.OnStageChanged += OnStageChanged;
    }

    #region Event Callbacks

    private void OnComboAction(ComboAction action)
    {
        comboState.AddToComboBuffer(action);
        OnComboStateChanged?.Invoke(comboState);
    }
    
    private void OnCastAction()
    {
        OnComboCompleted?.Invoke(comboState);
        
        comboState.ClearComboBuffer();
        
        ClearCombo();
    }
    
    private void OnSwapToCombatModeAction()
    {
        if (!CanSwapToCombatMode())
            return;
        
        OnComboStageCompleted?.Invoke();
    }
    
    private void OnStageChanged(Stage oldStage, Stage newStage)
    {
        ClearCombo();
    }

    #endregion
    
    private void ClearCombo()
    {
        comboState.ClearComboBuffer();
        OnComboStateChanged?.Invoke(comboState);
        OnComboCancelled?.Invoke();
    }
    
    private bool CanSwapToCombatMode()
    {
        return monsterController != null && monsterController.MonstersState.MonstersList.Count > 0;
    }
}
