using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WithoutDOTween
{
    public class CardSwipeEffect : MonoBehaviour
    {
        public RectTransform card;
        public float swipeDistance = 500f;
        public float swipeDuration = 0.5f;

        public void SwipeLeft()
        {
            StartCoroutine(Swipe(-swipeDistance));
        }

        public void SwipeRight()
        {
            StartCoroutine(Swipe(swipeDistance));
        }

        IEnumerator Swipe(float targetX)
        {
            Vector2 startPos = card.anchoredPosition;
            Vector2 endPos = new Vector2(targetX, startPos.y);
            float elapsedTime = 0f;

            while (elapsedTime < swipeDuration)
            {
                card.anchoredPosition = Vector2.Lerp(startPos, endPos, elapsedTime / swipeDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            card.anchoredPosition = endPos;

            // Œ³‚ÌˆÊ’u‚É–ß‚·
            elapsedTime = 0f;
            while (elapsedTime < swipeDuration)
            {
                card.anchoredPosition = Vector2.Lerp(endPos, startPos, elapsedTime / swipeDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            card.anchoredPosition = startPos;
        }
    }
}

