using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace WithDOTween
{
    public class ColorChangeEffect : MonoBehaviour
    {
        public Image buttonImage;
        public float colorDuration = 0.3f;

        void Start()
        {
            buttonImage.DOColor(Color.green, colorDuration);
        }
    }
}
/*�{�^���ŐF�ύX���g���K�[����:
Button���g�p���Ă���ꍇ�A�N���b�N���ɐF��ς��邱�Ƃ��ł��܂��B
Button��On Click()�C�x���g��ColorChangeEffect�̃��\�b�h��ݒ�B

 �C���^���N�e�B�u�ȐF�ύX
�{�^���̃z�o�[���ɐF��ς���: �{�^����Event Trigger���g�p���āA�z�o�[��N���b�N���ɈقȂ�F��K�p�ł��܂��B
2. Ease�̃J�X�^�}�C�Y
DOTween���g�p���Ă���ꍇ�A�F�ύX��Ease��K�p���ĕω������炩�ɂł��܂��B

�Q�l�X�N���v�g
image.DOColor(endColor, colorDuration).SetEase(Ease.InOutSine);*/
