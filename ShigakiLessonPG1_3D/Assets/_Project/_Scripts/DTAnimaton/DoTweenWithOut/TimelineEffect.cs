using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WithoutDOTween
{
    public class TimelineEffect : MonoBehaviour
    {
        public CanvasGroup[] uiElements;
        public float interval = 0.5f;

        void Start()
        {
            StartCoroutine(ShowElements());
        }

        IEnumerator ShowElements()
        {
            foreach (var element in uiElements)
            {
                yield return StartCoroutine(FadeIn(element));
                yield return new WaitForSeconds(interval);
            }
        }

        IEnumerator FadeIn(CanvasGroup canvasGroup)
        {
            float fadeDuration = 0.5f;
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

