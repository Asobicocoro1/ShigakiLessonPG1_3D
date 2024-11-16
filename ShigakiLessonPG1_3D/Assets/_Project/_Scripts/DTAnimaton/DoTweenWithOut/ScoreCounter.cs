using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace WithoutDOTween
{
    public class ScoreCounter : MonoBehaviour
    {
        public TMP_Text scoreText;
        public int targetScore = 1000;
        public float countDuration = 1f;

        void Start()
        {
            StartCoroutine(CountUp());
        }

        IEnumerator CountUp()
        {
            int currentScore = 0;
            float elapsedTime = 0f;

            while (elapsedTime < countDuration)
            {
                currentScore = Mathf.RoundToInt(Mathf.Lerp(0, targetScore, elapsedTime / countDuration));
                scoreText.text = currentScore.ToString();
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            scoreText.text = targetScore.ToString();
        }
    }
}
