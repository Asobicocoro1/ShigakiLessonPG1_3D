using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WithoutDOTween
{
    public class RippleAppearEffect : MonoBehaviour
    {
        public RectTransform ripple;
        public float rippleDuration = 0.8f;

        void Start()
        {
            StartCoroutine(RippleEffect());
        }

        IEnumerator RippleEffect()
        {
            Vector3 startScale = Vector3.zero;
            Vector3 peakScale = new Vector3(3f, 3f, 1f);
            float elapsedTime = 0f;

            while (elapsedTime < rippleDuration)
            {
                ripple.localScale = Vector3.Lerp(startScale, peakScale, elapsedTime / rippleDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            elapsedTime = 0f;
            while (elapsedTime < rippleDuration)
            {
                ripple.localScale = Vector3.Lerp(peakScale, startScale, elapsedTime / rippleDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            ripple.localScale = startScale;
        }
    }
}
