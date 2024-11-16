using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WithoutDOTween
{
    public class WeatherUIEffect : MonoBehaviour
    {
        public Image backgroundUI;
        public Color dayColor;
        public Color nightColor;
        public float transitionDuration = 2f;

        public void ChangeToDay()
        {
            StartCoroutine(ChangeColor(dayColor));
        }

        public void ChangeToNight()
        {
            StartCoroutine(ChangeColor(nightColor));
        }

        IEnumerator ChangeColor(Color targetColor)
        {
            Color startColor = backgroundUI.color;
            float elapsedTime = 0f;

            while (elapsedTime < transitionDuration)
            {
                backgroundUI.color = Color.Lerp(startColor, targetColor, elapsedTime / transitionDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            backgroundUI.color = targetColor;
        }
    }
}

