using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WithoutDOTween
{
    public class AutoHideEffect : MonoBehaviour
    {
        public CanvasGroup notification;
        public float displayDuration = 2f;
        public float fadeDuration = 0.5f;

        void Start()
        {
            StartCoroutine(AutoHide());
        }

        IEnumerator AutoHide()
        {
            yield return new WaitForSeconds(displayDuration);

            float elapsedTime = 0f;

            while (elapsedTime < fadeDuration)
            {
                notification.alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            notification.alpha = 0;
        }
    }
}

