using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace WithDOTween
{
    public class SlideEffect : MonoBehaviour
    {
        public RectTransform rectTransform;
        public Vector2 startPos = new Vector2(-500, 0);
        public Vector2 endPos = Vector2.zero;
        public float slideDuration = 0.5f;

        void Start()
        {
            rectTransform.anchoredPosition = startPos;
            rectTransform.DOAnchorPos(endPos, slideDuration); // スライドイン
        }
    }
}

