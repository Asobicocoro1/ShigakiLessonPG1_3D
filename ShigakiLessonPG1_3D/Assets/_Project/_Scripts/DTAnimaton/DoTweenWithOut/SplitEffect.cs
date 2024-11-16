using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WithoutDOTween
{
    public class SplitEffect : MonoBehaviour
    {
        public RectTransform uiElement;
        public float duration = 0.5f;
        public Vector2 splitDistance = new Vector2(200, 200);

        public void Split()
        {
            for (int i = 0; i < 4; i++)
            {
                Vector2 direction = new Vector2(
                    (i % 2 == 0 ? 1 : -1) * splitDistance.x,
                    (i < 2 ? 1 : -1) * splitDistance.y
                );

                RectTransform clone = Instantiate(uiElement, transform);
                StartCoroutine(MoveAndDestroy(clone, direction));
            }

            StartCoroutine(ShrinkAndDestroy(uiElement));
        }

        IEnumerator MoveAndDestroy(RectTransform element, Vector2 direction)
        {
            Vector2 startPos = element.anchoredPosition;
            Vector2 endPos = startPos + direction;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                element.anchoredPosition = Vector2.Lerp(startPos, endPos, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            Destroy(element.gameObject);
        }

        IEnumerator ShrinkAndDestroy(RectTransform element)
        {
            Vector3 startScale = Vector3.one;
            Vector3 endScale = Vector3.zero;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                element.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            Destroy(element.gameObject);
        }
    }
}

