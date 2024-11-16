using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace WithDOTween
{
    public class ShakeEffect : MonoBehaviour
    {
        public RectTransform errorMessage;
        public float duration = 0.5f;

        void Start()
        {
            errorMessage.DOShakePosition(duration, strength: new Vector3(10, 0, 0), vibrato: 10, randomness: 90);
        }
    }
}
