using System;
using UnityEngine;

public class Stage : MonoBehaviour
{
    // Properties
    [SerializeField] StageType stageType;
    [SerializeField] private StageTransitionData stageTransitionData;
    [SerializeField] private Transform cameraPosTransform;
    
    // Getters
    public StageType StageType => stageType;
    public StageTransitionData StageTransitionData => stageTransitionData;
    public Transform CameraPosTransform => cameraPosTransform;
    
    // Events
    public Action OnStageEnter;
    public Action OnStageExit;

    // Methods
    public void Enter()
    {
        OnStageEnter?.Invoke();
    }
    
    public void Exit()
    {
        OnStageExit?.Invoke();
    }
}
