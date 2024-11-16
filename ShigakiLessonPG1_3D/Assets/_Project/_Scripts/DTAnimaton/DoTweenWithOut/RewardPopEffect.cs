using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WithoutDOTween
{
    public class RewardPopEffect : MonoBehaviour
    {
        public RectTransform rewardIcon;
        public float popDuration = 0.8f;

        void Start()
        {
            StartCoroutine(PopReward());
        }

        IEnumerator PopReward()
        {
            Vector3 startScale = Vector3.zero;
            Vector3 peakScale = new Vector3(1.5f, 1.5f, 1f);
            float halfDuration = popDuration / 2;
            float elapsedTime = 0f;

            while (elapsedTime < halfDuration)
            {
                rewardIcon.localScale = Vector3.Lerp(startScale, peakScale, elapsedTime / halfDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            elapsedTime = 0f;
            while (elapsedTime < halfDuration)
            {
                rewardIcon.localScale = Vector3.Lerp(peakScale, startScale, elapsedTime / halfDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            rewardIcon.localScale = startScale;
        }
    }
}

