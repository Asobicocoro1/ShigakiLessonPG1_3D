using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Text scoreText;
    public Slider healthBar;
    public GameObject gameOverScreen;
    public GameObject titleScreen;
    public GameObject hud;

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

    public void UpdateScoreText(int score)
    {
        scoreText.text = "Score: " + score;
    }

    public void UpdateHealthBar(int health)
    {
        healthBar.value = health;
    }

    public void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true);
    }

    public void HideGameOverScreen()
    {
        gameOverScreen.SetActive(false);
    }

    public void ShowTitleScreen()
    {
        titleScreen.SetActive(true);
        hud.SetActive(false);
    }

    public void HideTitleScreen()
    {
        titleScreen.SetActive(false);
        hud.SetActive(true);
    }

    public void ShowHUD()
    {
        hud.SetActive(true);
    }

    public void HideHUD()
    {
        hud.SetActive(false);
    }
}


