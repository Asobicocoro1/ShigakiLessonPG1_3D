using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 ### �A�j���[�V�����N���b�v�̃C���X�y�N�^�[�ڍ�

Unity�̃A�j���[�^�[�E�B���h�E�ŃA�j���[�V�����N���b�v��I������ƁA���̃N���b�v�̏ڍאݒ���C���X�y�N�^�[�Ŋm�F����ѕύX�ł��܂��B�ȉ��́A���̃C���X�y�N�^�[�̊e���ڂ̏ڍׂȉ���ł��B

1. **���O (Name):**
   - �A�j���[�V�����N���b�v�̖��O�ł��B���̖��O�́A�A�j���[�^�[�R���g���[���[�Ŏ��ʂ���܂��B

2. **�T���v�����[�g (Sample Rate):**
   - �A�j���[�V�����̃T���v�����[�g��ݒ肵�܂��B�ʏ�͕b������̃t���[���� (FPS) �Ŏw�肳��܂��B�Ⴆ�΁A`30` �Ȃ�30FPS�ł��B

3. **���[�v�^�C�� (Loop Time):**
   - �`�F�b�N������ƁA�A�j���[�V�������I�������Ƃ��Ɏ����I�ɍŏ��ɖ߂��ă��[�v�Đ�����܂��B�E�H�[�N�T�C�N����A�C�h���A�j���[�V�����ȂǁA�J��Ԃ��Đ�����K�v������A�j���[�V�����ɓK���Ă��܂��B

4. **���[�v�|�[�Y (Loop Pose):**
   - �`�F�b�N������ƁA���[�v�A�j���[�V�����̍Ō�̃t���[���ƍŏ��̃t���[���̊ԂŃX���[�Y�ȑJ�ڂ��s����悤�ɕ�Ԃ���܂��B���[�v�A�j���[�V�����̌p���ڂ����炩�ɂȂ�܂��B

5. **���[�g�g�����X�t�H�[���̈ʒu (Root Transform Position):**
   - **�x�C�N�C���g�|�[�Y (Bake Into Pose):** ���[�g�g�����X�t�H�[���̈ʒu�i�ړ��j���A�j���[�V�����̃|�[�Y�ɏĂ��t�����܂��B
   - **���̈ʒu��ێ� (Based Upon Original):** �A�j���[�V�����̌��̃��[�g�ʒu��ێ����܂��B

6. **���[�g�g�����X�t�H�[���̉�] (Root Transform Rotation):**
   - **�x�C�N�C���g�|�[�Y (Bake Into Pose):** ���[�g�g�����X�t�H�[���̉�]���A�j���[�V�����̃|�[�Y�ɏĂ��t�����܂��B
   - **���̉�]��ێ� (Based Upon Original):** �A�j���[�V�����̌��̃��[�g��]��ێ����܂��B
   - **XZ (Original):** XZ���ʏ�̃��[�g�g�����X�t�H�[���̉�]�݂̂�ێ����܂��B
   - **Y (Original):** Y�����̃��[�g�g�����X�t�H�[���̉�]�݂̂�ێ����܂��B

7. **���[�g�g�����X�t�H�[���̃X�P�[�� (Root Transform Scale):**
   - **�x�C�N�C���g�|�[�Y (Bake Into Pose):** ���[�g�g�����X�t�H�[���̃X�P�[�����A�j���[�V�����̃|�[�Y�ɏĂ��t�����܂��B
   - **���̃X�P�[����ێ� (Based Upon Original):** �A�j���[�V�����̌��̃��[�g�X�P�[����ێ����܂��B

8. **�C�x���g (Events):**
   - �A�j���[�V�����̓���̃t���[���ɃJ�X�^���C�x���g��ǉ��ł��܂��B�C�x���g�̓X�N���v�g����Ăяo����A����̃A�j���[�V�����t���[���ŃA�N�V���������s���܂��B




### �g�����W�V������I�������Ƃ��̃C���X�y�N�^�[�ڍ�

�A�j���[�^�[�E�B���h�E�Ńg�����W�V������I������ƁA���̃g�����W�V�����̏ڍאݒ���C���X�y�N�^�[�Ŋm�F����ѕύX�ł��܂��B�ȉ��́A���̃C���X�y�N�^�[�̊e���ڂ̏ڍׂȉ���ł��B

1. **���O (Name):**
   - �g�����W�V�����̖��O�ł��B�ʏ�A�����Őݒ肳��܂����A�K�v�ɉ����ĕύX�ł��܂��B

2. **���� (Conditions):**
   - �g�����W�V�������������������ݒ肵�܂��B�����́A�A�j���[�^�[�R���g���[���[�̃p�����[�^�Ɋ�Â��Đݒ肳��܂��B
   - **�p�����[�^ (Parameter):** �g�����W�V�����̃g���K�[�ƂȂ�p�����[�^��I�����܂��B
   - **��r (Comparison):** �p�����[�^������̒l�ɓ��B�����Ƃ��Ƀg�����W�V��������������悤�ɐݒ肵�܂��B��r���Z�q�i`==`, `!=`, `>`, `<`, `>=`, `<=`�j���g�p���܂��B
   - **�l (Value):** �g�����W�V��������������ۂ̃p�����[�^�̒l��ݒ肵�܂��B

3. **�ݒ� (Settings):**
   - �g�����W�V�����̏ڍׂȐݒ���s���܂��B
   - **�J�ڂ̎��� (Transition Duration):** �J�ڂ���������܂ł̎��Ԃ�ݒ肵�܂��B�l���������قǁA�J�ڂ͐v���ɍs���܂��B
   - **�J�n�I�t�Z�b�g (Transition Offset):** �g�����W�V�����̊J�n�ʒu���A�j���[�V�����̎��Ԏ���Őݒ肵�܂��B����ɂ��A�A�j���[�V�����̓���̃|�C���g����J�ڂ��J�n�ł��܂��B
   - **�Œ���� (Fixed Duration):** �`�F�b�N������ƁA�J�ڎ��Ԃ��Œ肳��܂��B�`�F�b�N���O���ƁA�J�ڎ��Ԃ̓^�[�Q�b�g�A�j���[�V�����̒����ɑ΂��銄���Ƃ��Čv�Z����܂��B

4. **�C���^�[�|���[�V���� (Interpolation):**
   - **�u�����h (Blend):** �g�����W�V�����̕�ԕ��@��ݒ肵�܂��B���j�A�A�J�[�u�x�[�X�A�J�X�^���Ȃǂ̕�ԕ��@��I���ł��܂��B
   - **�N���X�t�F�[�h (Crossfade):** �J�ڒ��̃N���X�t�F�[�h�̊�����ݒ肵�܂��B����ɂ��A�X���[�Y�ȑJ�ڂ��\�ɂȂ�܂��B

5. **�~���[�g (Mute):**
   - �g�����W�V�������ꎞ�I�ɖ��������܂��B����ɂ��A�g�����W�V�������폜�����ɓ�����m�F���邱�Ƃ��ł��܂��B

6. **Solo:**
   - ����̃g�����W�V�����݂̂�L�������A���̃g�����W�V�����𖳌������܂��B�f�o�b�O���ɓ���̃g�����W�V�����̓�����m�F����̂ɕ֗��ł��B

�����̏ڍׂ��m�F���A�K�؂ɐݒ肷�邱�ƂŁA�A�j���[�V�����̑J�ڂ����炩���Ӑ}�����ʂ�ɐ���ł��܂��B
 */
public class AnimationClipintheAnimatorWindow : MonoBehaviour
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