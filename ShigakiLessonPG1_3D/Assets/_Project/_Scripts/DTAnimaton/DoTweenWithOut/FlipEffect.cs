using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WithoutDOTween
{
    public class FlipEffect : MonoBehaviour
    {
        public RectTransform card;
        public float flipDuration = 0.5f;

        public void Flip()
        {
            StartCoroutine(FlipCard());
        }

        IEnumerator FlipCard()
        {
            float halfDuration = flipDuration / 2f;
            float elapsedTime = 0f;

            while (elapsedTime < halfDuration)
            {
                float angle = Mathf.Lerp(0, 90, elapsedTime / halfDuration);
                card.rotation = Quaternion.Euler(0, angle, 0);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // 表示内容を切り替え

            elapsedTime = 0f;
            while (elapsedTime < halfDuration)
            {
                float angle = Mathf.Lerp(90, 180, elapsedTime / halfDuration);
                card.rotation = Quaternion.Euler(0, angle, 0);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            card.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}

