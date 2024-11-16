using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WithoutDOTween
{
    public class ParticleEffect : MonoBehaviour
    {
        public RectTransform uiElement;
        public ParticleSystem particleSystem;
        public float fadeDuration = 0.5f;

        void Start()
        {
            StartCoroutine(ShowWithParticles());
        }

        IEnumerator ShowWithParticles()
        {
            uiElement.localScale = Vector3.zero;
            particleSystem.Play();

            float elapsedTime = 0f;
            while (elapsedTime < fadeDuration)
            {
                uiElement.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, elapsedTime / fadeDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            uiElement.localScale = Vector3.one;
        }
    }
}

