using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    // 現在のステージをリトライ
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // メインメニューに戻る
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
