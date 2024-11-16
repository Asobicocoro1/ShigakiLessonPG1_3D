using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace WithDOTween
{
    public class IndicatorRingEffect : MonoBehaviour
    {
        public RectTransform ring;
        public float rotationSpeed = 1f;

        void Start()
        {
            ring.DORotate(new Vector3(0, 0, -360), rotationSpeed, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1);
        }
    }
}

