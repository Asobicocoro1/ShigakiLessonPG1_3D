using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace WithDOTween
{
    public class BeatEffect : MonoBehaviour
    {
        public RectTransform uiElement;
        public float beatDuration = 0.5f;

        void Start()
        {
            uiElement.DOScale(new Vector3(1.2f, 1.2f, 1f), beatDuration)
                     .SetEase(Ease.InOutSine)
                     .SetLoops(-1, LoopType.Yoyo);
        }
    }
}

