using System.Collections.Generic;
using UnityEngine;

public class MonstersState
{
    public List<Monster> MonstersList { get; private set; }
    
    public MonstersState()
    {
        MonstersList = new List<Monster>();
    }

    #region Monster Management
    
    public void ResetMonsters()
    {
        MonstersList.Clear();
    }

    public void AddMonster(Monster monster)
    {
        MonstersList.Add(monster);
    }
    
    public void RemoveMonster(Monster monster)
    {
        MonstersList.Remove(monster);
    }

    public Monster GetNextMonster()
    {
        if(MonstersList.Count == 0)
            return null;
        
        return MonstersList[0];
    }

    #endregion
}
