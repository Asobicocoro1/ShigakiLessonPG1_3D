using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

namespace WithDOTween
{
    public class GradientTextEffect : MonoBehaviour
    {
        public TMP_Text text;
        public Color startColor = Color.red;
        public Color endColor = Color.blue;
        public float duration = 2f;

        void Start()
        {
            DOTween.To(() => startColor, x => text.color = x, endColor, duration)
                   .SetLoops(-1, LoopType.Yoyo);
        }
    }
}

