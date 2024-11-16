using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace WithDOTween
{
    public class RippleEffect : MonoBehaviour
    {
        public RectTransform ripple;
        public float rippleDuration = 1f;

        void Start()
        {
            ripple.localScale = Vector3.zero;
            ripple.DOScale(new Vector3(3f, 3f, 1f), rippleDuration)
                  .SetEase(Ease.OutQuad)
                  .OnComplete(() => ripple.DOScale(Vector3.zero, 0));
        }
    }
}
