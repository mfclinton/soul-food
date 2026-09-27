using System;
using System.Linq;
using UnityEngine;

public class RecipeController : MonoBehaviour
{
    // Events
    public event Action<Recipe> OnRecipeExecuted;
    
    private void Awake()
    {
        CookController cookController = FindAnyObjectByType<CookController>();
        cookController.OnCookExecuted += OnCookExecuted;
    }
    
    private void OnCookExecuted(CookState cookState)
    {
        Recipe[] validRecipes = DataManager.Instance.recipesList.Where(recipe => recipe.Validate(cookState)).ToArray();
        if (validRecipes == null || validRecipes.Length == 0)
            return;
        
        // Filter for Recipe That Solves a Order
        FoodRecipe[] validFoodRecipes = validRecipes.Where(x => x is FoodRecipe).Select(x => (FoodRecipe)x).ToArray();
        
        FoodRecipe[] validOrderRecipes = validFoodRecipes
            .Where(foodRecipe => OrderManager.Instance.orders.Any(order => order.requestedFood == foodRecipe.FoodData))
            .Select(foodRecipe => new {
                Recipe = foodRecipe,
                MinTime = OrderManager.Instance.orders
                    .Where(order => order.requestedFood == foodRecipe.FoodData)
                    .Min(order => order.timeRemaining)
            })
            .OrderBy(foodRecipeWithTime => foodRecipeWithTime.MinTime)
            .Select(foodRecipeWithTime => foodRecipeWithTime.Recipe)
            .ToArray();
        
        // Get the Best Recipe
        Recipe bestRecipe = validRecipes.FirstOrDefault();
        if (validOrderRecipes.Length > 0)
            bestRecipe = validOrderRecipes[0];
        
        bestRecipe.Execute();
        OnRecipeExecuted?.Invoke(bestRecipe);
    }
}
