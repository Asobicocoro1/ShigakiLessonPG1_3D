using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace WithDOTween
{
    public class TimelineEffect : MonoBehaviour
    {
        public CanvasGroup[] uiElements;
        public float interval = 0.5f;

        void Start()
        {
            Sequence sequence = DOTween.Sequence();
            foreach (var element in uiElements)
            {
                element.alpha = 0;
                sequence.Append(element.DOFade(1f, 0.5f).SetEase(Ease.InOutQuad))
                        .AppendInterval(interval);
            }
        }
    }
}

