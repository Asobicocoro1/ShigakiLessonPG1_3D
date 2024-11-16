using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WithoutDOTween
{
    public class IndicatorRingEffect : MonoBehaviour
    {
        public RectTransform ring;
        public float rotationSpeed = 100f;

        void Update()
        {
            ring.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
        }
    }
}

