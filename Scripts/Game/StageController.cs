using System;
using UnityEngine;
using UnityEngine.Serialization;

public class StageController : MonoBehaviour
{
    // Properties
    [SerializeField] private Stage initialStage;
    
    // State
    public Stage currentStage { get; private set; }
    
    // Events
    public delegate void StageChangedDelegate(Stage oldStage, Stage newStage);
    public event StageChangedDelegate OnStageChanged;
    
    // Internal References
    PlayerController playerController;
    
    private void Awake()
    {
        playerController = FindAnyObjectByType<PlayerController>();
        playerController.OnMoveAction += OnMove;
        playerController.OnInteractAction += OnInteract;
        
        Initialize();
    }

    private void Start()
    {
        // Initialize Stage
        TransitionToStage(initialStage);
    }

    private void Initialize()
    {
        // Subscribe to Stage Completions
        ComboController comboController = FindAnyObjectByType<ComboController>();
        MonsterController monsterController = FindAnyObjectByType<MonsterController>();
        CookController cookController = FindAnyObjectByType<CookController>();
        
        comboController.OnComboStageCompleted += () => ProcessAction(DefaultActions.OnStageComplete);
        monsterController.OnMonsterStageCompleted += () => ProcessAction(DefaultActions.OnStageComplete);
        cookController.OnCookStageCompleted += () => ProcessAction(DefaultActions.OnStageComplete);
    }

    #region Event Callbacks

    private void OnMove(Vector2 direction)
    {
        DefaultActions action = direction.GetDefaultAction();
        ProcessAction(action);
    }
    
    private void OnInteract()
    {
        DefaultActions action = DefaultActions.Interact;   
        ProcessAction(action);
    }

    #endregion

    #region Helpers

    void ProcessAction(DefaultActions action)
    {
        if(currentStage == null)
            return;
        
        Stage newStage = currentStage.StageTransitionData.GetStage(action);
        TransitionToStage(newStage);
    }
    
    void TransitionToStage(Stage newStage)
    {
        if(newStage == null || currentStage == newStage)
            return;
        
        Debug.Log("Transitioning to " + newStage);
        
        switch (newStage.StageType)
        {
            case StageType.Default:
                playerController.SwapToDefaultStage();
                break;
            case StageType.Spell:
                playerController.SwapToSpellStage();
                break;
            case StageType.Combat:
                playerController.SwapToCombatStage();
                break;
            case StageType.Cook:
                playerController.SwapToCookStage();
                break;
        }
        
        Stage oldStage = currentStage;
        currentStage = newStage;
        
        OnStageChanged?.Invoke(oldStage, currentStage);
        oldStage?.Exit();
        currentStage?.Enter();
    }
    
    #endregion
}
