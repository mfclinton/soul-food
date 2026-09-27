using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class FoodRecipeDataEntry
{
    public MonsterType monsterType;
    public int requiredCount;
}

[CreateAssetMenu(fileName = "NewFoodRecipe", menuName = "Cooking/FoodRecipe")]
public class FoodRecipe : Recipe
{
    [SerializeField] private FoodRecipeDataEntry[] requiredMonsterTypes;
    [SerializeField] private FoodData foodData;
    
    // Getters
    public FoodRecipeDataEntry[] RequiredMonsterTypes => requiredMonsterTypes;
    public FoodData FoodData => foodData;
    
    // Internal Variables
    private Dictionary<MonsterType, int> requiredMonsterTypeCounts;

    #region Initialization

    private void OnEnable()
    {
        InitializeMonsterTypeCounts();
    }
    
    void InitializeMonsterTypeCounts()
    {
        // Convert the array to a dictionary
        requiredMonsterTypeCounts = requiredMonsterTypes
            .GroupBy(sm => sm.monsterType)
            .ToDictionary(group => group.Key, group => group.Count());
    }

    #endregion

    #region Recipe Implementation

    public override bool Validate(CookState cookState)
    {
        Dictionary<MonsterType, int> cookStateMonsterTypeCounts = cookState.SlainMonsters
            .GroupBy(sm => sm.monsterType)
            .ToDictionary(group => group.Key, group => group.Count());
        
        // Have all the same keys
        if(cookStateMonsterTypeCounts.Count != requiredMonsterTypeCounts.Count)
            return false;
        
        // Have all the same values
        return requiredMonsterTypeCounts.All(mt => mt.Value == requiredMonsterTypeCounts[mt.Key]);
    }

    public override void Execute()
    {
        // TODO: Implement the cooking logic
        Debug.Log($"Cooking the food recipe! {foodData.dataName}");
        OrderManager.Instance.ProcessFoodServed(foodData);
    }

    #endregion
    
    // Unity Editor data validation
    private void OnValidate()
    {
        // Checks if array is null, or any of the entries 0
        if(requiredMonsterTypes == null || requiredMonsterTypes.Any(rmt => rmt.requiredCount <= 0))
            Debug.LogError("Food Recipe has invalid monster type counts!");
    }
}