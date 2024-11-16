using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WithoutDOTween
{
    public class ShakeEffect : MonoBehaviour
    {
        public RectTransform errorMessage;
        public float duration = 0.5f;

        void Start()
        {
            StartCoroutine(Shake());
        }

        IEnumerator Shake()
        {
            Vector3 originalPos = errorMessage.anchoredPosition;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                float xOffset = Random.Range(-10f, 10f);
                errorMessage.anchoredPosition = originalPos + new Vector3(xOffset, 0, 0);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            errorMessage.anchoredPosition = originalPos;
        }
    }
}
