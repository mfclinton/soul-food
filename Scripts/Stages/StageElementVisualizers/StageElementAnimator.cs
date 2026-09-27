using System;
using UnityEngine;

[RequireComponent(typeof(StageElement))]
public class StageElementAnimator : MonoBehaviour
{
    // Internal References
    protected StageElement stageElement;
    protected Animator animator;

    #region Unity Functions

    protected virtual void Awake()
    {
        stageElement = GetComponent<StageElement>();
        animator = GetComponent<Animator>();
    }
    
    protected virtual void OnEnable()
    {
        stageElement.OnStateUpdated += OnStateUpdated;
    }
    
    protected virtual void OnDisable()
    {
        stageElement.OnStateUpdated -= OnStateUpdated;
    }

    #endregion

    #region Event Functions
    
    protected void OnStateUpdated(bool isActive)
    {
        animator?.SetBool(StageConstants.ANIM_BOOL_STAGE_ACTIVE, isActive);
    }
    
    #endregion
}
