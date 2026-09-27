using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private int maxOrders = 3;
    
    [Header("Attribute Range")]
    [SerializeField] private Vector2 timeToServeRange = new Vector2(15f, 30f);
    [SerializeField] private Vector2 rewardRange = new Vector2(10, 20);
    [SerializeField] private Vector2 penaltyRange = new Vector2(5, 10);
    
    [SerializeField] private Vector2 timeBetweenOrdersRange = new Vector2(5, 15f);
    
    // Getters
    public bool isMaxOrdersReached => orders.Count >= maxOrders;
    
    // Events
    public event Action<OrderData> OnOrderAdded;
    public event Action<OrderData, bool> OnOrderCompleted;
    
    // Internal References
    public List<OrderData> orders { get; private set; }
    
    // Internal Variables
    private Coroutine generateOrdersRoutine;
    private Queue<OrderData> markedForRemoval;

    public static OrderManager Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this;
        
        // Initialize
        orders = new List<OrderData>();
        markedForRemoval = new Queue<OrderData>();
    }

    private void Start()
    {
        AddOrder();
        TriggerGenerateOrdersRoutine();
    }

    private void Update()
    {
        ProcessTimeStep(Time.deltaTime);
    }

    // Coroutine logic
    private void TriggerGenerateOrdersRoutine()
    {
        if (generateOrdersRoutine != null)
            StopCoroutine(generateOrdersRoutine);
        
        generateOrdersRoutine = StartCoroutine(GenerateOrdersRoutine());
    }

    private IEnumerator GenerateOrdersRoutine()
    {
        while (true)
        {
            float timeBetweenOrders = Random.Range(timeBetweenOrdersRange.x, timeBetweenOrdersRange.y);
            yield return new WaitForSeconds(timeBetweenOrders);
            
            if (!isMaxOrdersReached)
                AddOrder();
        }
    }

    // Helpers
    private OrderData GenerateOrder()
    {
        // Generate Random Order
        DataManager dataManager = DataManager.Instance;
        FoodData foodData = dataManager.foodDataList[Random.Range(0, dataManager.foodDataList.Length)];
        
        // T value
        float t;
        
        // Generate Random Values
        t = Random.value;
        float timeToServe = Mathf.Lerp(timeToServeRange.x, timeToServeRange.y, t);
        
        t = Random.value;
        int reward = Mathf.RoundToInt(Mathf.Lerp(rewardRange.x, rewardRange.y, t));
        
        t = Random.value;
        int penalty = Mathf.RoundToInt(Mathf.Lerp(penaltyRange.x, penaltyRange.y, t));
        
        // Create Order
        OrderData order = new OrderData(foodData, timeToServe, reward, penalty);
        
        return order;
    }
    
    private void ProcessTimeStep(float deltaTime)
    {
        foreach (OrderData order in orders)
        {
            order.UpdateTimeElapsed(deltaTime);
            if (order.IsOrderExpired())
                CompleteOrder(order, false);
        }
        
        while (markedForRemoval.Count > 0)
            orders.Remove(markedForRemoval.Dequeue());
    }
    
    public void ProcessFoodServed(FoodData food)
    {
        foreach (OrderData order in orders)
        {
            if (order.IsOrderServed(food))
            {
                CompleteOrder(order, true);
                break;
            }
        }
    }
    
    // Events
    public void AddOrder()
    {
        if (isMaxOrdersReached)
            return;
        
        OrderData order = GenerateOrder();
        orders.Add(order);
        
        OnOrderAdded?.Invoke(order);
    }
    
    public void CompleteOrder(OrderData order, bool isServed)
    {
        if (!orders.Contains(order))
            return;
        
        Debug.Log($"Order Server: {order.requestedFood.dataName} - Served: {isServed}");
        markedForRemoval.Enqueue(order);
        
        OnOrderCompleted?.Invoke(order, isServed);
    }
}
