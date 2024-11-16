using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace WithDOTween
{
    public class SplitEffect : MonoBehaviour
    {
        public RectTransform uiElement;
        public float duration = 0.5f;
        public Vector2 splitDistance = new Vector2(200, 200);

        public void Split()
        {
            for (int i = 0; i < 4; i++)
            {
                Vector2 direction = new Vector2(
                    (i % 2 == 0 ? 1 : -1) * splitDistance.x,
                    (i < 2 ? 1 : -1) * splitDistance.y
                );

                RectTransform clone = Instantiate(uiElement, transform);
                clone.DOAnchorPos(clone.anchoredPosition + direction, duration)
                     .SetEase(Ease.OutQuad)
                     .OnComplete(() => Destroy(clone.gameObject));
            }

            uiElement.DOScale(Vector3.zero, duration).SetEase(Ease.InBack);
        }
    }
}
