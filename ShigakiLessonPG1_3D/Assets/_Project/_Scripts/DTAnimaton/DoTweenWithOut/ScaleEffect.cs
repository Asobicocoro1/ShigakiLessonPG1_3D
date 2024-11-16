using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WithoutDOTween
{
    public class ScaleEffect : MonoBehaviour
    {
        public RectTransform rectTransform;
        public float scaleDuration = 0.1f;

        void Start()
        {
            StartCoroutine(Scale());
        }

        IEnumerator Scale()
        {
            Vector3 startScale = Vector3.one;
            Vector3 endScale = new Vector3(1.2f, 1.2f, 1f);
            float elapsedTime = 0f;

            while (elapsedTime < scaleDuration)
            {
                rectTransform.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / scaleDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            rectTransform.localScale = endScale;

            elapsedTime = 0f;
            while (elapsedTime < scaleDuration)
            {
                rectTransform.localScale = Vector3.Lerp(endScale, startScale, elapsedTime / scaleDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            rectTransform.localScale = startScale;
        }
    }
}

