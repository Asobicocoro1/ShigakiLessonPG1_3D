using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace WithoutDOTween
{
    public class GradientTextEffect : MonoBehaviour
    {
        public TMP_Text text;
        public Color startColor = Color.red;
        public Color endColor = Color.blue;
        public float duration = 2f;

        void Start()
        {
            StartCoroutine(AnimateTextColor());
        }

        IEnumerator AnimateTextColor()
        {
            while (true)
            {
                float elapsedTime = 0f;

                while (elapsedTime < duration)
                {
                    text.color = Color.Lerp(startColor, endColor, elapsedTime / duration);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                // F‚ð‹t“]‚µ‚ÄÄ‚Ñ•Ï‰»
                Color temp = startColor;
                startColor = endColor;
                endColor = temp;
                elapsedTime = 0f;
            }
        }
    }
}
