using System;
using System.Collections.Generic;
using UnityEngine;

public class CookState
{
    public List<MonsterData> SlainMonsters { get; private set; }
    
    public CookState()
    {
        SlainMonsters = new List<MonsterData>();
    }

    #region Cook Management

    public void Reset()
    {
        SlainMonsters.Clear();
    }
    
    public void AddSlainMonster(MonsterData monsterData)
    {
        SlainMonsters.Add(monsterData);
    }

    #endregion
}
