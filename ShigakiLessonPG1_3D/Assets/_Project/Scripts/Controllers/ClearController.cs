using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearController : MonoBehaviour
{
    public string nextLevelName = "NextGamePlay"; // 次のステージのシーン名

    // 次のステージをロード
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevelName);
    }

    // メインメニューに戻る
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

