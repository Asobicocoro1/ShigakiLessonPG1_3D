using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WithoutDOTween
{
    public class RadialMenuEffect : MonoBehaviour
    {
        public RectTransform[] menuItems;
        public float radius = 100f;
        public float duration = 0.5f;

        void Start()
        {
            for (int i = 0; i < menuItems.Length; i++)
            {
                float angle = i * (360f / menuItems.Length);
                Vector2 targetPos = new Vector2(
                    Mathf.Cos(angle * Mathf.Deg2Rad) * radius,
                    Mathf.Sin(angle * Mathf.Deg2Rad) * radius
                );

                StartCoroutine(MoveToPosition(menuItems[i], targetPos));
            }
        }

        IEnumerator MoveToPosition(RectTransform item, Vector2 targetPos)
        {
            Vector2 startPos = item.anchoredPosition;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                item.anchoredPosition = Vector2.Lerp(startPos, targetPos, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            item.anchoredPosition = targetPos;
        }
    }
}
 
