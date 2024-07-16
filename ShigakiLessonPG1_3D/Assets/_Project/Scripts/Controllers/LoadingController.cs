using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadingController : MonoBehaviour
{
    public Slider progressBar;

    void Start()
    {
        string nextSceneName = GameManager.instance.nextScene;
        StartCoroutine(LoadAsync(nextSceneName));
    }

    IEnumerator LoadAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            progressBar.value = operation.progress;
            yield return null;
        }
    }
}

