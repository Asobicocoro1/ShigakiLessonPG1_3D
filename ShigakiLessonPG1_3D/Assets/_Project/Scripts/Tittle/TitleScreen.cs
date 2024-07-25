using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    public void OnStartGameButtonPressed()
    {
        GameManager.instance.StartGame();
    }

    public void OnQuitButtonPressed()
    {
        Application.Quit();
    }
}

