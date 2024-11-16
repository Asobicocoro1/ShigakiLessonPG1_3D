using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WithoutDOTween
{
    public class FollowMouseEffect : MonoBehaviour
    {
        public RectTransform uiElement;
        public float followSpeed = 0.5f;

        void Update()
        {
            Vector2 mousePosition = Input.mousePosition;
            uiElement.anchoredPosition = Vector2.Lerp(uiElement.anchoredPosition, mousePosition, followSpeed * Time.deltaTime);
        }
    }
}

