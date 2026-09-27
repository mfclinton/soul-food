using System;
using UnityEngine;

[Serializable]
public class StageTransitionDataEntry
{
    public DefaultActions action;
    public Stage stage;
}

[Serializable]
public class StageTransitionData
{
    // Properties
    public StageTransitionDataEntry[] stageTransitions;
    
    public Stage GetStage(DefaultActions action)
    {
        if(stageTransitions == null)
            return null;
        
        foreach (var stageTransition in stageTransitions)
        {
            if (stageTransition.action == action)
                return stageTransition.stage;
        }

        return null;
    }
    
    // Validate
    private void OnValidate()
    {
        foreach (var stageTransition in stageTransitions)
        {
            if (stageTransition.stage == null)
            {
                Debug.LogError("StageTransitionData: Stage is null in " + stageTransition.action);
            }
        }
    }
}
