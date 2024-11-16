using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WithoutDOTween
{
    public class TargetFollowEffect : MonoBehaviour
    {
        public RectTransform uiElement;
        public Transform target;
        public float followSpeed = 0.3f;

        void Update()
        {
            Vector2 screenPos = Camera.main.WorldToScreenPoint(target.position);
            uiElement.anchoredPosition = Vector2.Lerp(uiElement.anchoredPosition, screenPos, followSpeed * Time.deltaTime);
        }
    }
}
