using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace WithDOTween
{
    public class CardSwipeEffect : MonoBehaviour
    {
        public RectTransform card;
        public float swipeDistance = 500f;
        public float swipeDuration = 0.5f;

        public void Start()
        {
            SwipeLeft();
        }
        public void SwipeLeft()
        {
            card.DOAnchorPosX(-swipeDistance, swipeDuration)
                .SetEase(Ease.OutQuad)
                .OnComplete(() => card.DOAnchorPosX(0, swipeDuration));
        }

        public void SwipeRight()
        {
            card.DOAnchorPosX(swipeDistance, swipeDuration)
                .SetEase(Ease.OutQuad)
                .OnComplete(() => card.DOAnchorPosX(0, swipeDuration));
        }
    }
}
