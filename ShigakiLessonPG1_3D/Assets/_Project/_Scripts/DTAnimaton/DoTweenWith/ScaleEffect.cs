using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace WithDOTween
{
    public class ScaleEffect : MonoBehaviour
    {
        public RectTransform rectTransform;
        public float scaleDuration = 0.1f;

        void Start()
        {
            rectTransform.DOScale(new Vector3(1.2f, 1.2f, 1f), scaleDuration)
                         .SetEase(Ease.OutBack)
                         .OnComplete(() => rectTransform.DOScale(Vector3.one, scaleDuration));
        }
    }
}

