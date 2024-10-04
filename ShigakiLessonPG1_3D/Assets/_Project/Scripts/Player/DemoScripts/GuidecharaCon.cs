using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 ### 解説内容の整理

#### 1. `movementDirection.Normalize()`
`Normalize()`は、ベクトルの長さ（マグニチュード）を1にする関数です。ベクトルの方向を保ちながら、その長さを標準化します。

- **目的**: 移動方向のベクトルを正規化し、速度計算の際に一貫した移動を実現するため。
- **例**: 
  ```csharp
  Vector3 movementDirection = new Vector3(3, 0, 4); // ベクトル (3, 0, 4)
  movementDirection.Normalize(); // ベクトル (0.6, 0, 0.8) になり、長さが 1 になる
  ```

#### 2. `float actualSpeed = Mathf.Abs(speed) == 1f ? runSpeed : walkSpeed;`
三項演算子を使用して、実際の移動速度を設定する行です。

- **目的**: `speed`が1または-1の場合、走行速度`runSpeed`を設定し、それ以外の場合は歩行速度`walkSpeed`を設定する。
- **例**:
  ```csharp
  float speed = 1f; // ダッシュ状態の場合
  float actualSpeed = Mathf.Abs(speed) == 1f ? runSpeed : walkSpeed; // actualSpeedはrunSpeedになる
  
  speed = 0.5f; // 歩行状態の場合
  actualSpeed = Mathf.Abs(speed) == 1f ? runSpeed : walkSpeed; // actualSpeedはwalkSpeedになる
  ```

#### 3. `transform.Translate(movementDirection * actualSpeed * Time.deltaTime, Space.World)`
`transform.Translate`は、オブジェクトの位置を指定された距離だけ移動させる関数です。

- **目的**: 正規化された方向ベクトルに実際の移動速度と経過時間を掛け合わせることで、キャラクターを一定の速度で指定方向に移動させる。
- **パラメーター**:
  - `movementDirection`: 正規化された方向ベクトル。
  - `actualSpeed`: 実際の移動速度（走行速度または歩行速度）。
  - `Time.deltaTime`: 前フレームからの経過時間。フレームレートに依存しない移動を実現するために使用します。
  - `Space.World`: 世界座標系での移動を指定します。
- **例**:
  ```csharp
  transform.Translate(Vector3.forward * 5f); // オブジェクトを前方に5単位移動させる
  ```

### 全体の流れ
```csharp
movementDirection.Normalize(); // 移動方向を正規化して方向ベクトルの長さを1にする
float actualSpeed = Mathf.Abs(speed) == 1f ? runSpeed : walkSpeed; // 速度が1か-1の場合は走行速度、それ以外は歩行速度を設定
transform.Translate(movementDirection * actualSpeed * Time.deltaTime, Space.World); // 正規化された移動方向に対して実際の移動速度を掛けて、毎フレーム移動する距離を計算し、キャラクターを移動
```

#### 各ステップの目的

1. **`movementDirection.Normalize()`**:
   - 移動方向のベクトルを正規化し、方向を保ちながら長さを1にします。これにより、移動速度が一貫して計算されます。

2. **`float actualSpeed = Mathf.Abs(speed) == 1f ? runSpeed : walkSpeed;`**:
   - `speed`が1または-1の場合に走行速度を設定し、それ以外の場合に歩行速度を設定します。これにより、キャラクターの移動速度を動作に応じて調整します。

3. **`transform.Translate(movementDirection * actualSpeed * Time.deltaTime, Space.World);`**:
   - 正規化された移動方向ベクトルに実際の移動速度と前フレームからの経過時間を掛けて、キャラクターをその方向に一定の速度で移動させます。

この一連の操作により、キャラクターは指定された方向に一貫した速度で移動します。これにより、移動のスムーズさと正確さが保証されます。

Quaternion.LookRotationの詳細な解説と整理
基本的な使い方
Quaternion.LookRotation は、指定された方向を向くための回転を計算するメソッドです。このメソッドは、特定の方向を向くためのクォータニオンを生成します。

csharp
コードをコピーする
Quaternion rotation = Quaternion.LookRotation(Vector3.forward);
このコードは、Vector3.forward（世界座標系でのZ軸方向）を向く回転を生成します。

使用例と詳細
パラメーター:

Vector3 forward: 向かせたい方向のベクトル。
Vector3 upwards (省略可能): 上方向を定義するベクトル。デフォルトはVector3.up。
戻り値: 指定された方向を向くためのクォータニオン。

理由
Quaternion.LookRotation を使用することで、キャラクターやオブジェクトを指定した方向に向けることができます。これにより、移動方向に対してキャラクターが自然に回転します。


### 三項演算子の詳細な解説と整理

三項演算子（条件演算子）は、簡潔に条件分岐を行うための構文です。以下の形式で使用します：

```csharp
条件 ? 式1 : 式2;
```

- **条件**: 論理式。`true`または`false`を返します。
- **式1**: 条件が`true`の場合に評価される式。
- **式2**: 条件が`false`の場合に評価される式。

