using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

namespace WithDOTween
{
    public class ScoreCounter : MonoBehaviour
    {
        public TMP_Text scoreText;
        public int targetScore = 1000;
        public float countDuration = 1f;

        void Start()
        {
            int currentScore = 0;
            DOTween.To(() => currentScore, x => currentScore = x, targetScore, countDuration)
                   .OnUpdate(() => scoreText.text = currentScore.ToString());
        }
    }
}

