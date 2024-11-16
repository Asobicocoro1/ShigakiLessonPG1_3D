using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace WithDOTween
{
    public class BounceEffect : MonoBehaviour
    {
        public RectTransform buttonRect;
        public float duration = 0.5f;

        void Start()
        {
            buttonRect.DOScale(new Vector3(1.1f, 1.1f, 1f), duration)
                      .SetEase(Ease.OutBounce)
                      .OnComplete(() => buttonRect.DOScale(Vector3.one, 0.2f));
        }
    }
}

