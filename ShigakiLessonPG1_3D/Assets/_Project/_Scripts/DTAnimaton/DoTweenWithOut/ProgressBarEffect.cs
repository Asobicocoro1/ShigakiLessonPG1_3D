using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WithoutDOTween
{
    public class ProgressBarEffect : MonoBehaviour
    {
        public Slider progressBar;
        public float loadDuration = 2f;

        void Start()
        {
            StartCoroutine(FillProgressBar());
        }

        IEnumerator FillProgressBar()
        {
            float elapsedTime = 0f;

            while (elapsedTime < loadDuration)
            {
                progressBar.value = Mathf.Lerp(0, 1, elapsedTime / loadDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            progressBar.value = 1f;
        }
    }
}

