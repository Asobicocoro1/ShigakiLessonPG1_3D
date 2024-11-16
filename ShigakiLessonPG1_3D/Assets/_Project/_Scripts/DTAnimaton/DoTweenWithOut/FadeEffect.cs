using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WithoutDOTween
{
    public class FadeEffect : MonoBehaviour
    {
        public CanvasGroup canvasGroup;
        public float fadeDuration = 0.5f;

        void Start()
        {
            StartCoroutine(FadeIn());
        }

        IEnumerator FadeIn()
        {
            float elapsedTime = 0f;
            while (elapsedTime < fadeDuration)
            {
                canvasGroup.alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            canvasGroup.alpha = 1;
        }
    }
}

