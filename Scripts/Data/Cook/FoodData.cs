using UnityEngine;

[CreateAssetMenu(fileName = "NewFoodData", menuName = "Cooking/FoodData")]
public class FoodData : GameScriptableObject
{
    public int FoodID { get; private set; }
    
    public void SetFoodID(int id)
    {
        FoodID = id;
    }
}