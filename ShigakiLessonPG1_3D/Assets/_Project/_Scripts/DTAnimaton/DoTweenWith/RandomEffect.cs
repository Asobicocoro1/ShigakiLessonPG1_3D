using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace WithDOTween
{
    public class RandomEffect : MonoBehaviour
    {
        public RectTransform[] uiElements;
        public float duration = 0.5f;
        public Vector2 minPosition = new Vector2(-300, -300);
        public Vector2 maxPosition = new Vector2(300, 300);

        void Start()
        {
            foreach (var element in uiElements)
            {
                Vector2 randomPos = new Vector2(Random.Range(minPosition.x, maxPosition.x), Random.Range(minPosition.y, maxPosition.y));
                element.DOAnchorPos(randomPos, duration).SetEase(Ease.OutQuad);
            }
        }
    }
}
