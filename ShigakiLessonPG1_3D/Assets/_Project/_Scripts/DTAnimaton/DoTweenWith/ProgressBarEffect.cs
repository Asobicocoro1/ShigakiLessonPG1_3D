using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace WithDOTween
{
    public class ProgressBarEffect : MonoBehaviour
    {
        public Slider progressBar;
        public float loadDuration = 2f;

        void Start()
        {
            progressBar.DOValue(1f, loadDuration).SetEase(Ease.Linear);
        }
    }
}

