using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OrderManager))]
public class OrderManagerVisualizer : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private OrderVisualizer orderVisualizerPrefab;
    [SerializeField] private UISmoothList orderList;
    
    // Internal References
    private OrderManager orderManager;
    
    // Internal Variables
    private Dictionary<OrderData, OrderVisualizer> orderVisualizers;
    
    private void Awake()
    {
        orderManager = GetComponent<OrderManager>();
        orderVisualizers = new Dictionary<OrderData, OrderVisualizer>();
        
        // Events
        orderManager.OnOrderAdded += OnOrderAdded;
        orderManager.OnOrderCompleted += OnOrderCompleted;
    }

    private void OnOrderCompleted(OrderData order, bool wasServed)
    {
        if (orderVisualizers.ContainsKey(order))
        {
            OrderVisualizer orderVisualizer = orderVisualizers[order];
            orderList.Remove(orderVisualizer.gameObject);
            orderVisualizers.Remove(order);
        }
        else
        {
            Debug.LogError("OrderVisualizer not found for order: " + order);
        }
    }

    private void OnOrderAdded(OrderData order)
    {
        GameObject newElement = orderList.Add(orderVisualizerPrefab.gameObject);
        
        OrderVisualizer orderVisualizer = newElement.GetComponent<OrderVisualizer>();
        orderVisualizer.Initialize(order);
        
        orderVisualizers.Add(order, orderVisualizer);
    }
}
