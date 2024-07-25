using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 ### マテリアルシェーダーのインスペクター詳細

#### シェーダーの基本設定
Unityのマテリアルシェーダーは、シェーダーコードを使用してオブジェクトの外観を定義します。以下は、インスペクターで一般的に見られるシェーダー設定項目の詳細です：

1. **Shader (シェーダー)**
   - マテリアルに使用するシェーダーを選択します。シェーダーは、オブジェクトのレンダリング方法を決定するコードです。Unityには標準シェーダー、PBRシェーダー、カスタムシェーダーなどが含まれます。

2. **Main Maps (メインマップ)**
   - **Albedo (アルベド)**: オブジェクトの基本色を設定します。テクスチャと色を組み合わせることができます。
   - **Metallic (メタリック)**: オブジェクトのメタリック感を調整します。0は非金属、1は完全な金属を意味します。
   - **Smoothness (スムーズネス)**: 表面の滑らかさを調整します。高い値は鏡のような反射を示し、低い値は粗い表面を示します。
   - **Normal Map (ノーマルマップ)**: 表面の細かい凹凸をシミュレートします。
   - **Height Map (ハイトマップ)**: 表面の高さを調整します。ディスプレイスメントマッピングに使用されます。
   - **Occlusion Map (オクルージョンマップ)**: 環境閉塞を表現します。影の詳細を強調するために使用されます。
   - **Emission Map (エミッションマップ)**: 自発光する部分を設定します。光を放つ部分を表現するために使用されます。
   - **Detail Mask (ディテールマスク)**: 詳細マップの適用領域をマスクします。

3. **Secondary Maps (セカンダリマップ)**
   - セカンダリマップは、メインマップに加えて使用される追加のテクスチャマップです。詳細なテクスチャやノーマルマップを設定できます。

#### シェーダープロパティ
シェーダープロパティは、シェーダーコード内で使用されるパラメータで、インスペクターで調整可能です。以下は、一般的なシェーダープロパティの例です：

- **_MainTex ("Main Texture", 2D)**: メインテクスチャの設定。テクスチャのスロットにドラッグ＆ドロップします。
- **_Color ("Color", Color)**: 基本色の設定。色選択パレットから選択できます。
- **_Gloss ("Glossiness", Range(0, 1))**: グロス値の設定。滑らかさを調整するために使用されます。
- **_Metallic ("Metallic", Range(0, 1))**: メタリック値の設定。メタリック感を調整します。

#### サーフェスオプション
サーフェスオプションは、シェーダーがどのようにレンダリングされるかを詳細に設定する項目です：

- **Surface Type (サーフェスタイプ)**: 不透明（Opaque）か透明（Transparent）を選択します。
- **Blending Mode (ブレンドモード)**: 透明サーフェスタイプの場合のブレンディングモードを選択します。Alpha, Premultiply, Additive, Multiplyのいずれかを選べます。
- **Render Face (レンダーフェイス)**: 描画面を設定します。Front（前面）, Back（背面）, Both（両面）から選択します。
- **Alpha Clipping (アルファクリッピング)**: アルファ値が特定のしきい値を下回るピクセルを除去します。完全な透明を実現するために使用されます。

### カスタムシェーダーの設定
カスタムシェーダーを使用すると、特定の機能やエフェクトを追加するために独自のシェーダーコードを作成できます。以下は、カスタムシェーダーの設定例です：

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

このカスタムシェーダーでは、基本的なアルベドカラー、メインテクスチャ、グロス、メタリック値を設定しています。

### 参考資料
- [Unity Shader Documentation](https://docs.unity3d.com/Manual/Shaders.html)
- [Unity Shader Graph](https://docs.unity3d.com/Packages/com.unity.shadergraph@10.3/manual/ShaderGraph.html)
- [Custom Shader Development](https://docs.unity3d.com/Manual/SL-Reference.html)

これらの設定を理解し、適切に使用することで、シーンの視覚品質とパフォーマンスを最適化することができます。
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
