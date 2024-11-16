using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WithoutDOTween
{
    public class RotateIndicatorEffect : MonoBehaviour
    {
        public RectTransform indicator;
        public float rotateDuration = 1f;

        void Start()
        {
            StartCoroutine(Rotate());
        }

        IEnumerator Rotate()
        {
            while (true)
            {
                float elapsedTime = 0f;

                while (elapsedTime < rotateDuration)
                {
                    float angle = Mathf.Lerp(0, -360, elapsedTime / rotateDuration);
                    indicator.rotation = Quaternion.Euler(0, 0, angle);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }
            }
        }
    }
}

