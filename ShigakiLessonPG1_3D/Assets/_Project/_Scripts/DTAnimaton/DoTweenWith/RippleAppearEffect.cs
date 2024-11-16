using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace WithDOTween
{
    public class RippleAppearEffect : MonoBehaviour
    {
        public RectTransform ripple;
        public float rippleDuration = 0.8f;

        void Start()
        {
            ripple.localScale = Vector3.zero;
            ripple.DOScale(new Vector3(3f, 3f, 1f), rippleDuration)
                  .SetEase(Ease.OutQuad)
                  .OnComplete(() => ripple.DOScale(Vector3.zero, rippleDuration).SetEase(Ease.InQuad));
        }
    }
}

