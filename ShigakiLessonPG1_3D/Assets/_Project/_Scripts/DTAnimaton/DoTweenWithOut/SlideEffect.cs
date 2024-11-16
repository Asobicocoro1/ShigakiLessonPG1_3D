using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WithoutDOTween
{
    public class SlideEffect : MonoBehaviour
    {
        public RectTransform rectTransform;
        public Vector2 startPos = new Vector2(-500, 0);
        public Vector2 endPos = Vector2.zero;
        public float slideDuration = 0.5f;

        void Start()
        {
            StartCoroutine(SlideIn());
        }

        IEnumerator SlideIn()
        {
            float elapsedTime = 0f;
            while (elapsedTime < slideDuration)
            {
                rectTransform.anchoredPosition = Vector2.Lerp(startPos, endPos, elapsedTime / slideDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            rectTransform.anchoredPosition = endPos;
        }
    }
}

