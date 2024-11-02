using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 Unityのブレンドツリー（Blend Tree）は、複数のアニメーションを一つのパラメータに基づいてブレンドするための仕組みです。これにより、スムーズなアニメーション遷移が可能になります。ブレンドツリーのインスペクターの各セクションについて説明します。

### ブレンドツリーのインスペクターの構成

1. **Blend Tree ノード**
   - **名前:** ブレンドツリーの名前を表示します。デフォルトでは `Blend Tree` という名前が付きます。
   - **Blend Type:** どのタイプのブレンドを使用するかを選択します。以下のオプションがあります。
     - **1D:** 単一のパラメータに基づいてブレンドします。
     - **2D Simple Directional:** 二つのパラメータに基づいてブレンドします。アニメーションは方向に基づいて配置されます。
     - **2D Freeform Directional:** 二つのパラメータに基づいてブレンドします。アニメーションは自由に配置されます。
     - **2D Freeform Cartesian:** 二つのパラメータに基づいてブレンドします。アニメーションはグリッドに沿って配置されます。

2. **Blend Parameters**
   - **Blend Parameter:** ブレンドに使用するパラメータを選択します。1Dの場合は一つ、2Dの場合は二つのパラメータを指定します。
     - **Parameter:** アニメーターコントローラーのパラメータリストから選択します。
     - **Threshold:** ブレンドが始まるしきい値を設定します。例えば、速度が特定の値に達したときにアニメーションが切り替わるように設定できます。

3. **Motions (モーション)**
   - ブレンドツリーに含めるアニメーションクリップやサブブレンドツリーを追加します。
     - **Motion:** ブレンドに含めるアニメーションクリップやサブブレンドツリーを設定します。
     - **Threshold:** それぞれのモーションがどのしきい値で再生されるかを設定します（1Dの場合）。
     - **Position:** 2Dブレンドツリーの場合、各モーションの座標を設定します。

4. **Visualization (可視化)**
   - ブレンドツリーのビジュアル表示。アニメーションがどのようにブレンドされるかを視覚的に確認できます。特に2Dブレンドツリーの場合、各アニメーションの位置を確認します。


5. **Adjustments and Fine-tuning (調整と微調整)**
   - **Blend Weights:** ブレンドツリーが再生されているときに、各モーションのブレンドウェイトを表示します。これにより、どのアニメーションがどれくらいの割合で再生されているかを確認できます。
   - **Synchronize Motion:** 複数のモーションを同期させるかどうかを選択します。例えば、歩行と走行のアニメーションを同期させたい場合に使用します。

### 具体例: 1D ブレンドツリーの設定

1. **Blend Type:** 1D
2. **Blend Parameter:** `speed`
3. **Motions:**
   - `idle` (Threshold: 0)
   - `walk` (Threshold: 1)
   - `run` (Threshold: 2)

この設定では、`speed` パラメータが0のときは `idle` アニメーションが再生され、1のときは `walk`、2のときは `run` アニメーションが再生されます。また、`speed` パラメータが0と1の間の値を取る場合、`idle` と `walk` のアニメーションがブレンドされます。同様に、`speed` が1と2の間の値を取る場合は `walk` と `run` のアニメーションがブレンドされます。

### 具体例: 2D ブレンドツリーの設定

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

この設定では、`horizontal` と `vertical` のパラメータに基づいてアニメーションがブレンドされます。例えば、キャラクターが前方に進む場合 (`vertical` が1)、`walk_forward` アニメーションが再生され、左に進む場合 (`horizontal` が-1)、`walk_left` アニメーションが再生されます。これらのパラメータが中間の値を取る場合、適切なアニメーションがブレンドされます。

### よくあるトラブルシューティング

1. **パラメータが動いていない場合:**
   - スクリプトでパラメータを正しく設定しているか確認します。例えば、`animator.SetFloat("speed", speed);` のようにアニメーターパラメータが更新されていることを確認します。
   - アニメーターウィンドウで、ブレンドパラメータが正しく動作しているかデバッグします。

2. **アニメーションが期待通りにブレンドされない場合:**
   - ブレンドツリーの設定を再確認し、各モーションのしきい値や位置が正しいか確認します。
   - アニメーションのトランジションや条件も確認し、不要なトランジションがないか確認します。

これらの設定と確認を通じて、ブレンドツリーのインスペクターを効果的に使用し、スムーズなアニメーション遷移を実現できるはずです。
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
