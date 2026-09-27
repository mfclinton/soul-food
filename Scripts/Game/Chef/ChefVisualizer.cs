using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ChefVisualizer : MonoBehaviour
{
    // Internal References
    private Animator animator;
    
    // Anim Constants
    private static readonly int AnimTriggerHit = Animator.StringToHash("Hit");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        
        // Events
        AttackController attackController = FindAnyObjectByType<AttackController>();
        attackController.OnAttack += OnAttack;
    }
    
    private void OnAttack(int damage)
    {
        animator.SetTrigger(AnimTriggerHit);
    }
}