### 例

以下のコードは、三項演算子を使用して、変数`actualSpeed`に値を代入する例です。

```csharp
float speed = 1f; // 仮の速度値
float runSpeed = 6.0f; // 走行速度
float walkSpeed = 2.0f; // 歩行速度

float actualSpeed = Mathf.Abs(speed) == 1f ? runSpeed : walkSpeed;
```

この例では、`speed`の絶対値が1の場合に`runSpeed`を代入し、それ以外の場合に`walkSpeed`を代入します。

### 詳細な使用例と解説

```csharp
float actualSpeed = Mathf.Abs(speed) == 1f ? runSpeed : walkSpeed;
```

- **`Mathf.Abs(speed)`**: `speed`の絶対値を計算します。
  - 例: `speed`が-1, 1, -0.5, 0.5の場合、それぞれ1, 1, 0.5, 0.5を返します。
- **`Mathf.Abs(speed) == 1f`**: 絶対値が1であるかどうかをチェックします。
  - 絶対値が1の場合、条件は`true`になります。それ以外の場合、条件は`false`になります。
- **三項演算子の条件部分**: `Mathf.Abs(speed) == 1f`
  - `true`の場合: `runSpeed`を`actualSpeed`に代入します。
  - `false`の場合: `walkSpeed`を`actualSpeed`に代入します。

### 理由

- **三項演算子を使用する理由**: コードを簡潔にし、条件分岐を一行で表現するためです。`if-else`文を使用するよりも読みやすくなります。

### まとめ

```csharp
float actualSpeed = Mathf.Abs(speed) == 1f ? runSpeed : walkSpeed;
```

1. **条件の評価**: `Mathf.Abs(speed) == 1f`
   - `speed`の絶対値が1の場合、条件は`true`になります。
   - それ以外の場合、条件は`false`になります。

2. **条件が`true`の場合**: `runSpeed`が`actualSpeed`に代入されます。
3. **条件が`false`の場合**: `walkSpeed`が`actualSpeed`に代入されます。

このコードにより、`speed`が1または-1の場合に走行速度を使用し、それ以外の場合に歩行速度を使用することができます。三項演算子を使うことで、条件分岐を簡潔に表現し、コードの可読性を高めています。




以下に、この `CharacterController` スクリプト内で使用されている `UnityEngine` 名前空間に属するクラスを詳細に解説します。

### 使用されている `UnityEngine` クラスの詳細

#### 1. `Animator`
- **説明**: `Animator` クラスは、アニメーションを制御するためのコンポーネントです。アニメーターコントローラを介してアニメーションを再生、停止、およびパラメータの設定を行います。
- **使用箇所**: アニメーションのパラメータ設定とトリガー設定。
  ```csharp
  public Animator animator; // Animator コンポーネント
  ...
  animator.SetFloat("Speed", speed); // アニメーションパラメーターを設定
  animator.SetTrigger("JumpStart"); // ジャンプアニメーションを開始
  animator.SetTrigger("JumpEnd"); // ジャンプアニメーションを終了
  ```

#### 2. `Rigidbody`
- **説明**: `Rigidbody` クラスは、物理シミュレーションを制御するためのコンポーネントです。オブジェクトに質量、重力、物理的な力を適用することができます。
- **使用箇所**: ジャンプの力を加えるため。
  ```csharp
  private Rigidbody rb; // Rigidbody コンポーネント
  ...
  rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // ジャンプの力を加える
  ```

#### 3. `Transform`
- **説明**: `Transform` クラスは、ゲームオブジェクトの位置、回転、スケールを表すクラスです。すべてのゲームオブジェクトは `Transform` コンポーネントを持っています。
- **使用箇所**: カメラの位置と回転の取得、キャラクターの位置と回転の設定。
  ```csharp
  private Transform cameraTransform; // メインカメラのTransform
  ...
  cameraTransform = Camera.main.transform; // メインカメラのTransformを取得
  transform.Translate(movementDirection * actualSpeed * Time.deltaTime, Space.World); // キャラクターの移動
  transform.rotation = Quaternion.LookRotation(movementDirection, Vector3.up); // キャラクターの回転
  ```

#### 4. `Vector3`
- **説明**: `Vector3` クラスは、3次元ベクトル（x, y, z）を表す構造体です。位置、方向、速度などを表現するために使用されます。
- **使用箇所**: 移動方向やジャンプの力の設定に使用。
  ```csharp
  private Vector3 currentVelocity; // カメラの現在の速度
  ...
  Vector3 forward = cameraTransform.forward; // カメラの前方向
  Vector3 right = cameraTransform.right; // カメラの右方向
  rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // ジャンプの力を加える
  ```

#### 5. `Quaternion`
- **説明**: `Quaternion` クラスは、回転を表現するための構造体です。回転をオイラー角や回転軸と角度として表現することができます。
- **使用箇所**: キャラクターの回転の設定に使用。
  ```csharp
  private Quaternion forwardRotation; // 前進時の向き
  ...
  forwardRotation = Quaternion.LookRotation(movementDirection, Vector3.up); // 前進時の向きを更新
  ```

