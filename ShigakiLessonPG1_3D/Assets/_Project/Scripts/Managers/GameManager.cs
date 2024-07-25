using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score = 0;
    public int playerHealth = 100;
    private bool isGameOver = false; // フィールドはプライベートのまま
    public bool IsGameOver // 読み取り専用のプロパティを追加
    {
        get { return isGameOver; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UIManager.instance.ShowTitleScreen();
    }

    private void Update()
    {
        if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    public void AddScore(int points)
    {
        score += points;
        UIManager.instance.UpdateScoreText(score);
    }

    public void ReduceHealth(int damage)
    {
        playerHealth -= damage;
        UIManager.instance.UpdateHealthBar(playerHealth);
        if (playerHealth <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        isGameOver = true;
        UIManager.instance.ShowGameOverScreen();
    }

    public void StartGame()
    {
        isGameOver = false;
        score = 0;
        playerHealth = 100;
        UIManager.instance.HideTitleScreen();
        UIManager.instance.UpdateScoreText(score);
        UIManager.instance.UpdateHealthBar(playerHealth);
    }

    public void RestartGame()
    {
        isGameOver = false;
        score = 0;
        playerHealth = 100;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // シーンのリロード
        UIManager.instance.HideGameOverScreen();
        UIManager.instance.UpdateScoreText(score);
        UIManager.instance.UpdateHealthBar(playerHealth);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}


