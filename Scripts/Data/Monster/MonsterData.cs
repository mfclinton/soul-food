using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "NewMonsterData", menuName = "Monster/Create New Monster")]
public class MonsterData : GameScriptableObject
{
    [Header("Config Properties")]
    public MonsterType monsterType;
    public int monsterHP;
    
    [Header("Visual Properties")]
    public Sprite monsterSprite; // TODO: Remove later
    public RuntimeAnimatorController animatorController;
}
