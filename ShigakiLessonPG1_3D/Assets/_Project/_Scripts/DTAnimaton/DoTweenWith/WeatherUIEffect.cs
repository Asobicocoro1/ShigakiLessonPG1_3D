using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace WithDOTween
{
    public class WeatherUIEffect : MonoBehaviour
    {
        public Image backgroundUI;
        public Color dayColor;
        public Color nightColor;
        public float transitionDuration = 2f;

        public void ChangeToDay()
        {
            backgroundUI.DOColor(dayColor, transitionDuration);
        }

        public void ChangeToNight()
        {
            backgroundUI.DOColor(nightColor, transitionDuration);
        }
    }
}

