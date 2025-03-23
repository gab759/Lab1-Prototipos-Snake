using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject mealPrefab;
    public Vector2 limits;
    public int maxScore = 999;

    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private UI_Manager uiManager;

    [SerializeField] private GameObject gameOverPanel;
    private bool isGameOver = false;

    private void Start()
    {
        scoreManager.Initialize(this, uiManager);
        Player.Instance.Initialize(scoreManager, this);
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