using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace WithDOTween
{
    public class SlideEffect : MonoBehaviour
    {
        public RectTransform rectTransform;
        public Vector2 startPos = new Vector2(-500, 0);
        public Vector2 endPos = Vector2.zero;
        public float slideDuration = 0.5f;

        void Start()
        {
            rectTransform.anchoredPosition = startPos;
            rectTransform.DOAnchorPos(endPos, slideDuration); // �X���C�h�C��
        }
    }
}
/*
 1. �X���C�h�A�E�g�̒ǉ�
�X���C�h�C����A�{�^�������Ȃǂ̃g���K�[�ŃX���C�h�A�E�g����A�j���[�V������ǉ����邱�Ƃ��\�ł��B

csharp
�R�[�h���R�s�[����
public void SlideOut()
{
    rectTransform.DOAnchorPos(startPos, slideDuration); // �J�n�ʒu�֖߂�
}
2. Ease�̐ݒ�
DOTween���g���ꍇ�AEase��ݒ肷�邱�ƂŃA�j���[�V�����̓����Ƀ����n�����������܂��B

csharp
�R�[�h���R�s�[����
rectTransform.DOAnchorPos(endPos, slideDuration).SetEase(Ease.OutBounce);
 */
