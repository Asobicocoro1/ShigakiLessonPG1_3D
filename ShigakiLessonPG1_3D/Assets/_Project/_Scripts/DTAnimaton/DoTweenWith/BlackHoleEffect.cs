using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace WithDOTween
{
    public class BlackHoleEffect : MonoBehaviour
    {
        public RectTransform[] uiElements;
        public RectTransform centerPoint;
        public float duration = 1f;

        void Start()
        {
            foreach (var element in uiElements)
            {
                element.DOAnchorPos(centerPoint.anchoredPosition, duration)
                       .SetEase(Ease.InBack)
                       .OnComplete(() => element.DOScale(Vector3.zero, 0.5f));
            }
        }
    }
}
