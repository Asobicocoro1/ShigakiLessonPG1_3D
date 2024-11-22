using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/*
 �d�v�ȃ��b�Z�[�W�≉�o	1�b�`3�b
�ʏ��UI�̕\���E��\��	0.5�b�`1�b
�u�ԓI�ȑ����ʒm	0.1�b�`0.5�b
 */

namespace WithDOTween
{
    public class FadeEffect : MonoBehaviour
    {
        public CanvasGroup canvasGroup;
        public float fadeDuration = 0.5f;

        void Start()
        {
            canvasGroup.alpha = 0;
            canvasGroup.DOFade(1f, fadeDuration); // �t�F�[�h�C��
        }
    }
}
