using System;
using UnityEngine;

[RequireComponent(typeof(StageInteractable))]
public class StageInteractableAnimator : StageElementAnimator
{
    // Internal References
    protected StageInteractable stageInteractable;
    
    #region Unity Functions

    protected override void Awake()
    {
        base.Awake();
        stageInteractable = GetComponent<StageInteractable>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        stageInteractable.OnInteract += OnInteract;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        stageInteractable.OnInteract -= OnInteract;
    }

    #endregion

    #region Interaction Functions

    public void OnInteract()
    {
        animator?.SetTrigger(StageConstants.ANIM_TRIGGER_STAGE_INTERACT);
    }

    #endregion
}