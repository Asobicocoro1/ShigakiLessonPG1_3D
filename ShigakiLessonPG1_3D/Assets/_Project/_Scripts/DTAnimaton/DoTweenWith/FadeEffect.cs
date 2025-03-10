using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/*
 dvÈbZ[Wâo	1b`3b
ÊíÌUIÌ\¦Eñ\¦	0.5b`1b
uÔIÈìâÊm	0.1b`0.5b
 */

namespace WithDOTween
{
    public class FadeEffect : MonoBehaviour
    {
        public CanvasGroup canvasGroup;
        public float fadeDuration = 0.5f;

        void Start()
        {
            canvasGroup.alpha = 0;
            canvasGroup.DOFade(1f, fadeDuration); // tF[hC
        }
    }
}
