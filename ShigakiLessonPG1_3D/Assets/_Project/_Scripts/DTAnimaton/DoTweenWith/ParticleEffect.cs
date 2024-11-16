using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace WithDOTween
{
    public class ParticleEffect : MonoBehaviour
    {
        public RectTransform uiElement;
        public ParticleSystem particleSystem;
        public float fadeDuration = 0.5f;

        void Start()
        {
            uiElement.localScale = Vector3.zero;
            uiElement.DOScale(Vector3.one, fadeDuration).SetEase(Ease.OutBack)
                     .OnStart(() => particleSystem.Play());
        }
    }
}

