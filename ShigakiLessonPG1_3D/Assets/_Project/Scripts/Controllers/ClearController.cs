using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearController : MonoBehaviour
{
    public string nextLevelName = "NextGamePlay"; // ���̃X�e�[�W�̃V�[����

    // ���̃X�e�[�W�����[�h
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevelName);
    }

    // ���C�����j���[�ɖ߂�
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

