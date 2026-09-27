using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerVisualizer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;
    
    [SerializeField] private Image livesIcon;
    [SerializeField] private Transform livesContainer;
    
    private void Awake()
    {
        GameManager gameManager = FindAnyObjectByType<GameManager>();
        gameManager.OnScoreChanged += OnScoreChanged;
        gameManager.OnLivesChanged += OnLivesChanged;
    }

    private void Start()
    {
        GameManager gameManager = FindAnyObjectByType<GameManager>();
        
        score.text = 0.ToString();
        SpawnLivesIcon(gameManager.gameState.Lives);
    }

    public void OnScoreChanged(int newScore)
    {
        score.text = newScore.ToString();
    }
    
    public void OnLivesChanged(int newLives)
    {
        SpawnLivesIcon(newLives);
    }
    
    private void SpawnLivesIcon(int lives)
    {
        // Update Lives UI
        for (int i = 0; i < livesContainer.childCount; i++) {
            Transform child = livesContainer.GetChild(i);
            Destroy(child.gameObject);
        }
        
        for (int i = 0; i < lives; i++)
            Instantiate(livesIcon, livesContainer);

    }
}
