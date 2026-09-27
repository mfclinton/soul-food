using System;
using System.Linq;
using UnityEngine;

public class CookController : MonoBehaviour
{
    // Events
    public Action<CookState> OnCookExecuted;
    public Action OnCookStageCompleted;
    
    // Internal Variables
    private CookState cookState;
    
    private void Awake()
    {
        cookState = new CookState();
        
        // Input Callback
        PlayerController pc = FindAnyObjectByType<PlayerController>();
        pc.OnCookAction += OnCookAction;
        
        // Events
        MonsterController monsterController = FindAnyObjectByType<MonsterController>();
        monsterController.OnMonsterSlain += OnMonsterSlain;
    }

    #region Event Callbacks

    private void OnMonsterSlain(MonsterData monsterData)
    {
        cookState.AddSlainMonster(monsterData);
    }

    private void OnCookAction()
    {
        Debug.Log("Cooking the food recipe!");
        Debug.Log($"Slain Monsters: {string.Join(", ", cookState.SlainMonsters.Select(sm => sm.monsterType.ToString()))}");
        
        OnCookExecuted?.Invoke(cookState);
        cookState.Reset();
        OnCookStageCompleted?.Invoke();
    }

    #endregion
}
