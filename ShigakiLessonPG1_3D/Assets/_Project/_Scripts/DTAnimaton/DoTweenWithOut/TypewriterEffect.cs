using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace WithoutDOTween
{
    public class TypewriterEffect : MonoBehaviour
    {
        public TMP_Text textComponent;
        public string fullText;
        public float typeSpeed = 0.05f;

        void Start()
        {
            StartCoroutine(ShowText());
        }

        IEnumerator ShowText()
        {
            textComponent.text = ""; // èâä˙âª
            foreach (char c in fullText)
            {
                textComponent.text += c;
                yield return new WaitForSeconds(typeSpeed);
            }
        }
    }
}

