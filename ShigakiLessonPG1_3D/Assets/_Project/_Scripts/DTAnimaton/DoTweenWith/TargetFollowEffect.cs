using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace WithDOTween
{
    public class TargetFollowEffect : MonoBehaviour
    {
        public RectTransform uiElement;
        public Transform target;
        public float followDuration = 0.3f;

        void Update()
        {
            Vector2 screenPos = Camera.main.WorldToScreenPoint(target.position);
            uiElement.DOAnchorPos(screenPos, followDuration).SetEase(Ease.OutQuad);
        }
    }
}
