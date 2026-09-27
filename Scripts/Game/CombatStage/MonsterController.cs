using System;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [Header("Monster Properties")]
    [SerializeField] private Monster monsterPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform monsterParent;
    [SerializeField] private float xPositionOffset = 3f;
    
    // Events
    public Action<MonsterData> OnMonsterSummoned;
    public Action<MonsterData> OnMonsterSlain;
    public Action OnMonsterStageCompleted;
    
    // Internal Variables
    private MonstersState monstersState;
    public MonstersState MonstersState => monstersState;
    
    // Component References
    public static MonsterController Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this;
        
        monstersState = new MonstersState();
        
        // Events
        AttackController attackController = FindAnyObjectByType<AttackController>();
        attackController.OnAttack += OnAttack;
    }
    
    private void OnAttack(int damage)
    {
        Monster monster = monstersState.GetNextMonster();
        if(monster == null)
            return;
        
        monster.ModifyHealth(damage);
    }
    
    #region Monster Management

    public void SummonMonster(MonsterData monsterData)
    {
        Monster monster = Instantiate(monsterPrefab, spawnPoint.position, Quaternion.identity, monsterParent);
        monster.Initialize(monsterData);
        
        // Add to XPos based on index
        int index = monstersState.MonstersList.Count;
        monster.transform.position += new Vector3(index * xPositionOffset, 0, 0);
        
        // Events
        monster.OnDeath += () => OnMonsterDeath(monster);
        
        monstersState.AddMonster(monster);
        
        OnMonsterSummoned?.Invoke(monster.MonsterData);
    }
    
    #endregion
    
    #region Event Management
    
    public void OnMonsterDeath(Monster monster)
    {
        monstersState.RemoveMonster(monster);
        
        OnMonsterSlain?.Invoke(monster.MonsterData);
        if (monstersState.MonstersList.Count == 0)
            OnMonsterStageCompleted?.Invoke();
    }
    
    #endregion
}
