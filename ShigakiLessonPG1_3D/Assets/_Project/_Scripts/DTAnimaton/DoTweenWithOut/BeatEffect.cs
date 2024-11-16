using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WithoutDOTween
{
    public class BeatEffect : MonoBehaviour
    {
        public RectTransform uiElement;
        public float beatDuration = 0.5f;

        void Start()
        {
            StartCoroutine(Beat());
        }

        IEnumerator Beat()
        {
            Vector3 originalScale = Vector3.one;
            Vector3 targetScale = new Vector3(1.2f, 1.2f, 1f);

            while (true)
            {
                float elapsedTime = 0f;

                while (elapsedTime < beatDuration)
                {
                    uiElement.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / beatDuration);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                elapsedTime = 0f;

                while (elapsedTime < beatDuration)
                {
                    uiElement.localScale = Vector3.Lerp(targetScale, originalScale, elapsedTime / beatDuration);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }
            }
        }
    }
}