#### 6. `Mathf`
- **説明**: `Mathf` クラスは、数学的な関数や定数を提供する静的クラスです。三角関数、補間、絶対値、クランプなどの関数が含まれます。
- **使用箇所**: 移動速度の設定に使用。
  ```csharp
  float actualSpeed = Mathf.Abs(speed) == 1f ? runSpeed : walkSpeed; // 実際の移動速度
  ```

#### 7. `Input`
- **説明**: `Input` クラスは、ユーザー入力を取得するための静的クラスです。キーボード、マウス、タッチ、コントローラなどの入力を取得できます。
- **使用箇所**: キーボード入力の取得に使用。
  ```csharp
  if (Input.GetKey(KeyCode.W)) { ... } // Wキーの入力を取得
  if (Input.GetKey(KeyCode.S)) { ... } // Sキーの入力を取得
  if (Input.GetKey(KeyCode.A)) { ... } // Aキーの入力を取得
  if (Input.GetKey(KeyCode.D)) { ... } // Dキーの入力を取得
  if (Input.GetKeyDown(KeyCode.Space) && !isJumping) { ... } // スペースキーの入力を取得
  ```

#### 8. `Collision`
- **説明**: `Collision` クラスは、物理衝突イベントに関する情報を提供します。衝突したオブジェクト、衝突点、衝突の力などの情報を取得できます。
- **使用箇所**: 地面との衝突を検知してジャンプ状態をリセットするために使用。
  ```csharp
  void OnCollisionEnter(Collision collision)
  {
      if (collision.gameObject.CompareTag("Ground"))
      {
          isJumping = false;
          animator.SetTrigger("JumpEnd");
      }
  }
  ```

### クラスごとの詳細な説明と使用例

#### `Animator`
- **役割**: アニメーションの制御。
- **使用例**:
  ```csharp
  public Animator animator; // Animator コンポーネント
  ...
  animator.SetFloat("Speed", speed); // アニメーションパラメーターを設定
  animator.SetTrigger("JumpStart"); // ジャンプアニメーションを開始
  animator.SetTrigger("JumpEnd"); // ジャンプアニメーションを終了
  ```

#### `Rigidbody`
- **役割**: 物理シミュレーションの制御。
- **使用例**:
  ```csharp
  private Rigidbody rb; // Rigidbody コンポーネント
  ...
  rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // ジャンプの力を加える
  ```

#### `Transform`
- **役割**: ゲームオブジェクトの位置、回転、スケールの管理。
- **使用例**:
  ```csharp
  private Transform cameraTransform; // メインカメラのTransform
  ...
  cameraTransform = Camera.main.transform; // メインカメラのTransformを取得
  transform.Translate(movementDirection * actualSpeed * Time.deltaTime, Space.World); // キャラクターの移動
  transform.rotation = Quaternion.LookRotation(movementDirection, Vector3.up); // キャラクターの回転
  ```

#### `Vector3`
- **役割**: 3次元ベクトルデータの管理。
- **使用例**:
  ```csharp
  private Vector3 currentVelocity; // カメラの現在の速度
  ...
  Vector3 forward = cameraTransform.forward; // カメラの前方向
  Vector3 right = cameraTransform.right; // カメラの右方向
  rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // ジャンプの力を加える
  ```

#### `Quaternion`
- **役割**: 回転の管理。
- **使用例**:
  ```csharp
  private Quaternion forwardRotation; // 前進時の向き
  ...
  forwardRotation = Quaternion.LookRotation(movementDirection, Vector3.up); // 前進時の向きを更新
  ```

#### `Mathf`
- **役割**: 数学的な関数や定数の提供。
- **使用例**:
  ```csharp
  float actualSpeed = Mathf.Abs(speed) == 1f ? runSpeed : walkSpeed; // 実際の移動速度
  ```

#### `Input`
- **役割**: ユーザー入力の取得。
- **使用例**:
  ```csharp
  if (Input.GetKey(KeyCode.W)) { ... } // Wキーの入力を取得
  if (Input.GetKey(KeyCode.S)) { ... } // Sキーの入力を取得
  if (Input.GetKey(KeyCode.A)) { ... } // Aキーの入力を取得
  if (Input.GetKey(KeyCode.D)) { ... } // Dキーの入力を取得
  if (Input.GetKeyDown(KeyCode.Space) && !isJumping) { ... } // スペースキーの入力を取得
  ```

#### `Collision`
- **役割**: 物理衝突イベントの情報の提供。
- **使用例**:
  ```csharp
  void OnCollisionEnter(Collision collision)
  {
      if (collision.gameObject.CompareTag("Ground

"))
      {
          isJumping = false;
          animator.SetTrigger("JumpEnd");
      }
  }
  ```

このスクリプトでは、これらのクラスを使用してキャラクターの移動、回転、アニメーション、ジャンプを制御し、ユーザーの入力に応じて動作を変更します。
 */
public class GuidecharaCon : MonoBehaviour
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
