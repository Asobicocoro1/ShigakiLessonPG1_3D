using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace WithDOTween
{
    public class AutoHideEffect : MonoBehaviour
    {
        public CanvasGroup notification;
        public float displayDuration = 2f;
        public float fadeDuration = 0.5f;

        void Start()
        {
            notification.DOFade(0f, fadeDuration).SetDelay(displayDuration);
        }
    }
}

