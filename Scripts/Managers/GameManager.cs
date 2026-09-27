using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] private int startingLives = 3;
    
    // State
    public GameState gameState { get; private set; }
    
    // Events
    public Action<int> OnScoreChanged;
    public Action<int> OnLivesChanged;
    
    // Internal References
    private StageController stageController;
    
    private void Awake()
    {
        stageController = FindAnyObjectByType<StageController>();

        // Events
        OrderManager orderManager = FindAnyObjectByType<OrderManager>();
        orderManager.OnOrderCompleted += OnOrderCompleted;
        
        InitializeGame();
    }

    private void OnOrderCompleted(OrderData order, bool isServed)
    {
        if (isServed)
        {
            // Add Score
            gameState.AddScore(order.reward);
        }
        else
        {
            // Deduct Score
            gameState.AddScore(-order.penalty);
            gameState.AddLives(-1);
            OnLivesChanged?.Invoke(gameState.Lives);
        }
        
        OnScoreChanged?.Invoke(gameState.Score);
        
        if (gameState.Lives <= 0)
        {
            GameOver();
        }
    }

    private void InitializeGame()
    {
        gameState = new GameState(startingLives);
    }
    
    private void GameOver()
    {
        // Set Score and High Score
        PlayerPrefs.SetInt("Score", gameState.Score);
        int prefHighScore = PlayerPrefs.GetInt("BestScore", 0);
        if(gameState.Score > prefHighScore)
            PlayerPrefs.SetInt("BestScore", gameState.Score);
        
        // Show Game Over Screen
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
