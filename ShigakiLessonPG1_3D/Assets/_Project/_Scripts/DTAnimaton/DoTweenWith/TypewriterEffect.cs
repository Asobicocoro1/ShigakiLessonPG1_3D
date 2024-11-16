using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

namespace WithDOTween
{
    public class TypewriterEffect : MonoBehaviour
    {
        public TMP_Text textComponent;
        public string fullText;
        public float typeSpeed = 0.05f;

        void Start()
        {
            textComponent.text = ""; // èâä˙âª
            int length = fullText.Length;

            Sequence sequence = DOTween.Sequence();
            for (int i = 0; i < length; i++)
            {
                int index = i;
                sequence.AppendCallback(() => textComponent.text += fullText[index])
                        .AppendInterval(typeSpeed);
            }
        }
    }
}

