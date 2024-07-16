using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 Unityにおける接線ベクトル（Tangent Vector）の計算について説明します。接線ベクトルは、法線マッピング（ノーマルマッピング）を正しく行うために必要な情報です。法線マッピングは、テクスチャを使用して詳細な法線情報を提供し、よりリアルな表面のディテールを表現します。このため、法線とともに接線ベクトルとバイノーマルベクトル（またはビットangent）も必要となります。

### 接線ベクトルの計算方法

1. **頂点データの準備**：
   メッシュの各頂点に対して、位置、法線、UV座標を取得します。これらの情報を使用して接線ベクトルを計算します。

2. **エッジベクトルの計算**：
   三角形の頂点 \( v0, v1, v2 \) を使用して、エッジベクトル \(\mathbf{E1}\) と \(\mathbf{E2}\) を計算します。
   - \(\mathbf{E1} = v1 - v0\)
   - \(\mathbf{E2} = v2 - v0\)

3. **UVスペースのエッジベクトルの計算**：
   同様に、UV座標の差分を計算します。
   - \(\Delta \text{UV1} = \text{UV}_1 - \text{UV}_0\)
   - \(\Delta \text{UV2} = \text{UV}_2 - \text{UV}_0\)

4. **接線とバイノーマルの計算**：
   次に、接線ベクトルとバイノーマルベクトルを計算します。接線ベクトル \(\mathbf{T}\) とバイノーマルベクトル \(\mathbf{B}\) は以下のように求められます：
   \[
   \begin{align*}
   r &= \frac{1.0}{(\Delta \text{UV1}_x \cdot \Delta \text{UV2}_y - \Delta \text{UV1}_y \cdot \Delta \text{UV2}_x)} \\
   \mathbf{T} &= (\Delta \text{UV2}_y \cdot \mathbf{E1} - \Delta \text{UV1}_y \cdot \mathbf{E2}) \cdot r \\
   \mathbf{B} &= (\Delta \text{UV1}_x \cdot \mathbf{E2} - \Delta \text{UV2}_x \cdot \mathbf{E1}) \cdot r
   \end{align*}
   \]

5. **正規化**：
   接線ベクトルとバイノーマルベクトルを正規化します。

6. **メッシュに接線を設定**：
   計算された接線ベクトルをメッシュに設定します。

### 実装例（C#）

以下は、Unityで接線ベクトルを計算し、メッシュに設定するコードの例です。

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

### まとめ

接線ベクトルの計算は、ノーマルマッピングやその他のシェーディング技術において重要な役割を果たします。上記の方法で接線ベクトルを計算し、メッシュに適用することで、よりリアルなライティング効果を得ることができます。Unityでは、手動で接線ベクトルを計算することもできますが、多くの場合、モデリングツールや他のライブラリが自動的にこれを行ってくれます。
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
