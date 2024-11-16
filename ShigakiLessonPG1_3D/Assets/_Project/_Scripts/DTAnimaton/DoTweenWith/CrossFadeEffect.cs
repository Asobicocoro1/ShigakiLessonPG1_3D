using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace WithDOTween
{
    public class CrossFadeEffect : MonoBehaviour
    {
        public CanvasGroup currentUI;
        public CanvasGroup nextUI;
        public float fadeDuration = 0.5f;

        public void CrossFade()
        {
            currentUI.DOFade(0f, fadeDuration);
            nextUI.DOFade(1f, fadeDuration);
        }
    }
}

