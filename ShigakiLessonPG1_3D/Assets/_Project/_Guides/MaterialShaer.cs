using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 ### �}�e���A���V�F�[�_�[�̃C���X�y�N�^�[�ڍ�

#### �V�F�[�_�[�̊�{�ݒ�
Unity�̃}�e���A���V�F�[�_�[�́A�V�F�[�_�[�R�[�h���g�p���ăI�u�W�F�N�g�̊O�ς��`���܂��B�ȉ��́A�C���X�y�N�^�[�ň�ʓI�Ɍ�����V�F�[�_�[�ݒ荀�ڂ̏ڍׂł��F

1. **Shader (�V�F�[�_�[)**
   - �}�e���A���Ɏg�p����V�F�[�_�[��I�����܂��B�V�F�[�_�[�́A�I�u�W�F�N�g�̃����_�����O���@�����肷��R�[�h�ł��BUnity�ɂ͕W���V�F�[�_�[�APBR�V�F�[�_�[�A�J�X�^���V�F�[�_�[�Ȃǂ��܂܂�܂��B

2. **Main Maps (���C���}�b�v)**
   - **Albedo (�A���x�h)**: �I�u�W�F�N�g�̊�{�F��ݒ肵�܂��B�e�N�X�`���ƐF��g�ݍ��킹�邱�Ƃ��ł��܂��B
   - **Metallic (���^���b�N)**: �I�u�W�F�N�g�̃��^���b�N���𒲐����܂��B0�͔�����A1�͊��S�ȋ������Ӗ����܂��B
   - **Smoothness (�X���[�Y�l�X)**: �\�ʂ̊��炩���𒲐����܂��B�����l�͋��̂悤�Ȕ��˂������A�Ⴂ�l�͑e���\�ʂ������܂��B
   - **Normal Map (�m�[�}���}�b�v)**: �\�ʂׂ̍������ʂ��V�~�����[�g���܂��B
   - **Height Map (�n�C�g�}�b�v)**: �\�ʂ̍����𒲐����܂��B�f�B�X�v���C�X�����g�}�b�s���O�Ɏg�p����܂��B
   - **Occlusion Map (�I�N���[�W�����}�b�v)**: ���ǂ�\�����܂��B�e�̏ڍׂ��������邽�߂Ɏg�p����܂��B
   - **Emission Map (�G�~�b�V�����}�b�v)**: ���������镔����ݒ肵�܂��B�����������\�����邽�߂Ɏg�p����܂��B
   - **Detail Mask (�f�B�e�[���}�X�N)**: �ڍ׃}�b�v�̓K�p�̈���}�X�N���܂��B

3. **Secondary Maps (�Z�J���_���}�b�v)**
   - �Z�J���_���}�b�v�́A���C���}�b�v�ɉ����Ďg�p�����ǉ��̃e�N�X�`���}�b�v�ł��B�ڍׂȃe�N�X�`����m�[�}���}�b�v��ݒ�ł��܂��B

#### �V�F�[�_�[�v���p�e�B
�V�F�[�_�[�v���p�e�B�́A�V�F�[�_�[�R�[�h���Ŏg�p�����p�����[�^�ŁA�C���X�y�N�^�[�Œ����\�ł��B�ȉ��́A��ʓI�ȃV�F�[�_�[�v���p�e�B�̗�ł��F

- **_MainTex ("Main Texture", 2D)**: ���C���e�N�X�`���̐ݒ�B�e�N�X�`���̃X���b�g�Ƀh���b�O���h���b�v���܂��B
- **_Color ("Color", Color)**: ��{�F�̐ݒ�B�F�I���p���b�g����I���ł��܂��B
- **_Gloss ("Glossiness", Range(0, 1))**: �O���X�l�̐ݒ�B���炩���𒲐����邽�߂Ɏg�p����܂��B
- **_Metallic ("Metallic", Range(0, 1))**: ���^���b�N�l�̐ݒ�B���^���b�N���𒲐����܂��B

#### �T�[�t�F�X�I�v�V����
�T�[�t�F�X�I�v�V�����́A�V�F�[�_�[���ǂ̂悤�Ƀ����_�����O����邩���ڍׂɐݒ肷�鍀�ڂł��F

- **Surface Type (�T�[�t�F�X�^�C�v)**: �s�����iOpaque�j�������iTransparent�j��I�����܂��B
- **Blending Mode (�u�����h���[�h)**: �����T�[�t�F�X�^�C�v�̏ꍇ�̃u�����f�B���O���[�h��I�����܂��BAlpha, Premultiply, Additive, Multiply�̂����ꂩ��I�ׂ܂��B
- **Render Face (�����_�[�t�F�C�X)**: �`��ʂ�ݒ肵�܂��BFront�i�O�ʁj, Back�i�w�ʁj, Both�i���ʁj����I�����܂��B
- **Alpha Clipping (�A���t�@�N���b�s���O)**: �A���t�@�l������̂������l�������s�N�Z�����������܂��B���S�ȓ������������邽�߂Ɏg�p����܂��B

### �J�X�^���V�F�[�_�[�̐ݒ�
�J�X�^���V�F�[�_�[���g�p����ƁA����̋@�\��G�t�F�N�g��ǉ����邽�߂ɓƎ��̃V�F�[�_�[�R�[�h���쐬�ł��܂��B�ȉ��́A�J�X�^���V�F�[�_�[�̐ݒ��ł��F

```shader
Shader "Custom/MyShader" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Main Texture", 2D) = "white" {}
        _Gloss ("Glossiness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            half _Gloss;
            half _Metallic;

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                fixed4 col = tex2D(_MainTex, i.uv) * _Color;
                return col;
            }
            ENDCG
        }
    }
}
```

���̃J�X�^���V�F�[�_�[�ł́A��{�I�ȃA���x�h�J���[�A���C���e�N�X�`���A�O���X�A���^���b�N�l��ݒ肵�Ă��܂��B

### �Q�l����
- [Unity Shader Documentation](https://docs.unity3d.com/Manual/Shaders.html)
- [Unity Shader Graph](https://docs.unity3d.com/Packages/com.unity.shadergraph@10.3/manual/ShaderGraph.html)
- [Custom Shader Development](https://docs.unity3d.com/Manual/SL-Reference.html)

�����̐ݒ�𗝉����A�K�؂Ɏg�p���邱�ƂŁA�V�[���̎��o�i���ƃp�t�H�[�}���X���œK�����邱�Ƃ��ł��܂��B
 */
public class MaterialShaer : MonoBehaviour
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
