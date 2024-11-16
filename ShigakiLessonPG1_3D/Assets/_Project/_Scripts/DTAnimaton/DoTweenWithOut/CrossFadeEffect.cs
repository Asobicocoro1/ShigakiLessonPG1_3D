using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WithoutDOTween
{
    public class CrossFadeEffect : MonoBehaviour
    {
        public CanvasGroup currentUI;
        public CanvasGroup nextUI;
        public float fadeDuration = 0.5f;

        public void CrossFade()
        {
            StartCoroutine(FadeOutIn());
        }

        IEnumerator FadeOutIn()
        {
            float elapsedTime = 0f;

            while (elapsedTime < fadeDuration)
            {
                currentUI.alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
                nextUI.alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            currentUI.alpha = 0;
            nextUI.alpha = 1;
        }
    }
}
