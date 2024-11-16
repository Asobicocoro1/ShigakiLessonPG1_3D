using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace WithDOTween
{
    public class RadialMenuEffect : MonoBehaviour
    {
        public RectTransform[] menuItems;
        public float radius = 100f;
        public float duration = 0.5f;

        void Start()
        {
            for (int i = 0; i < menuItems.Length; i++)
            {
                float angle = i * (360f / menuItems.Length);
                Vector2 targetPos = new Vector2(
                    Mathf.Cos(angle * Mathf.Deg2Rad) * radius,
                    Mathf.Sin(angle * Mathf.Deg2Rad) * radius
                );

                menuItems[i].DOAnchorPos(targetPos, duration).SetEase(Ease.OutBack);
            }
        }
    }
}

