using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 Unity�ł̖@���v�Z�����ɂ��Đ������܂��B�@���x�N�g���iNormal Vector�j�́A���b�V���̊e���_��ʂ̌�����\���d�v�ȗv�f�ŁA���C�e�B���O��V�F�[�f�B���O�ɉe����^���܂��B�@���̌v�Z�����ɂ͂������̕��@������A���ꂼ��̗p�r��ړI�ɉ����đI������܂��B

### �@���v�Z�̊�{
�@���x�N�g���́A3D���f���̖ʂ��ǂ���̕����������Ă��邩�������܂��B�ʏ�A���_�@���iVertex Normal�j�Ɩʖ@���iFace Normal�j������A�����͈ȉ��̕��@�Ōv�Z����܂��B

### �ʖ@���iFace Normal�j
�ʖ@���́A�|���S���i�ʏ�͎O�p�`�j�̖ʂ��ƂɌv�Z����܂��B�e�O�p�`�̖@���́A���̖ʂ̐���������\���܂��B

**�v�Z���@**�F
1. �O�p�`�̒��_ \(v0\), \(v1\), \(v2\) ���`���܂��B
2. ���_�Ԃ̃x�N�g�����v�Z���܂��B
   - \(\mathbf{u} = v1 - v0\)
   - \(\mathbf{v} = v2 - v0\)
3. �@���x�N�g�� \(\mathbf{n}\) �́A\(\mathbf{u}\) �� \(\mathbf{v}\) �̊O�ςŋ��߂��܂��B
   - \(\mathbf{n} = \mathbf{u} \times \mathbf{v}\)
4. �@���x�N�g���𐳋K�����܂��i������1�ɂ��܂��j�B

### ���_�@���iVertex Normal�j
���_�@���́A���b�V���̊��炩�ȊO�ς��쐬���邽�߂Ɏg�p����܂��B���_�@���́A���̒��_�����L���邷�ׂĂ̖ʖ@���̕��ςƂ��Čv�Z����܂��B

**�v�Z���@**�F
1. �e���_�ɑ΂��āA���L���Ă��邷�ׂĂ̖ʂ̖@�������W���܂��B
2. �����̖ʖ@���𕽋ω����܂��B
3. ���ω����ꂽ�@���x�N�g���𐳋K�����܂��B

### �@���̃��L�����L�����[�V�����iRecalculation�j
Unity�ł́A���b�V���̖@���������I�ɍČv�Z���邽�߂̋@�\���񋟂���Ă��܂��B����ɂ��A���f���̌`���g�|���W�[�̕ύX�ɉ����Ė@�����X�V���邱�Ƃ��ł��܂��B

```csharp
// ���b�V���̖@�����Čv�Z�����
Mesh mesh = GetComponent<MeshFilter>().mesh;
mesh.RecalculateNormals();
```

### �J�X�^���@���v�Z
�ꍇ�ɂ���ẮA�J�X�^���̖@���v�Z���K�v�ɂȂ邱�Ƃ�����܂��B����ɂ́A�X���[�Y�Ȗ@�������̌��ʂ��o�����߂̃J�X�^���V�F�[�_�[���g�p���邱�Ƃ��܂܂�܂��B

### �����@���iSmooth Normals�j�ƃt���b�g�@���iFlat Normals�j
- **�����@���iSmooth Normals�j**: ���f���̕\�ʂ����炩�Ɍ����邽�߂Ɏg�p����܂��B���_�@�����g�p���A�אڂ���ʂ̖@���𕽋ω����܂��B
- **�t���b�g�@���iFlat Normals�j**: �ʂ��ƂɈقȂ�@���������߁A�G�b�W���͂�����ƌ����܂��B�ʖ@�������̂܂܎g�p���܂��B

### �܂Ƃ�
�@���v�Z��3D�O���t�B�b�N�X�̊�{�I�ȋZ�p�ł���A���f���̌��h����傫�����E���܂��BUnity�ł́A�@���̍Čv�Z��J�X�^���@���̐ݒ肪�ȒP�ɍs���邽�߁A�ړI�ɉ������K�؂Ȗ@���v�Z������I�����邱�Ƃ��d�v�ł��B
 */
public class Normal : MonoBehaviour
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
