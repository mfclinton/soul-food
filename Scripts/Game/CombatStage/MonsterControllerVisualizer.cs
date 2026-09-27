using System;
using UnityEngine;

[RequireComponent(typeof(MonsterController))]
public class MonsterControllerVisualizer : MonoBehaviour
{
    [Header("Death Properties")]
    [SerializeField] private GameObject deathEffectPrefab;
    [SerializeField] private Transform deathEffectParent;
    [SerializeField] private float deathEffectDuration = 3f;
    
    // Internal References
    private MonsterController monsterController;

    public static MonsterControllerVisualizer Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        monsterController = GetComponent<MonsterController>();
    }

    #region Event Callbacks

    public void DeathPoof(Vector3 position)
    {
        GameObject deathEffect = Instantiate(deathEffectPrefab, position, Quaternion.identity, deathEffectParent);
        Destroy(deathEffect, deathEffectDuration);
    }

    #endregion
}
