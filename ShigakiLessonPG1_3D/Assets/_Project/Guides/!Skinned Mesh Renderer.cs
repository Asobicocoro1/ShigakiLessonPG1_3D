using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 ### ���b�V���ƃX�L���̏ڍ�

#### ���b�V�� (Mesh)
���b�V���́A3D�I�u�W�F�N�g�̕\�ʂ��\�����钸�_�̏W���ŁA�ȉ��̗v�f���琬��܂��F
- **���_ (Vertices)**: ���b�V���̊e�|�C���g�B
- **�G�b�W (Edges)**: ���_�Ԃ̐��B
- **�|���S�� (Polygons)**: �G�b�W�ɂ���Ĉ͂܂ꂽ���ʁB�ʏ�͎O�p�`�܂��͎l�p�`�B
- **UV�}�b�s���O (UV Mapping)**: 2D�e�N�X�`�������b�V���̕\�ʂɃ}�b�s���O���邽�߂̍��W�B

#### �X�L�� (Skin)
�X�L���́A���b�V�����{�[���i���j�ɂ���ĕό`����d�g�݂ł��B�e���_�ɂ̓X�L���E�F�C�g�����蓖�Ă��A�ǂ̃{�[���ɂǂ̒��x�e�����󂯂邩�����܂�܂��B�{�[���́A���b�V���̓���̕����𓮂������߂̉��z�I�ȍ��ł��B

#### �X�L�����b�V�������_���[ (Skinned Mesh Renderer)
Skinned Mesh Renderer �́A�X�L�����b�V���������_�����O���邽�߂̃R���|�[�l���g�ŁA�{�[���̕ό`�ɏ]���ă��b�V���𓮂����A�A�j���[�V�������������܂��B

### �C���X�y�N�^�[�̊e���ڂ̐���

1. **Quality (�i��)**
   - ���b�V���̃X�L���j���O�̕i����ݒ肵�܂��B���i���ɐݒ肷��ƁA��芊�炩�ȕό`���\�ł����A�p�t�H�[�}���X�ɉe����^���邱�Ƃ�����܂��B

2. **Update When Offscreen (��ʊO�ł��X�V)**
   - ��ʊO�ɂ���ꍇ�ł����b�V�����X�V���邩�ǂ�����ݒ肵�܂��B�I�t�ɂ���ƁA�p�t�H�[�}���X�����サ�܂��B

3. **Root Bone (���[�g�{�[��)**
   - �X�L�����b�V�����e�����󂯂�ŏ�ʂ̃{�[�����w�肵�܂��B�ʏ�A�L�����N�^�[�̃q�b�v�{�[����y���r�X�{�[�����ݒ肳��܂��B

4. **Bones (�{�[��)**
   - �X�L�����b�V�����e�����󂯂邷�ׂẴ{�[���̃��X�g�ł��B

5. **Bounds (���E)**
   - ���b�V���̋��E��ݒ肵�܂��B�����I�Ɍv�Z����邱�Ƃ�����܂����A�蓮�Őݒ肷�邱�Ƃł�萳�m�ȋ��E���w��ł��܂��B

6. **Blend Shapes (�u�����h�V�F�C�v)**
   - ���b�V���̓���̕�����ό`�����邽�߂̃V�F�C�v�L�[�ł��B��̕\��ȂǁA���ׂȕό`�Ɏg�p����܂��B

### �œK���̃q���g

1. **���b�V���̈��k**
   - �f�B�X�N�X�y�[�X��ߖ񂷂邽�߂Ƀ��b�V�������k���܂��B���k���x���ɂ���ẮA���x�ɉe����^���邱�Ƃ�����܂��B

2. **Read/Write ������**
   - ���b�V���̃������g�p�ʂ����炷���߂ɁARead/Write �I�v�V�����𖳌��ɂ��܂��B

3. **�s�v�ȃ��O�ƃu�����h�V�F�C�v�̖�����**
   - �X�P���g����u�����h�V�F�C�v�A�j���[�V�������s�v�ȏꍇ�A�����̃I�v�V�����𖳌��ɂ��܂��B

4. **�X�L�����b�V���̍œK��**
   - SkinnedMeshRenderer ���g�p���Ă���I�u�W�F�N�g���{���ɕK�v���m�F���܂��B�A�j���[�V�������K�v�Ȃ��ꍇ�ABakeMesh �֐����g�p���ă��b�V����ÓI�ȃ|�[�Y�ɌŒ肵�AMeshRenderer �ɐ؂�ւ��邱�Ƃ��ł��܂��B

### �Q�l����

- **[Optimize SkinnedMeshRenderers](https://docs.unity3d.com/2021.1/Documentation/Manual/class-SkinnedMeshRenderer.html)** �y12:2��JW-10207_Unity_eBook_OptimizeYourMobileGamePerformance_V6_May2021.pdf�z
- **[Skinned Mesh sampling effects](https://docs.unity3d.com/2021.1/Documentation/Manual/class-SkinnedMeshRenderer.html)** �y12:0��The_definitive_guide_to_creating_advanced_visual_effects_in_Unity.pdf�z

�����̐ݒ�ƍœK�����@�𗝉����邱�ƂŁA�L�����N�^�[�A�j���[�V�����̕i���ƃp�t�H�[�}���X�̃o�����X����邱�Ƃ��ł��܂��B



### Skinned Mesh Renderer �C���X�y�N�^�[�p��̏ڍ�

#### Materials (�}�e���A��)
- **Size (�T�C�Y)**: �g�p����}�e���A���̐���ݒ肵�܂��B
- **Element 0, Element 1, ... (�G�������g 0, �G�������g 1, ...)**: �e�}�e���A���X���b�g�Ɋ��蓖�Ă���}�e���A���B�e�X���b�g�ɂ͈قȂ�}�e���A����ݒ�ł��܂��B

#### Lighting (���C�e�B���O)
- **Cast Shadows (�e���L���X�g����)**: �I�u�W�F�N�g���e�𐶐����邩�ǂ�����ݒ肵�܂��B
  - �I�v�V����: **On (�I��)**, **Off (�I�t)**, **Two Sided (����)**

- **Receive Shadows (�e���󂯂�)**: �I�u�W�F�N�g�����̃I�u�W�F�N�g�̉e���󂯂邩�ǂ�����ݒ肵�܂��B

- **Motion Vectors (���[�V�����x�N�^�[)**: ���[�V�����x�N�^�[���g�p���ē����̃u���[���ʂ�L���ɂ��邩�ǂ�����ݒ肵�܂��B
  - �I�v�V����: **Camera (�J����)**, **Object (�I�u�W�F�N�g)**, **None (�Ȃ�)**

- **Skinned Motion Vectors (�X�L�����[�V�����x�N�^�[)**: �X�L�����b�V���̃��[�V�����x�N�^�[���g�p���邩�ǂ�����ݒ肵�܂��B

#### Probes (�v���[�u)
- **Light Probes (���C�g�v���[�u)**: ���I�I�u�W�F�N�g�����C�g�v���[�u�̃f�[�^���g�p���邩�ǂ�����ݒ肵�܂��B
  - �I�v�V����: **Off (�I�t)**, **Blend Probes (�u�����h�v���[�u)**, **Use Proxy Volume (�v���L�V�{�����[�����g�p)**

- **Reflection Probes (���t���N�V�����v���[�u)**: ���̃��t���N�V�������L���v�`�����Ďg�p���邩�ǂ�����ݒ肵�܂��B
  - �I�v�V����: **Off (�I�t)**, **Blend Probes (�u�����h�v���[�u)**, **Blend Probes and Skybox (�u�����h�v���[�u�ƃX�J�C�{�b�N�X)**, **Simple (�V���v��)**

#### Additional Settings (�A�f�B�V���i���Z�b�e�B���O)
- **Update When Offscreen (��ʊO�ł��X�V)**: �I�u�W�F�N�g����ʊO�ɂ���ꍇ�ł����b�V�����X�V���邩�ǂ�����ݒ肵�܂��B

- **Bounds (���E)**
  - **Center (���S)**: ���E�{�b�N�X�̒��S�ʒu��ݒ肵�܂��B
  - **Size (�T�C�Y)**: ���E�{�b�N�X�̑傫����ݒ肵�܂��B

- **Quality (�i��)**: ���b�V���̃X�L���j���O�̕i����ݒ肵�܂��B
  - �I�v�V����: **Auto (����)**, **High (���i��)**, **Medium (���i��)**, **Low (��i��)**

### ���Ԃɐݒ荀�ڂ̏ڍׂƂ��̌���

1. **Materials (�}�e���A��)**
   - **Size (�T�C�Y)**: �g�p����}�e���A���̐���ݒ肵�A�e�X���b�g�Ɋ��蓖�Ă܂��B
   - **Element 0, Element 1, ... (�G�������g 0, �G�������g 1, ...)**: �e�X���b�g�ɈقȂ�}�e���A����ݒ肷�邱�ƂŁA���b�V���̌����ڂ��J�X�^�}�C�Y�ł��܂��B

2. **Lighting (���C�e�B���O)**
   - **Cast Shadows (�e���L���X�g����)**: �I�u�W�F�N�g���e�𐶐����邩�ݒ肵�A�V�[���̃��A���Y�������コ���܂��B
   - **Receive Shadows (�e���󂯂�)**: ���̃I�u�W�F�N�g�̉e���󂯂邱�ƂŁA�I�u�W�F�N�g�̊O�ς��V�[�����ł�莩�R�ɂȂ�܂��B
   - **Motion Vectors (���[�V�����x�N�^�[)**: ���[�V�����u���[���ʂ�L���ɂ��A�����̊��炩�������コ���܂��B
   - **Skinned Motion Vectors (�X�L�����[�V�����x�N�^�[)**: �X�L�����b�V���̓����ɑΉ��������[�V�����x�N�^�[���g�p���܂��B

3. **Probes (�v���[�u)**
   - **Light Probes (���C�g�v���[�u)**: ���C�g�v���[�u���g�p���āA���I�I�u�W�F�N�g�̃��C�e�B���O�����P���܂��B
   - **Reflection Probes (���t���N�V�����v���[�u)**: ���̃��t���N�V�������L���v�`�����A���A���Ȕ��˂��쐬���܂��B

4. **Additional Settings (�A�f�B�V���i���Z�b�e�B���O)**
   - **Update When Offscreen (��ʊO�ł��X�V)**: �I�u�W�F�N�g����ʊO�ɂ����Ă��X�V���邩�ǂ�����ݒ肵�A��ɃL�����N�^�[�̈ʒu��A�j���[�V�����𐳂����ۂ��܂��B
   - **Bounds (���E)**: ���E�{�b�N�X�̒��S�ʒu�ƃT�C�Y��ݒ肵�A�����_�����O�̍œK����}��܂��B
   - **Quality (�i��)**: ���b�V���̃X�L���j���O�i����ݒ肵�A���炩�ȕό`�ƃp�t�H�[�}���X�𒲐����܂��B

�����̐ݒ�𗝉����A�v���W�F�N�g�̗v���ɍ��킹�Ē������邱�ƂŁA�V�[���̎��o�i���ƃp�t�H�[�}���X���œK���ł��܂��B

�ׂ����p��ꗗ
### Skinned Mesh Renderer �̏ڍאݒ�ƌ��ʂ̉��

#### �u���[���� (Motion Vectors)
**���[�V�����x�N�^�[ (Motion Vectors)** �́A�����̃u���[���ʂ��������邽�߂Ɏg�p����܂��B����ɂ��A�I�u�W�F�N�g�������ۂɎc���������A��芊�炩�ȓ��������o�I�ɕ\���ł��܂��B

- **Camera (�J����)**: �J�����̓�������Ƀ��[�V�����x�N�^�[�𐶐����܂��B
- **Object (�I�u�W�F�N�g)**: �I�u�W�F�N�g�̓�������Ƀ��[�V�����x�N�^�[�𐶐����܂��B
- **None (�Ȃ�)**: ���[�V�����x�N�^�[���g�p���܂���B

**����**: ���[�V�����u���[���ʂ�L���ɂ���ƁA�����̂���V�[���Ŋ��炩�ȓ����������ł��܂����A�p�t�H�[�}���X�ɉe����^���邱�Ƃ�����܂��B

#### Blend Probes (�u�����h�v���[�u)
**Blend Probes (�u�����h�v���[�u)** �́A�����̃��C�g�v���[�u����̃��C�e�B���O�f�[�^���u�����h���āA���I�I�u�W�F�N�g�ɓK�p����@�\�ł��B����ɂ��A�V�[�����̈قȂ�Ɩ����������R�ɓK�����邱�Ƃ��ł��܂��B

**����**: ���C�g�v���[�u���u�����h���邱�ƂŁA���I�I�u�W�F�N�g���V�[���̏Ɩ������ɂ�胊�A���ɓK�����A���炩�ȃ��C�e�B���O�J�ڂ��������܂��B

#### Use Proxy Volume (�v���L�V�{�����[�����g�p)
**Use Proxy Volume (�v���L�V�{�����[�����g�p)** �́A���G�ȃ��C�g�v���[�u�̔z�u���V���v���ȃ{�����[���ɒu�������A�v�Z���ȗ�������@�\�ł��B�v���L�V�{�����[���́A���G�ȃV�[���ł������I�Ƀ��C�e�B���O���v�Z���邽�߂Ɏg�p����܂��B

**����**: �v���L�V�{�����[�����g�p���邱�ƂŁA�v�Z�R�X�g���팸���Ȃ�����A�K�؂ȃ��C�e�B���O��񋟂ł��܂��B���ɑ�K�͂ȃV�[���⑽���̃��C�g�v���[�u������ꍇ�ɗL���ł��B

#### �X�L���j���O�̃��[�V�����x�N�^�[
**�X�L���j���O�̃��[�V�����x�N�^�[ (Skinned Motion Vectors)** �́A�X�L�����b�V���̓����ɑΉ����郂�[�V�����x�N�^�[�𐶐����܂��B����́A�X�L�����b�V�����ό`����ۂ̓����𐳊m�ɔ��f���邽�߂Ɏg�p����܂��B

**����**: �X�L�����b�V���̃A�j���[�V���������炩�ɕ\������A�����̃u���[���ʂ����A���ɍČ�����܂��B����ɂ��A�L�����N�^�[�̃A�j���[�V��������莩�R�Ɍ����܂��B

#### Light Probes (���C�g�v���[�u)
**Light Probes (���C�g�v���[�u)** �́A�V�[�����̓���̃|�C���g�ł̌��̏����L���v�`�����A���I�I�u�W�F�N�g�̃��C�e�B���O�����P���邽�߂Ɏg�p����܂��B���C�g�v���[�u�̃f�[�^���u�����h���ēK�p���邱�ƂŁA���I�I�u�W�F�N�g�����͂̏Ɩ������Ɏ��R�ɓK�����܂��B

**����**: ���C�g�v���[�u���g�p���邱�ƂŁA�V�[���̃��C�e�B���O�����I�I�u�W�F�N�g�ɑ΂��Ă����m�ɓK�p����A���A���ȃ��C�e�B���O���ʂ𓾂邱�Ƃ��ł��܂��B

#### Reflection Probes (���t���N�V�����v���[�u)
**Reflection Probes (���t���N�V�����v���[�u)** �́A���̃��t���N�V�������L���v�`�����A�I�u�W�F�N�g�ɔ��f���邽�߂̃f�[�^��񋟂��܂��B���t���N�V�����v���[�u�́A���A���Ȕ��ˌ��ʂ��쐬���邽�߂ɏd�v�ł��B

- **Off (�I�t)**: ���t���N�V�����v���[�u���g�p���܂���B
- **Blend Probes (�u�����h�v���[�u)**: �����̃��t���N�V�����v���[�u�̃f�[�^���u�����h���Ďg�p���܂��B
- **Blend Probes and Skybox (�u�����h�v���[�u�ƃX�J�C�{�b�N�X)**: ���t���N�V�����v���[�u�ƃX�J�C�{�b�N�X�̃f�[�^���u�����h���Ďg�p���܂��B
- **Simple (�V���v��)**: �P��̃��t���N�V�����v���[�u���g�p���܂��B

**����**: ���t���N�V�����v���[�u���g�p���邱�ƂŁA�I�u�W�F�N�g�\�ʂɃ��A���Ȕ��˂��\������A���̏ڍׂ����f����܂��B����ɂ��A���^����K���X�̂悤�ȕ\�ʂ̃I�u�W�F�N�g�����A���Ɍ����܂��B

�����̐ݒ�𗝉����A�K�؂Ɏg�p���邱�ƂŁA�V�[���̎��o�i���ƃp�t�H�[�}���X���œK�����邱�Ƃ��ł��܂��B
 */
public class SkinnedMeshRenderer : MonoBehaviour
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
