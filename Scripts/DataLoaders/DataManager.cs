using System;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    // Processed scriptable object datastructures
    public Dictionary<ComboAction, ComboActionData> comboActionDataDictionary { get; private set; }
    public Spell[] spellsList { get; private set; }
    public Dictionary<MonsterType, MonsterData> monsterDataDictionary { get; private set; }
    public Recipe[] recipesList { get; private set; }
    public FoodData[] foodDataList { get; private set; }
    public Dictionary<FoodData, LinkedList<FoodRecipe>> foodRecipeDictionary { get; private set; }
    
    // Paths
    private const string scriptableObjectsPath = "ScriptableObjects";

    public static DataManager Instance;
    
    private void Awake()
    {
        Instance = this;
        
        LoadData();
    }
    
    void LoadData()
    {
        LoadComboActionData();
        LoadSpellsData();
        LoadMonsterData();
        LoadRecipesData();
        LoadFoodData();
    }

    #region Data Loading Helpers

    private void LoadComboActionData()
    {
        DataLoader<ComboActionData> comboActionDataLoader = new DataLoader<ComboActionData>(scriptableObjectsPath);
        ComboActionData[] comboActionData = comboActionDataLoader.LoadAll();

        comboActionDataDictionary = new Dictionary<ComboAction, ComboActionData>();
        foreach(ComboActionData data in comboActionData)
            comboActionDataDictionary[data.action] = data;
    }
    
    public void LoadSpellsData()
    {
        DataLoader<Spell> spellDataLoader = new DataLoader<Spell>(scriptableObjectsPath);
        spellsList = spellDataLoader.LoadAll();
    }
    
    public void LoadMonsterData()
    {
        DataLoader<MonsterData> monsterDataLoader = new DataLoader<MonsterData>(scriptableObjectsPath);
        MonsterData[] monsterData = monsterDataLoader.LoadAll();

        monsterDataDictionary = new Dictionary<MonsterType, MonsterData>();
        foreach(MonsterData data in monsterData)
            monsterDataDictionary[data.monsterType] = data;
    }
    
    public void LoadRecipesData()
    {
        DataLoader<Recipe> recipeDataLoader = new DataLoader<Recipe>(scriptableObjectsPath);
        recipesList = recipeDataLoader.LoadAll();
        
        foodRecipeDictionary = new Dictionary<FoodData, LinkedList<FoodRecipe>>();
        foreach(Recipe recipe in recipesList)
        {
            if(recipe is FoodRecipe foodRecipe)
            {
                FoodData foodData = foodRecipe.FoodData;
                if(!foodRecipeDictionary.ContainsKey(foodData))
                    foodRecipeDictionary[foodData] = new LinkedList<FoodRecipe>();
                
                foodRecipeDictionary[foodData].AddLast(foodRecipe);
            }
        }
    }
    
    public void LoadFoodData()
    {
        DataLoader<FoodData> foodDataLoader = new DataLoader<FoodData>(scriptableObjectsPath);
        foodDataList = foodDataLoader.LoadAll();
        for (int id = 0; id < foodDataList.Length; id++)
            foodDataList[id].SetFoodID(id);
    }

    #endregion
}
