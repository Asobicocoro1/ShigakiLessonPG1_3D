using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace WithDOTween
{
    public class SequenceEffect : MonoBehaviour
    {
        public CanvasGroup canvasGroup;
        public RectTransform rectTransform;

        void Start()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(canvasGroup.DOFade(1f, 0.5f))
                    .Join(rectTransform.DOScale(new Vector3(1.2f, 1.2f, 1f), 0.5f))
                    .Append(rectTransform.DOScale(Vector3.one, 0.2f));
        }
    }
}

