using System;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private int attackDamage = -1;
    
    // Events
    public Action<int> OnAttack;
    
    private void Awake()
    {
        PlayerController playerController = FindAnyObjectByType<PlayerController>();
        playerController.OnAttackAction += OnAttackAction;
    }
    
    #region Event Callbacks

    private void OnAttackAction()
    {
        OnAttack?.Invoke(attackDamage);
    }
    
    #endregion
}
