using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WithoutDOTween
{
    public class BounceEffect : MonoBehaviour
    {
        public RectTransform buttonRect;
        public float duration = 0.5f;

        void Start()
        {
            StartCoroutine(Bounce());
        }

        IEnumerator Bounce()
        {
            Vector3 startScale = Vector3.one;
            Vector3 peakScale = new Vector3(1.1f, 1.1f, 1f);
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                buttonRect.localScale = Vector3.Lerp(startScale, peakScale, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            buttonRect.localScale = startScale;
        }
    }
}

