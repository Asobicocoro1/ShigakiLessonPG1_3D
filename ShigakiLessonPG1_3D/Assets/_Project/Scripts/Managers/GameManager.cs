using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public string nextScene;

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

    public void LoadScene(string loadingScene, string targetScene)
    {
        nextScene = targetScene;
        SceneManager.LoadScene(loadingScene);
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void GameClear()
    {
        SceneManager.LoadScene("Clear");
    }
}

