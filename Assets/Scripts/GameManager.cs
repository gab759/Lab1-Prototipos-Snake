using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonNotPersistent<GameManager>
{
    [SerializeField] private GameObject mealPrefab;
    public Vector2 limits;
    public int maxScore = 999;

    [SerializeField] public ScoreManager scoreManager;
    [SerializeField] private UI_Manager uiManager;
    [SerializeField] private Player player;

    [SerializeField] private GameObject gameOverPanel;
    private bool isGameOver = false;

    private void Start()
    {
        scoreManager.Initialize(uiManager);
        player.Initialize(scoreManager);
        gameOverPanel.SetActive(false);
        GenerateFood();
    }

    public void GenerateFood()
    {
        Vector2 randomPos = new Vector2(
            Mathf.Round(Random.Range(-limits.x, limits.x)),
            Mathf.Round(Random.Range(-limits.y, limits.y))
        );

        Instantiate(mealPrefab, randomPos, Quaternion.identity);
    }

    public void GameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            Debug.Log("Game Over");
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }
}