using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WithoutDOTween
{
    public class ColorChangeEffect : MonoBehaviour
    {
        public Image buttonImage;
        public float colorDuration = 0.3f;

        void Start()
        {
            StartCoroutine(ChangeColor());
        }

        IEnumerator ChangeColor()
        {
            Color startColor = buttonImage.color;
            Color endColor = Color.green;
            float elapsedTime = 0f;

            while (elapsedTime < colorDuration)
            {
                buttonImage.color = Color.Lerp(startColor, endColor, elapsedTime / colorDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            buttonImage.color = endColor;
        }
    }
}

