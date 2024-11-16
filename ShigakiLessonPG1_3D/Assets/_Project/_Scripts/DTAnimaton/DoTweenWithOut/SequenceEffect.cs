using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WithoutDOTween
{
    public class SequenceEffect : MonoBehaviour
    {
        public CanvasGroup canvasGroup;
        public RectTransform rectTransform;

        void Start()
        {
            StartCoroutine(PlaySequence());
        }

        IEnumerator PlaySequence()
        {
            yield return StartCoroutine(FadeIn());
            yield return StartCoroutine(ScaleUp());
            yield return StartCoroutine(ScaleDown());
        }

        IEnumerator FadeIn()
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

        IEnumerator ScaleUp()
        {
            Vector3 startScale = new Vector3(1f, 1f, 1f);
            Vector3 endScale = new Vector3(1.2f, 1.2f, 1f);
            float scaleDuration = 0.5f;
            float elapsedTime = 0f;

            while (elapsedTime < scaleDuration)
            {
                rectTransform.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / scaleDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            rectTransform.localScale = endScale;
        }

        IEnumerator ScaleDown()
        {
            Vector3 startScale = new Vector3(1.2f, 1.2f, 1f);
            Vector3 endScale = new Vector3(1f, 1f, 1f);
            float scaleDuration = 0.2f;
            float elapsedTime = 0f;

            while (elapsedTime < scaleDuration)
            {
                rectTransform.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / scaleDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            rectTransform.localScale = endScale;
        }
    }
}

