using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    // Default Input Actions
    public Action<Vector2> OnMoveAction;
    public Action OnInteractAction;
    
    // Spell Stage Events
    public Action OnCastAction;
    public Action OnSwapToCombatModeAction;
    public Action<ComboAction> OnComboAction;
    
    // Combat Stage Events
    public Action OnAttackAction;
    
    // Cook Stage Events
    public Action OnCookAction;
    
    // Internal References
    private GameplayActions gameplayActions;
    
    private void Awake()
    {
        // Creates a Input Actions instance
        gameplayActions = new GameplayActions();
        gameplayActions.Enable();
        
        // Subscribes to Default Input Events
        gameplayActions.PlayerDefaultActions.Move.performed += OnMove;
        gameplayActions.PlayerDefaultActions.Interact.performed += OnInteract;
        
        // Subscribes to Spell Stage Input Events
        gameplayActions.PlayerSpellActions.Cast.performed += OnCast;
        gameplayActions.PlayerSpellActions.SwapToCombatMode.performed += OnSwapToCombatMode;
        
        gameplayActions.PlayerSpellActions.ComboUp.performed += OnComboUp;
        gameplayActions.PlayerSpellActions.ComboRight.performed += OnComboRight;
        gameplayActions.PlayerSpellActions.ComboDown.performed += OnComboDown;
        gameplayActions.PlayerSpellActions.ComboLeft.performed += OnComboLeft;
        
        // Subscribes to Combat Stage Input Events
        gameplayActions.PlayerCombatActions.Attack.performed += OnAttack;
        
        // Subscribes to Cook Stage Input Events
        gameplayActions.PlayerCookActions.Cook.performed += OnCook;
    }
    
    #region Default Input Callbacks
    
    private void OnMove(InputAction.CallbackContext context)
    {
        OnMoveAction?.Invoke(context.ReadValue<Vector2>());
    }
    
    private void OnInteract(InputAction.CallbackContext context)
    {
        OnInteractAction?.Invoke();
    }
    
    #endregion
    
    #region Player Spell Stage Input Callbacks

    // Combo Callbacks
    private void OnComboUp(InputAction.CallbackContext context)
    {
        PerformAction(ComboAction.Up);
    }
    
    private void OnComboRight(InputAction.CallbackContext context)
    {
        PerformAction(ComboAction.Right);
    }
    
    private void OnComboDown(InputAction.CallbackContext context)
    {
        PerformAction(ComboAction.Down);
    }
    
    private void OnComboLeft(InputAction.CallbackContext context)
    {
        PerformAction(ComboAction.Left);
    }
    
    // Other Callbacks
    
    private void PerformAction(ComboAction action)
    {
        OnComboAction?.Invoke(action);
    }

    private void OnCast(InputAction.CallbackContext context)
    {
        OnCastAction?.Invoke();
    }
    
    private void OnSwapToCombatMode(InputAction.CallbackContext obj)
    {
        OnSwapToCombatModeAction?.Invoke();
    }
    
    #endregion

    #region Player Combat Stage Input Callbacks

    private void OnAttack(InputAction.CallbackContext context)
    {
        OnAttackAction?.Invoke();
    }

    #endregion
    
    #region Player Cook Stage Input Callbacks

    private void OnCook(InputAction.CallbackContext context)
    {
        OnCookAction?.Invoke();
    }

    #endregion

    #region Input Mode Swapping
    
    // Swap to Stage Methods
    public void SwapToDefaultStage()
    {
        DisableAllInputActions();
    }
    
    public void SwapToCombatStage()
    {
        DisableAllInputActions();
        gameplayActions.PlayerCombatActions.Enable();
    }
    
    public void SwapToSpellStage()
    {
        DisableAllInputActions();
        gameplayActions.PlayerSpellActions.Enable();
    }
    
    public void SwapToCookStage()
    {
        DisableAllInputActions();
        gameplayActions.PlayerCookActions.Enable();
    }

    // Helper Methods
    private void DisableAllInputActions()
    {
        gameplayActions.PlayerSpellActions.Disable();
        gameplayActions.PlayerCombatActions.Disable();
        gameplayActions.PlayerCookActions.Disable();
    }
    
    public void DisablePlayer()
    {
        DisableAllInputActions();
        gameplayActions.PlayerDefaultActions.Disable();
    }
    
    #endregion
    
}