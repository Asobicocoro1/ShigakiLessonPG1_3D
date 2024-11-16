using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace WithDOTween
{
    public class ZoomEffect : MonoBehaviour
    {
        public RectTransform targetUI;
        public float zoomDuration = 0.5f;

        void Start()
        {
            targetUI.DOScale(new Vector3(1.5f, 1.5f, 1f), zoomDuration)
                    .SetEase(Ease.OutBack)
                    .OnComplete(() => targetUI.DOScale(Vector3.one, zoomDuration));
        }
    }
}

