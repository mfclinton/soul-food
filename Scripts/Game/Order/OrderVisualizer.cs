using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class OrderVisualizer : MonoBehaviour
{
    [SerializeField] private Image foodImage;
    [SerializeField] private Image monsterImagePrefab;
    [SerializeField] private Transform monsterImagesParent;
    [SerializeField] private Image timeImageFill;

    private void Awake()
    {
        // Delete All Monster Image Children
        foreach(Transform child in monsterImagesParent)
            Destroy(child.gameObject);
    }

    public void Initialize(OrderData orderData)
    {
        FoodData requestedFood = orderData.requestedFood;

        LinkedList<FoodRecipe> foodRecipes = DataManager.Instance.foodRecipeDictionary[requestedFood];
        if (foodRecipes.Count != 1)
            throw new Exception("Multiple recipes found for food: " + requestedFood.name);
        
        // Sets Food Image
        foodImage.sprite = requestedFood.icon;
        
        // Sets Monster Images
        FoodRecipe recipe = foodRecipes.First.Value;
        foreach(FoodRecipeDataEntry entry in recipe.RequiredMonsterTypes)
        {
            MonsterData monsterData = DataManager.Instance.monsterDataDictionary[entry.monsterType];
            Sprite monsterSprite = monsterData.icon;

            for (int i = 0; i < entry.requiredCount; i++)
            {
                Image monsterImage = Instantiate(monsterImagePrefab, monsterImagesParent);
                monsterImage.sprite = monsterSprite;
            }
        }
        
        orderData.OnStateChanged += UpdateFill;
    }
    
    private void UpdateFill(OrderData orderData)
    {
        timeImageFill.fillAmount = 1 - (orderData.timeElapsed / orderData.timeToServe);
    }
}
