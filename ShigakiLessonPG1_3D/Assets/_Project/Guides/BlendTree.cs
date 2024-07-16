using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 Unity�̃u�����h�c���[�iBlend Tree�j�́A�����̃A�j���[�V��������̃p�����[�^�Ɋ�Â��ău�����h���邽�߂̎d�g�݂ł��B����ɂ��A�X���[�Y�ȃA�j���[�V�����J�ڂ��\�ɂȂ�܂��B�u�����h�c���[�̃C���X�y�N�^�[�̊e�Z�N�V�����ɂ��Đ������܂��B

### �u�����h�c���[�̃C���X�y�N�^�[�̍\��

1. **Blend Tree �m�[�h**
   - **���O:** �u�����h�c���[�̖��O��\�����܂��B�f�t�H���g�ł� `Blend Tree` �Ƃ������O���t���܂��B
   - **Blend Type:** �ǂ̃^�C�v�̃u�����h���g�p���邩��I�����܂��B�ȉ��̃I�v�V����������܂��B
     - **1D:** �P��̃p�����[�^�Ɋ�Â��ău�����h���܂��B
     - **2D Simple Directional:** ��̃p�����[�^�Ɋ�Â��ău�����h���܂��B�A�j���[�V�����͕����Ɋ�Â��Ĕz�u����܂��B
     - **2D Freeform Directional:** ��̃p�����[�^�Ɋ�Â��ău�����h���܂��B�A�j���[�V�����͎��R�ɔz�u����܂��B
     - **2D Freeform Cartesian:** ��̃p�����[�^�Ɋ�Â��ău�����h���܂��B�A�j���[�V�����̓O���b�h�ɉ����Ĕz�u����܂��B

2. **Blend Parameters**
   - **Blend Parameter:** �u�����h�Ɏg�p����p�����[�^��I�����܂��B1D�̏ꍇ�͈�A2D�̏ꍇ�͓�̃p�����[�^���w�肵�܂��B
     - **Parameter:** �A�j���[�^�[�R���g���[���[�̃p�����[�^���X�g����I�����܂��B
     - **Threshold:** �u�����h���n�܂邵�����l��ݒ肵�܂��B�Ⴆ�΁A���x������̒l�ɒB�����Ƃ��ɃA�j���[�V�������؂�ւ��悤�ɐݒ�ł��܂��B

3. **Motions (���[�V����)**
   - �u�����h�c���[�Ɋ܂߂�A�j���[�V�����N���b�v��T�u�u�����h�c���[��ǉ����܂��B
     - **Motion:** �u�����h�Ɋ܂߂�A�j���[�V�����N���b�v��T�u�u�����h�c���[��ݒ肵�܂��B
     - **Threshold:** ���ꂼ��̃��[�V�������ǂ̂������l�ōĐ�����邩��ݒ肵�܂��i1D�̏ꍇ�j�B
     - **Position:** 2D�u�����h�c���[�̏ꍇ�A�e���[�V�����̍��W��ݒ肵�܂��B

4. **Visualization (����)**
   - �u�����h�c���[�̃r�W���A���\���B�A�j���[�V�������ǂ̂悤�Ƀu�����h����邩�����o�I�Ɋm�F�ł��܂��B����2D�u�����h�c���[�̏ꍇ�A�e�A�j���[�V�����̈ʒu���m�F���܂��B


5. **Adjustments and Fine-tuning (�����Ɣ�����)**
   - **Blend Weights:** �u�����h�c���[���Đ�����Ă���Ƃ��ɁA�e���[�V�����̃u�����h�E�F�C�g��\�����܂��B����ɂ��A�ǂ̃A�j���[�V�������ǂꂭ�炢�̊����ōĐ�����Ă��邩���m�F�ł��܂��B
   - **Synchronize Motion:** �����̃��[�V�����𓯊������邩�ǂ�����I�����܂��B�Ⴆ�΁A���s�Ƒ��s�̃A�j���[�V�����𓯊����������ꍇ�Ɏg�p���܂��B

### ��̗�: 1D �u�����h�c���[�̐ݒ�

1. **Blend Type:** 1D
2. **Blend Parameter:** `speed`
3. **Motions:**
   - `idle` (Threshold: 0)
   - `walk` (Threshold: 1)
   - `run` (Threshold: 2)

���̐ݒ�ł́A`speed` �p�����[�^��0�̂Ƃ��� `idle` �A�j���[�V�������Đ�����A1�̂Ƃ��� `walk`�A2�̂Ƃ��� `run` �A�j���[�V�������Đ�����܂��B�܂��A`speed` �p�����[�^��0��1�̊Ԃ̒l�����ꍇ�A`idle` �� `walk` �̃A�j���[�V�������u�����h����܂��B���l�ɁA`speed` ��1��2�̊Ԃ̒l�����ꍇ�� `walk` �� `run` �̃A�j���[�V�������u�����h����܂��B

### ��̗�: 2D �u�����h�c���[�̐ݒ�

1. **Blend Type:** 2D Freeform Directional
2. **Blend Parameters:** 
   - Parameter 1: `horizontal`
   - Parameter 2: `vertical`
3. **Motions:**
   - `idle` (Position: (0, 0))
   - `walk_forward` (Position: (0, 1))
   - `walk_backward` (Position: (0, -1))
   - `walk_left` (Position: (-1, 0))
   - `walk_right` (Position: (1, 0))

���̐ݒ�ł́A`horizontal` �� `vertical` �̃p�����[�^�Ɋ�Â��ăA�j���[�V�������u�����h����܂��B�Ⴆ�΁A�L�����N�^�[���O���ɐi�ޏꍇ (`vertical` ��1)�A`walk_forward` �A�j���[�V�������Đ�����A���ɐi�ޏꍇ (`horizontal` ��-1)�A`walk_left` �A�j���[�V�������Đ�����܂��B�����̃p�����[�^�����Ԃ̒l�����ꍇ�A�K�؂ȃA�j���[�V�������u�����h����܂��B

### �悭����g���u���V���[�e�B���O

1. **�p�����[�^�������Ă��Ȃ��ꍇ:**
   - �X�N���v�g�Ńp�����[�^�𐳂����ݒ肵�Ă��邩�m�F���܂��B�Ⴆ�΁A`animator.SetFloat("speed", speed);` �̂悤�ɃA�j���[�^�[�p�����[�^���X�V����Ă��邱�Ƃ��m�F���܂��B
   - �A�j���[�^�[�E�B���h�E�ŁA�u�����h�p�����[�^�����������삵�Ă��邩�f�o�b�O���܂��B

2. **�A�j���[�V���������Ғʂ�Ƀu�����h����Ȃ��ꍇ:**
   - �u�����h�c���[�̐ݒ���Ċm�F���A�e���[�V�����̂������l��ʒu�����������m�F���܂��B
   - �A�j���[�V�����̃g�����W�V������������m�F���A�s�v�ȃg�����W�V�������Ȃ����m�F���܂��B

�����̐ݒ�Ɗm�F��ʂ��āA�u�����h�c���[�̃C���X�y�N�^�[�����ʓI�Ɏg�p���A�X���[�Y�ȃA�j���[�V�����J�ڂ������ł���͂��ł��B
 */
public class BlendTree : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
