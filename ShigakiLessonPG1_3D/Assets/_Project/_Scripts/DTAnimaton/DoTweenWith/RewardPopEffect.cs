using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace WithDOTween
{
    public class RewardPopEffect : MonoBehaviour
    {
        public RectTransform rewardIcon;
        public float popDuration = 0.8f;

        void Start()
        {
            rewardIcon.localScale = Vector3.zero;
            rewardIcon.DOScale(new Vector3(1.5f, 1.5f, 1f), popDuration / 2)
                      .SetEase(Ease.OutBack)
                      .OnComplete(() => rewardIcon.DOScale(Vector3.zero, popDuration / 2).SetEase(Ease.InBack));
        }
    }
}
