using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace WithDOTween
{
    public class FadeEffect : MonoBehaviour
    {
        public CanvasGroup canvasGroup;
        public float fadeDuration = 0.5f;

        void Start()
        {
            canvasGroup.alpha = 0;
            canvasGroup.DOFade(1f, fadeDuration); // フェードイン
        }
    }
}
