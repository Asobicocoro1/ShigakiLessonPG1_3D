using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace WithDOTween
{
    public class FollowMouseEffect : MonoBehaviour
    {
        public RectTransform uiElement;
        public float followSpeed = 0.5f;

        void Update()
        {
            Vector2 mousePosition = Input.mousePosition;
            uiElement.DOAnchorPos(mousePosition, followSpeed).SetEase(Ease.OutQuad);
        }
    }
}

