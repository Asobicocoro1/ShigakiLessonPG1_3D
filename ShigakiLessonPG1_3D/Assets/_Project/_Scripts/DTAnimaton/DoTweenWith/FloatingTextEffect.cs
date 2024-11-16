using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

namespace WithDOTween
{
    public class FloatingTextEffect : MonoBehaviour
    {
        public TMP_Text floatingText;
        public float moveDistance = 50f;
        public float fadeDuration = 1f;

        void Start()
        {
            floatingText.DOFade(0f, fadeDuration);
            floatingText.rectTransform.DOAnchorPosY(floatingText.rectTransform.anchoredPosition.y + moveDistance, fadeDuration)
                                       .OnComplete(() => Destroy(gameObject));
        }
    }
}

