using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    // ���݂̃X�e�[�W�����g���C
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // ���C�����j���[�ɖ߂�
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
