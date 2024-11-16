using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WithoutDOTween
{
    public class RandomEffect : MonoBehaviour
    {
        public RectTransform[] uiElements;
        public float duration = 0.5f;
        public Vector2 minPosition = new Vector2(-300, -300);
        public Vector2 maxPosition = new Vector2(300, 300);

        void Start()
        {
            foreach (var element in uiElements)
            {
                Vector2 randomPos = new Vector2(Random.Range(minPosition.x, maxPosition.x), Random.Range(minPosition.y, maxPosition.y));
                StartCoroutine(MoveToRandomPosition(element, randomPos));
            }
        }

        IEnumerator MoveToRandomPosition(RectTransform element, Vector2 targetPosition)
        {
            Vector2 startPos = element.anchoredPosition;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                element.anchoredPosition = Vector2.Lerp(startPos, targetPosition, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            element.anchoredPosition = targetPosition;
        }
    }
}

