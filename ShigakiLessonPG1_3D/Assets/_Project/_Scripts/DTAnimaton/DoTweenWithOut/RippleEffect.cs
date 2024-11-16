using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WithoutDOTween
{
    public class RippleEffect : MonoBehaviour
    {
        public RectTransform ripple;
        public float rippleDuration = 1f;

        void Start()
        {
            StartCoroutine(PlayRipple());
        }

        IEnumerator PlayRipple()
        {
            Vector3 startScale = Vector3.zero;
            Vector3 endScale = new Vector3(3f, 3f, 1f);
            float elapsedTime = 0f;

            while (elapsedTime < rippleDuration)
            {
                ripple.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / rippleDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            ripple.localScale = startScale;
        }
    }
}
