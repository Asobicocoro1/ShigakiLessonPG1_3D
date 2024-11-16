using System.Collections;
using System.Collections.Generic;
using WithDOTween;
using UnityEngine;

namespace WithoutDOTween
{
    public class BackgroundGradientEffect : MonoBehaviour
    {
        public Camera mainCamera;
        public Color startColor;
        public Color endColor;
        public float gradientDuration = 5f;

        void Start()
        {
            StartCoroutine(GradientEffect());
        }

        IEnumerator GradientEffect()
        {
            float elapsedTime = 0f;
            bool reverse = false;

            while (true)
            {
                while (elapsedTime < gradientDuration)
                {
                    mainCamera.backgroundColor = Color.Lerp(reverse ? endColor : startColor, reverse ? startColor : endColor, elapsedTime / gradientDuration);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                elapsedTime = 0f;
                reverse = !reverse;
            }
        }
    }
}

