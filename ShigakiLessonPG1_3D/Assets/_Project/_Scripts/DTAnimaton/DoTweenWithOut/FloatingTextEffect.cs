using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace WithoutDOTween
{
    public class FloatingTextEffect : MonoBehaviour
    {
        public TMP_Text floatingText;
        public float moveDistance = 50f;
        public float fadeDuration = 1f;

        void Start()
        {
            StartCoroutine(FloatAndFade());
        }

        IEnumerator FloatAndFade()
        {
            Vector2 startPos = floatingText.rectTransform.anchoredPosition;
            Vector2 targetPos = startPos + new Vector2(0, moveDistance);
            float elapsedTime = 0f;

            while (elapsedTime < fadeDuration)
            {
                floatingText.rectTransform.anchoredPosition = Vector2.Lerp(startPos, targetPos, elapsedTime / fadeDuration);
                floatingText.alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            Destroy(gameObject);
        }
    }
}

