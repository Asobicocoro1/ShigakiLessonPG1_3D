using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*### Model �^�u

1. **Mesh Compression**:
   - **Off**: ���k���Ȃ�
   - **Low/Medium/High**: ���b�V���f�[�^�����k���ă������g�p�ʂ����炷

2. **Read/Write Enabled**:
   - **Enabled**: ���b�V���f�[�^���X�N���v�g�œǂݏ����\

3. **Optimize Mesh**:
   - **Enabled**: ���b�V���f�[�^�̕`��p�t�H�[�}���X���œK��

4. **Keep Quads**:
   - **Enabled**: �l�p�`�|���S�����ێ�

5. **Weld Vertices**:
   - **Enabled**: �ߐڂ��钸�_���������ăf�[�^�ʂ����炷

6. **Index Format**:
   - **16 bit**: �����ȃ��b�V���p�i���_����65,536�����j
   - **32 bit**: �傫�ȃ��b�V���p�i���_����65,536�ȏ�j

7. **Normals**:
   - **Import**: ���f���ɕt���̖@�����g�p
   - **Calculate**: Unity���@�����v�Z
   - **None**: �@�����g�p���Ȃ�

8. **Normals Mode**:
   - **Unweighted Legacy**: �Â��@���v�Z����
   - **Unweighted**: �V�����@���v�Z����
   - **Angle Weighted**: �p�x���l�����Čv�Z
   - **Area Weighted**: �ʐς���Ɍv�Z
   - **None**: �@�����v�Z���Ȃ�

9. **Smoothing Angle**:
   - �@���v�Z���̊p�x�ݒ�B�傫���قǃX���[�Y�ȃV�F�[�f�B���O

10. **Tangents**:
    - **Import**: ���f���ɕt���̐ڐ����g�p
    - **Calculate (MikkTSpace)**: MikkTSpace�A���S���Y���Őڐ����v�Z
    - **Calculate (Legacy)**: �������Őڐ����v�Z
    - **None**: �ڐ����g�p���Ȃ�

11. **Generate Lightmap UVs**:
    - **Enabled**: ���C�g�}�b�v�pUV�𐶐�

12. **Lightmap Pack Margin**:
    - ���C�g�}�b�vUV�Ԃ̃}�[�W���ݒ�

13. **Generate Secondary UVs**:
    - **Enabled**: ��UV�𐶐�

14. **Secondary UV Pack Margin**:
    - ��UV�Ԃ̃}�[�W���ݒ�

### Rig �^�u

1. **Animation Type**:
   - **None**: �A�j���[�V�������C���|�[�g���Ȃ�
   - **Legacy**: �Â��A�j���[�V�����V�X�e�����g�p
   - **Generic**: �ėp���O���g�p
   - **Humanoid**: �q���[�}�m�C�h���O���g�p

2. **Avatar Definition**:
   - **Create From This Model**: �V�����A�o�^�[���쐬
   - **Copy From Other Avatar**: ���̃��f������A�o�^�[���R�s�[

3. **Root Node**:
   - ���[�g�m�[�h�̎w��B�A�j���[�V�����̊

4. **Optimize Game Objects**:
   - **Enabled**: �s�v�ȃ{�[����I�u�W�F�N�g���폜���ĊK�w���ȗ���

### Animation �^�u

1. **Import Animation**:
   - **Enabled**: �A�j���[�V�������C���|�[�g

2. **Animation Compression**:
   - **Off**: ���k���Ȃ�
   - **Keyframe Reduction**: �L�[�t���[���̐������炷
   - **Keyframe Reduction and Compression**: �L�[�t���[���팸�ƈ��k���s��
   - **Optimal**: �œK�Ȉ��k���@���g�p

3. **Animation Clip Settings**:
   - **Loop Time**: �A�j���[�V���������[�v
   - **Loop Pose**: ���[�v�̃X���[�Y����ۂ��߃|�[�Y�𒲐�
   - **Cycle Offset**: ���[�v�I�t�Z�b�g�̐ݒ�
   - **Additive Reference Pose**: ���Z��|�[�Y�̐ݒ�

4. **Root Transform Rotation**:
   - **Bake Into Pose**: ��]���|�[�Y�Ƀx�C�N
   - **Based Upon**: ��]�̊�ݒ�

5. **Root Transform Position (Y)**:
   - **Bake Into Pose**: Y���W�̈ʒu���|�[�Y�Ƀx�C�N
   - **Based Upon**: Y���W�̊�ݒ�

6. **Root Transform Position (XZ)**:
   - **Bake Into Pose**: XZ���W�̈ʒu���|�[�Y�Ƀx�C�N
   - **Based Upon**: XZ���W�̊�ݒ�

### Materials �^�u

1. **Import Materials**:
   - **Enabled**: �}�e���A�����C���|�[�g

2. **Material Naming**:
   - **By Base Texture Name**: �x�[�X�e�N�X�`�����Ń}�e���A���𖽖�
   - **From Model's Material**: ���f���̃}�e���A�������g�p

3. **Material Search**:
   - **Local Materials Folder**: ���[�J���t�H���_�[���猟��
   - **Recursive Up**: ��ʃt�H���_�[���ċA�I�Ɍ���
   - **Project-Wide**: �v���W�F�N�g�S�̂��猟��

### ���̑��̃I�v�V����

1. **Import Constraints**:
   - **Enabled**: ���f���̐���i�R���X�g���C���g�j���C���|�[�g

 * 
 * 
 * 
 * �ڂ������
 * 
 * 
 * 
 * 
 * 

### Model �^�u

1. **Mesh Compression**:
   - **Off**: ���k���܂���B
   - **Low, Medium, High**: ���b�V���f�[�^�����k���āA�������g�p�ʂ����������܂��B���k���������قǁA�f�[�^���������Ȃ�܂����A�i�����ቺ����\��������܂��B

2. **Read/Write Enabled**:
   - **Enabled**: ���b�V���f�[�^���X�N���v�g�œǂݏ����ł���悤�ɂ��܂��B�I�t�ɂ���ƃ�������ߖ�ł��܂����A�X�N���v�g�Ń��b�V���𑀍�ł��Ȃ��Ȃ�܂��B

3. **Optimize Mesh**:
   - **Enabled**: ���b�V���f�[�^���œK�����āA�`��p�t�H�[�}���X�����コ���܂��B
    Unity��Optimize Mesh�@�\�ł́A���b�V���̃|���S���I�[�_�[�iPolygon Order�j�ƃo�[�e�b�N�X�I�[�_�[�iVertex Order�j�̈Ⴂ�͈ȉ��̒ʂ�ł��B

-----------------------------------------------------------------------------------------------------------------------------
### �|���S���I�[�_�[�iPolygon Order�j
�|���S���I�[�_�[�́A���b�V�����̎O�p�`�i�|���S���j�̕��я����œK�����܂��B����ɂ��A���b�V���������_�����O����ۂ̌��������サ�܂��B��̓I�ɂ́A�ȉ��̂悤�Ȍ��ʂ�����܂��F

1. **�L���b�V���̍œK��**�F
   - GPU�̒��_�L���b�V���̌��������߂邱�ƂŁA�������_�����x���t�F�b�`�����̂�����܂��B����ɂ��A�����_�����O�̃p�t�H�[�}���X�����サ�܂��B

2. **�����_�����O�̈�ѐ�**�F
   - �|���S���̏������œK�����邱�ƂŁA���b�V���̕`�悪���X���[�Y�ɂȂ�A���o�I�Ȉ�ѐ����ۂ���܂��B

### �o�[�e�b�N�X�I�[�_�[�iVertex Order�j
�o�[�e�b�N�X�I�[�_�[�́A���b�V�����̒��_�̕��я����œK�����܂��B���̍œK���́A���ɃX�L�j���O���b�V����A�j���[�V�������b�V���ŏd�v�ł��B�ȉ��̂悤�Ȍ��ʂ�����܂��F

1. **�X�L�j���O�̌�����**�F
   - �X�L�����b�V���̏ꍇ�A�œK�����ꂽ���_�����̓X�L���j���O�v�Z�����������ACPU�̕��ׂ����炵�܂��B����ɂ��A�A�j���[�V�����̃p�t�H�[�}���X�����サ�܂��B

2. **�������̋Ǐ���**�F
   - ���_�f�[�^�̃������Ǐ��������サ�A�L���b�V���~�X���������܂��B����ɂ��A�������A�N�Z�X�̌��������܂�A�S�̓I�ȃp�t�H�[�}���X�����P���܂��B


�����̍œK���́A�ŏI�I�ɂ̓��b�V���̃����_�����O�p�t�H�[�}���X�����コ���邽�߂̂��̂ł����A�قȂ鑤�ʂɏœ_�𓖂ĂĂ��܂��B�p�r�ɉ����āA�K�؂ȍœK����@��I�����邱�Ƃ��d�v�ł��B
-----------------------------------------------------------------------------------------------------------------------------

4. **Keep Quads**:
   - **Enabled**: �l�p�`�̃|���S�����ێ����܂��B���f�����O�\�t�g�E�F�A����C���|�[�g����ۂɕ֗��ł��B

5. **Weld Vertices**:
   - **Enabled**: �ߐڂ��钸�_���������A���b�V���̃f�[�^�ʂ����炵�܂��B

6. **Index Format**:
   - **16 bit**: �����ȃ��b�V���p�B
   - **32 bit**: �傫�ȃ��b�V���p�B���_����65,536�𒴂���ꍇ�ɕK�v�ł��B

7. **Normals**:
   - **Import**: ���f���ɕt���̖@�����g�p���܂��B
   - **Calculate**: Unity���@�����v�Z���܂��B�X���[�Y�ȃV�F�[�f�B���O���s�������ꍇ�Ɏg�p���܂��B
   - **None**: �@�����g�p���܂���B

8. **Normals Mode**:
   - **Unweighted Legacy**: �Â��@���v�Z�����B
   - **Unweighted**: �V�����@���v�Z�����B
   - **Angle Weighted**: ��芊�炩�Ȍ��ʂ𓾂邽�߂Ɋp�x���l�����܂�.
   - **Area Weighted**: �ʐςɊ�Â��Ė@�����v�Z���܂��B
   - **None**: �@�����v�Z���܂���B

9. **Smoothing Angle**:
   - �@�����v�Z����ۂɎg�p�����p�x�B�傫���قǃX���[�Y�ȃV�F�[�f�B���O�ɂȂ�܂��B

10. **Tangents**:
    - **Import**: ���f���ɕt���̐ڐ����g�p���܂��B
    - **Calculate (MikkTSpace)**: Unity��MikkTSpace�A���S���Y�����g�p���Đڐ����v�Z���܂��B
    - **Calculate (Legacy)**: Unity���������Őڐ����v�Z���܂��B
    - **None**: �ڐ����g�p���܂���B

11. **Mesh Compression**:
    - **Off, Low, Medium, High**: ���b�V���f�[�^�����k���܂��B���k���������قǃf�[�^�ʂ��������܂����A�i���ɉe�����o��ꍇ������܂��B

12. **Generate Lightmap UVs**:
    - **Enabled**: ���C�g�}�b�v�p��UV�𐶐����܂��B

13. **Lightmap Pack Margin**:
    - ���C�g�}�b�v�pUV�Ԃ̃}�[�W���B�l���傫���قǌ��Ԃ��L����܂��B

14. **Generate Secondary UVs**:
    - **Enabled**: ��UV�𐶐����܂��B���Ƀ��C�g�}�b�s���O��x�C�N�hGI�Ɏg�p���܂��B

15. **Secondary UV Pack Margin**:
    - ��UV�Ԃ̃}�[�W���B

### Rig �^�u

1. **Animation Type**:
   - **None**: �A�j���[�V�������C���|�[�g���܂���B
   - **Legacy**: �Â��A�j���[�V�����V�X�e�����g�p���܂��B
   - **Generic**: �W�F�l���b�N���O���g�p���܂��B
   - **Humanoid**: �q���[�}�m�C�h���O���g�p���܂��B

2. **Avatar Definition**:
   - **Create From This Model**: ���̃��f������V�����A�o�^�[���쐬���܂��B
   - **Copy From Other Avatar**: ���̃��f������A�o�^�[���R�s�[���܂��B

3. **Root Node**:
   - ���[�g�m�[�h���w�肵�܂��B�A�j���[�V�����̃x�[�X�m�[�h�ƂȂ�܂��B

-----------------------------------------------------------------------------------------------------------------------------
`Optimize Game Objects`
### ��ȋ@�\�ƌ���

1. **�K�w�\���̊ȗ���**�F
   - �A�j���[�V�����ɕs�v�ȃ{�[����I�u�W�F�N�g���폜���邱�ƂŁA�K�w�\�����ȗ�������܂��B����ɂ��A�������g�p�ʂ��팸����A�����^�C���p�t�H�[�}���X�����サ�܂��B

2. **�p�t�H�[�}���X�̌���**�F
   - �s�v�ȃI�u�W�F�N�g���폜����邱�ƂŁA�����_�����O��A�j���[�V�����̌v�Z������������܂��B����ɂ��A���Ƀ��o�C���f�o�C�X�⃊�\�[�X������ꂽ���ł̃p�t�H�[�}���X�����サ�܂��B

3. **�G�f�B�^���̌�����**�F
   - �K�w���ȗ�������邱�ƂŁA�G�f�B�^���ł̑��삪�e�ՂɂȂ�܂��B�V�[���r���[��C���X�y�N�^�ł̑��삪�v���ɍs����悤�ɂȂ�܂��B

### �g�p���@

`Optimize Game Objects` ���g�p����ɂ́AUnity�̃C���X�y�N�^�Ń��f���̃C���|�[�g�ݒ�𒲐����܂��B�ȉ��͂��̎菇�ł��B

1. **���f���̃C���|�[�g�ݒ�**�F
   - �C���|�[�g���������f�����v���W�F�N�g�r���[�őI�����܂��B
   - �C���X�y�N�^�E�B���h�E�ŃC���|�[�g�ݒ���J���܂��B
   - �uRig�v�^�u��I�����A�uAnimation Type�v���uHumanoid�v�܂��́uGeneric�v�ɐݒ肵�܂��B
   - �uOptimize Game Objects�v�Ƀ`�F�b�N�����܂��B

2. **�K�v�ȃ{�[���̌��J**�F
   - �uOptimize Game Objects�v�Ƀ`�F�b�N������ƁA�uExposed Transforms�v���X�g���\������܂��B
   - �A�j���[�V������X�N���v�g�ŃA�N�Z�X����K�v������{�[����I�u�W�F�N�g�����̃��X�g�ɒǉ����܂��B



### ���ۂ̎g�p��

�Ⴆ�΁AHumanoid�L�����N�^�[���C���|�[�g����ۂɁA�A�j���[�V�����̃p�t�H�[�}���X���œK���������ꍇ�A�ȉ��̎菇���s���܂��F

1. **���f�����C���|�[�g**�F
   - �L�����N�^�[���f����Unity�v���W�F�N�g�ɃC���|�[�g���܂��B

2. **�C���X�y�N�^�Őݒ�**�F
   - �C���X�y�N�^�ŁuRig�v�^�u��I�����A�uAnimation Type�v���uHumanoid�v�ɐݒ�B
   - �uOptimize Game Objects�v�Ƀ`�F�b�N�����܂��B
   - �uExposed Transforms�v�ɕK�v�ȃ{�[���i��F��A���Ȃǁj��ǉ����܂��B

3. **�X�N���v�g����A�N�Z�X**�F
   - �K�v�ȃ{�[���ɃX�N���v�g����A�N�Z�X���܂��B�ȉ��̂悤�ɁA���J���ꂽ�{�[�����擾�ł��܂��B


### �܂Ƃ�

`Optimize Game Objects` �́AUnity�ɂ�����L�����N�^�[�A�j���[�V�����̍œK���ɔ��ɗL���ȃc�[���ł��B�s�v�ȃ{�[����I�u�W�F�N�g���폜���邱�ƂŁA�������g�p�ʂ��팸���A�p�t�H�[�}���X�����コ���܂��B���̋@�\��K�؂Ɏg�p���邱�ƂŁA���\�[�X������ꂽ���ł����i���ȃA�j���[�V�������������邱�Ƃ��ł��܂��B
`StripBones`�́AUnity�ɂ�����X�L�j���O�⃁�b�V���̍œK���v���Z�X�̈ꕔ�Ƃ��Ďg�p�����I�v�V�����ł��B����́A�A�j���[�V�����Ɏg�p����Ă��Ȃ��{�[�������b�V������폜����@�\�ł��B����ɂ��A�������g�p�ʂ��팸����A�����^�C���̃p�t�H�[�}���X�����シ�邱�Ƃ�����܂��B

-----------------------------------------------------------------------------------------------------------------------------

### StripBones�̎�ȖړI�ƌ���

1. **�s�v�ȃ{�[���̍팸**�F
   - �A�j���[�V�����Ɋ�^���Ȃ��{�[�����폜���邱�ƂŁA���b�V���f�[�^���y�ʉ�����܂��B���ɁA���G�ȃ��O�⑽���̃{�[�������L�����N�^�[�ɂ����Č��ʓI�ł��B

2. **�������g�p�ʂ̍팸**�F
   - �s�v�ȃ{�[�����폜����邱�ƂŁA�������g�p�ʂ��������A���Ƀ��o�C���f�o�C�X�⃊�\�[�X������ꂽ���ł̃p�t�H�[�}���X�����サ�܂��B

3. **�p�t�H�[�}���X�̌���**�F
   - �����^�C���ł̃{�[���ό`�v�Z�̕��ׂ��y������A�`��̃p�t�H�[�}���X�����サ�܂��B

### �g�p���@

`StripBones`�͒ʏ�AUnity�̃C���|�[�g�ݒ��G�f�B�^�[�g���Ŏg�p����܂��B�ȉ��́A���f���̃C���|�[�g�ݒ��`StripBones`�I�v�V������L���ɂ���菇�ł��B

1. **���f���̃C���|�[�g�ݒ�**�F
   - ���f����I�����A�C���X�y�N�^�[�E�B���h�E�ŃC���|�[�g�ݒ��\�����܂��B
   - �uRig�v�^�u��I�����A�uAnimation Type�v���uHumanoid�v�܂��́uGeneric�v�ɐݒ肵�܂��B
   - �uOptimize Game Objects�v���`�F�b�N���A�uExposed Transforms�v�ŕK�v�ȃ{�[�����w�肵�܂��B

2. **�X�N���v�g���g�p����`StripBones`��L���ɂ���**�F
   - �X�N���v�g���g�p���ă��b�V�����œK�����A�s�v�ȃ{�[�����폜���邱�Ƃ��ł��܂��B�ȉ��́A���̈��ł��B



### �܂Ƃ�

`StripBones`�́AUnity�̃X�L�����b�V���œK���̈�Ƃ��āA�s�v�ȃ{�[�����폜���邽�߂֗̕��ȋ@�\�ł��B���̋@�\���g�p���邱�ƂŁA�������g�p�ʂ��팸���A�p�t�H�[�}���X�����コ���邱�Ƃ��ł��܂��B���ɕ��G�ȃ��O��L�����N�^�[�������ꍇ�ɂ́A`StripBones`��K�؂ɗ��p���邱�ƂŁA�����I�ȃQ�[���J�����\�ƂȂ�܂��B
`SkinWeights`�́AUnity�ɂ����ăX�L�j���O�i�L�����N�^�[�̃X�L���⃁�b�V���̕ό`�j���s���ۂɎg�p�����ݒ�ł��B�X�L���E�F�C�g�́A�e���_�ɑ΂��ĉe����^����{�[���i���j�̐��ƁA���̉e���̋��������肵�܂��B�X�L�j���O�́A�L�����N�^�[�A�j���[�V�����̏d�v�ȗv�f�ł���A���ɕ��G�ȃL�����N�^�[�⃊�O�������ꍇ�ɂ́A�����I�ȃX�L���E�F�C�g�ݒ肪�K�v�ł��B

### �X�L���E�F�C�g�̐ݒ�I�v�V����

Unity�̃X�L���E�F�C�g�ɂ́A��Ɉȉ��̐ݒ�I�v�V����������܂��F

1. **One Bone (1 Bone)**:
   - �e���_���e�����󂯂�̂�1�̃{�[���݂̂ł��B
   - �ł������I�ł����A�f�B�t�H�[���[�V���������ɒP���ŁA���炩�ȕό`�������Ȃ��ꍇ������܂��B

2. **Two Bones (2 Bones)**:
   - �e���_���e�����󂯂�͍̂ő�2�̃{�[���ł��B
   - �ꕔ�̕ό`�ɂ͏\���ł����A���G�ȃ��O�ɂ͕s�����ł��B

3. **Four Bones (4 Bones)**:
   - �e���_���e�����󂯂�͍̂ő�4�̃{�[���ł��B
   - ���i���ȕό`���\�ŁA�����̃L�����N�^�[�A�j���[�V�����ɓK���Ă��܂��B

4. **Unlimited (������)**:
   - �e���_���e�����󂯂�{�[���̐��ɐ���������܂���B
   - �ō��i���̕ό`���\�ł����A�p�t�H�[�}���X�ɉe����^����\��������܂��B

### �X�N���v�g�ł̃X�L���E�F�C�g�ݒ�

Unity�̃X�N���v�g���g�p���ăX�L���E�F�C�g��ݒ肷�邱�Ƃ��ł��܂��B�Ⴆ�΁A`SkinnedMeshRenderer`�R���|�[�l���g��`quality`�v���p�e�B���g�p���ăX�L���E�F�C�g��ݒ肵�܂��B



### �X�L���E�F�C�g�̎��ۂ̌v�Z

�X�L���E�F�C�g�́A�e���_�̕ό`���v�Z����ۂɎg�p����܂��B���_�̍ŏI�ʒu�́A�e�{�[���̕ό`�}�g���b�N�X�ƃX�L���E�F�C�g�̑g�ݍ��킹�ɂ���Čv�Z����܂��B�ȉ��́A���_�̍ŏI�ʒu���v�Z����[���R�[�h�ł��F



### �܂Ƃ�

`SkinWeights`�́A�L�����N�^�[�A�j���[�V�����ɂ����ďd�v�Ȗ������ʂ����܂��B�K�؂ȃX�L���E�F�C�g�̐ݒ�́A�L�����N�^�[�̕ό`�i���ƃp�t�H�[�}���X�ɒ��ډe�����܂��BUnity�ł́A�p�r��p�t�H�[�}���X�v���ɉ����ăX�L���E�F�C�g�𒲐����邱�Ƃ��ł��܂��B��ʓI�ɁA���G�ȃL�����N�^�[�ɂ͑����̃{�[�����g�p����ݒ�i�Ⴆ��4�̃{�[���j���K���Ă��܂����A�p�t�H�[�}���X�̃g���[�h�I�t���l������K�v������܂��B
### Animation �^�u

1. **Import Animation**:
   - **Enabled**: �A�j���[�V�������C���|�[�g���܂��B

2. **Animation Compression**:
   - **Off**: ���k���܂���B
   - **Keyframe Reduction**: �L�[�t���[���̐������炵�܂��B
   - **Keyframe Reduction and Compression**: �L�[�t���[���̍팸�ƈ��k���s���܂��B
   - **Optimal**: �œK�Ȉ��k���@���g�p���܂��B

3. **Animation Clip Settings**:
   - **Loop Time**: �A�j���[�V���������[�v�����܂��B
   - **Loop Pose**: ���[�v�̃X���[�Y����ۂ��߂Ƀ|�[�Y�𒲐����܂��B
   - **Cycle Offset**: ���[�v�̃I�t�Z�b�g��ݒ肵�܂��B
   - **Additive Reference Pose**: ���Z�I�A�j���[�V�����̊�|�[�Y��ݒ肵�܂��B

4. **Root Transform Rotation**:
   - **Bake Into Pose**: ��]���|�[�Y�Ƀx�C�N���܂��B
   - **Based Upon**: ��]�̊��ݒ肵�܂��iOriginal, Body Orientation�Ȃǁj�B

5. **Root Transform Position (Y)**:
   - **Bake Into Pose**: Y���W�̈ʒu���|�[�Y�Ƀx�C�N���܂��B
   - **Based Upon**: �ʒu�̊��ݒ肵�܂��iOriginal, Center of Mass�Ȃǁj�B

6. **Root Transform Position (XZ)**:
   - **Bake Into Pose**: XZ���W�̈ʒu���|�[�Y�Ƀx�C�N���܂��B
   - **Based Upon**: �ʒu�̊��ݒ肵�܂��iOriginal, Center of Mass�Ȃǁj�B

### Materials �^�u

1. **Import Materials**:
   - **Enabled**: �}�e���A�����C���|�[�g���܂��B

2. **Material Naming**:
   - **By Base Texture Name**: �x�[�X�e�N�X�`���̖��O�Ń}�e���A���𖽖����܂��B
   - **From Model's Material**: ���f���̃}�e���A�������g�p���܂��B

3. **Material Search**:
   - **Local Materials Folder**: ���[�J���̃}�e���A���t�H���_�[���猟�����܂��B
   - **Recursive Up**: ��ʂ̃t�H���_�[���ċA�I�Ɍ������܂��B
   - **Project-Wide**: �v���W�F�N�g�S�̂��猟�����܂��B

### ���̑��̃I�v�V����

1. **Import Constraints**:
   - **Enabled**: ���f���̐���i�R���X�g���C���g�j���C���|�[�g���܂��B

### �܂Ƃ�

Unity��3D���f���C���|�[�g�ݒ�ɂ͑����̃I�v�V����������A������K�؂ɐݒ肷�邱�ƂŁA���f���̌����ڂ�p�t�H�[�}���X���œK���ł��܂��B��L�̐������Q�l�ɂ��āA�v���W�F�N�g�̃j�[�Y�ɍ��킹���œK�Ȑݒ���s���Ă��������B
 */
public class modelImportSettings: MonoBehaviour
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
