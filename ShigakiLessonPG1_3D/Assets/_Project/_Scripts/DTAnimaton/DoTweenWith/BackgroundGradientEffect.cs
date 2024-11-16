using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace WithDOTween
{
    public class BackgroundGradientEffect : MonoBehaviour
    {
        public Camera mainCamera;
        public Color startColor;
        public Color endColor;
        public float gradientDuration = 5f;

        void Start()
        {
            mainCamera.DOColor(endColor, gradientDuration)
                      .SetLoops(-1, LoopType.Yoyo);
        }
    }
}

