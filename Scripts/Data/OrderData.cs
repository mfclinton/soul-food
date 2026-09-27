using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class OrderData
{
    // Properties
    public FoodData requestedFood { get; private set; }
    public float timeToServe  { get; private set; }
    public int reward  { get; private set; }
    public int penalty { get; private set; }
    
    // Events
    public event Action<OrderData> OnStateChanged;
    
    // Public Variables
    public float timeElapsed { get; private set; }
    public float timeRemaining => timeToServe - timeElapsed;
    
    public OrderData(FoodData requestedFood, float timeToServe, int reward, int penalty)
    {
        this.requestedFood = requestedFood;
        this.timeToServe = timeToServe;
        this.reward = reward;
        this.penalty = penalty;
    }

    #region Public Functions

    public void UpdateTimeElapsed(float time)
    {
        timeElapsed += time;
        OnStateChanged?.Invoke(this);
    }
    
    public bool IsOrderExpired()
    {
        return timeElapsed >= timeToServe;
    }
    
    public bool IsOrderServed(FoodData food)
    {
        return requestedFood == food;
    }

    #endregion
}
