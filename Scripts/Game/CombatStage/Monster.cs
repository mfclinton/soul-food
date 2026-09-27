using System;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public MonsterData MonsterData { get; private set; }
    public bool IsDead => currentHP <= 0;
    
    // Events
    public event Action<MonsterData> OnInitialized;
    
    public delegate void HealthModifiedHandler(float newHP, float oldHP);
    public event HealthModifiedHandler OnHealthModified;
    
    public event Action OnDeath;
    
    // Internal variables
    private int maxHP;
    private int currentHP;
    
    public void Initialize(MonsterData data)
    {
        this.MonsterData = data;
        
        gameObject.name = data.dataName;
        
        maxHP = data.monsterHP;
        currentHP = data.monsterHP;
        
        OnInitialized?.Invoke(data);
    }
    
    public void ModifyHealth(int amount)
    {
        int oldHP = currentHP;
        int newHP = Mathf.Clamp(currentHP + amount, 0, maxHP);
        currentHP = newHP;
        
        OnHealthModified?.Invoke(newHP, oldHP);
        
        if(IsDead)
            Die();
    }

    private void Die()
    {
        OnDeath?.Invoke();
    }
    
    public void TriggerDeletion()
    {
        Destroy(gameObject);
    }
}
