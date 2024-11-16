using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WithoutDOTween
{
    public class BlackHoleEffect : MonoBehaviour
    {
        public RectTransform[] uiElements;
        public RectTransform centerPoint;
        public float duration = 1f;

        void Start()
        {
            foreach (var element in uiElements)
            {
                StartCoroutine(ShrinkToCenter(element));
            }
        }

        IEnumerator ShrinkToCenter(RectTransform element)
        {
            Vector2 startPos = element.anchoredPosition;
            Vector2 endPos = centerPoint.anchoredPosition;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                element.anchoredPosition = Vector2.Lerp(startPos, endPos, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            element.anchoredPosition = endPos;

            elapsedTime = 0f;
            while (elapsedTime < 0.5f)
            {
                element.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, elapsedTime / 0.5f);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            element.localScale = Vector3.zero;
        }
    }
}
