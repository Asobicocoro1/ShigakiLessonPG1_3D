using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WithoutDOTween
{
    public class ZoomEffect : MonoBehaviour
    {
        public RectTransform targetUI;
        public float zoomDuration = 0.5f;

        void Start()
        {
            StartCoroutine(Zoom());
        }

        IEnumerator Zoom()
        {
            Vector3 originalScale = Vector3.one;
            Vector3 targetScale = new Vector3(1.5f, 1.5f, 1f);
            float elapsedTime = 0f;

            while (elapsedTime < zoomDuration)
            {
                targetUI.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / zoomDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            elapsedTime = 0f;
            while (elapsedTime < zoomDuration)
            {
                targetUI.localScale = Vector3.Lerp(targetScale, originalScale, elapsedTime / zoomDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            targetUI.localScale = originalScale;
        }
    }
}

