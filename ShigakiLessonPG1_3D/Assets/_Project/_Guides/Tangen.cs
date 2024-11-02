using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 Unity�ɂ�����ڐ��x�N�g���iTangent Vector�j�̌v�Z�ɂ��Đ������܂��B�ڐ��x�N�g���́A�@���}�b�s���O�i�m�[�}���}�b�s���O�j�𐳂����s�����߂ɕK�v�ȏ��ł��B�@���}�b�s���O�́A�e�N�X�`�����g�p���ďڍׂȖ@������񋟂��A��胊�A���ȕ\�ʂ̃f�B�e�[����\�����܂��B���̂��߁A�@���ƂƂ��ɐڐ��x�N�g���ƃo�C�m�[�}���x�N�g���i�܂��̓r�b�gangent�j���K�v�ƂȂ�܂��B

### �ڐ��x�N�g���̌v�Z���@

1. **���_�f�[�^�̏���**�F
   ���b�V���̊e���_�ɑ΂��āA�ʒu�A�@���AUV���W���擾���܂��B�����̏����g�p���Đڐ��x�N�g�����v�Z���܂��B

2. **�G�b�W�x�N�g���̌v�Z**�F
   �O�p�`�̒��_ \( v0, v1, v2 \) ���g�p���āA�G�b�W�x�N�g�� \(\mathbf{E1}\) �� \(\mathbf{E2}\) ���v�Z���܂��B
   - \(\mathbf{E1} = v1 - v0\)
   - \(\mathbf{E2} = v2 - v0\)

3. **UV�X�y�[�X�̃G�b�W�x�N�g���̌v�Z**�F
   ���l�ɁAUV���W�̍������v�Z���܂��B
   - \(\Delta \text{UV1} = \text{UV}_1 - \text{UV}_0\)
   - \(\Delta \text{UV2} = \text{UV}_2 - \text{UV}_0\)

4. **�ڐ��ƃo�C�m�[�}���̌v�Z**�F
   ���ɁA�ڐ��x�N�g���ƃo�C�m�[�}���x�N�g�����v�Z���܂��B�ڐ��x�N�g�� \(\mathbf{T}\) �ƃo�C�m�[�}���x�N�g�� \(\mathbf{B}\) �͈ȉ��̂悤�ɋ��߂��܂��F
   \[
   \begin{align*}
   r &= \frac{1.0}{(\Delta \text{UV1}_x \cdot \Delta \text{UV2}_y - \Delta \text{UV1}_y \cdot \Delta \text{UV2}_x)} \\
   \mathbf{T} &= (\Delta \text{UV2}_y \cdot \mathbf{E1} - \Delta \text{UV1}_y \cdot \mathbf{E2}) \cdot r \\
   \mathbf{B} &= (\Delta \text{UV1}_x \cdot \mathbf{E2} - \Delta \text{UV2}_x \cdot \mathbf{E1}) \cdot r
   \end{align*}
   \]

5. **���K��**�F
   �ڐ��x�N�g���ƃo�C�m�[�}���x�N�g���𐳋K�����܂��B

6. **���b�V���ɐڐ���ݒ�**�F
   �v�Z���ꂽ�ڐ��x�N�g�������b�V���ɐݒ肵�܂��B

### ������iC#�j

�ȉ��́AUnity�Őڐ��x�N�g�����v�Z���A���b�V���ɐݒ肷��R�[�h�̗�ł��B

```csharp
void CalculateTangents(Mesh mesh)
{
    int vertexCount = mesh.vertexCount;
    Vector3[] vertices = mesh.vertices;
    Vector3[] normals = mesh.normals;
    Vector2[] uv = mesh.uv;
    int[] triangles = mesh.triangles;
    
    Vector4[] tangents = new Vector4[vertexCount];
    Vector3[] tan1 = new Vector3[vertexCount];
    Vector3[] tan2 = new Vector3[vertexCount];

    for (int i = 0; i < triangles.Length; i += 3)
    {
        int i1 = triangles[i];
        int i2 = triangles[i + 1];
        int i3 = triangles[i + 2];

        Vector3 v1 = vertices[i1];
        Vector3 v2 = vertices[i2];
        Vector3 v3 = vertices[i3];

        Vector2 w1 = uv[i1];
        Vector2 w2 = uv[i2];
        Vector2 w3 = uv[i3];

        float x1 = v2.x - v1.x;
        float x2 = v3.x - v1.x;
        float y1 = v2.y - v1.y;
        float y2 = v3.y - v1.y;
        float z1 = v2.z - v1.z;
        float z2 = v3.z - v1.z;

        float s1 = w2.x - w1.x;
        float s2 = w3.x - w1.x;
        float t1 = w2.y - w1.y;
        float t2 = w3.y - w1.y;

        float r = 1.0f / (s1 * t2 - s2 * t1);
        Vector3 sdir = new Vector3((t2 * x1 - t1 * x2) * r, (t2 * y1 - t1 * y2) * r, (t2 * z1 - t1 * z2) * r);
        Vector3 tdir = new Vector3((s1 * x2 - s2 * x1) * r, (s1 * y2 - s2 * y1) * r, (s1 * z2 - s2 * z1) * r);

        tan1[i1] += sdir;
        tan1[i2] += sdir;
        tan1[i3] += sdir;

        tan2[i1] += tdir;
        tan2[i2] += tdir;
        tan2[i3] += tdir;
    }

    for (int i = 0; i < vertexCount; i++)
    {
        Vector3 n = normals[i];
        Vector3 t = tan1[i];

        Vector3.OrthoNormalize(ref n, ref t);
        tangents[i].x = t.x;
        tangents[i].y = t.y;
        tangents[i].z = t.z;

        tangents[i].w = (Vector3.Dot(Vector3.Cross(n, t), tan2[i]) < 0.0f) ? -1.0f : 1.0f;
    }

    mesh.tangents = tangents;
}
```

### �܂Ƃ�

�ڐ��x�N�g���̌v�Z�́A�m�[�}���}�b�s���O�₻�̑��̃V�F�[�f�B���O�Z�p�ɂ����ďd�v�Ȗ������ʂ����܂��B��L�̕��@�Őڐ��x�N�g�����v�Z���A���b�V���ɓK�p���邱�ƂŁA��胊�A���ȃ��C�e�B���O���ʂ𓾂邱�Ƃ��ł��܂��BUnity�ł́A�蓮�Őڐ��x�N�g�����v�Z���邱�Ƃ��ł��܂����A�����̏ꍇ�A���f�����O�c�[���⑼�̃��C�u�����������I�ɂ�����s���Ă���܂��B
 */
public class Tangen : MonoBehaviour
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
