using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace WithDOTween
{
    public class RotateIndicatorEffect : MonoBehaviour
    {
        public RectTransform indicator;
        public float rotateDuration = 1f;

        void Start()
        {
            indicator.DORotate(new Vector3(0, 0, -360), rotateDuration, RotateMode.FastBeyond360)
                     .SetEase(Ease.Linear)
                     .SetLoops(-1);
        }
    }
}

