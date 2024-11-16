using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace WithDOTween
{
    public class ColorChangeEffect : MonoBehaviour
    {
        public Image buttonImage;
        public float colorDuration = 0.3f;

        void Start()
        {
            buttonImage.DOColor(Color.green, colorDuration);
        }
    }
}
