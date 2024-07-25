using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // プレイヤーのTransform
    public Vector3 offset; // カメラのオフセット
    public float sensitivity = 5.0f; // カメラの回転感度
    public float smoothTime = 0.1f; // カメラのスムーズ追従時間

    private Vector3 currentVelocity;
    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    void Start()
    {
        offset = transform.position - player.position;
        Cursor.lockState = CursorLockMode.Locked; // マウスカーソルを画面中央に固定
    }

    void LateUpdate()
    {
        // マウスの入力を取得
        rotationX += Input.GetAxis("Mouse X") * sensitivity;
        rotationY -= Input.GetAxis("Mouse Y") * sensitivity;
        rotationY = Mathf.Clamp(rotationY, -35, 60); // 垂直方向の回転範囲を制限

        // プレイヤーを中心にカメラを回転
        Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);
        Vector3 targetPosition = player.position + rotation * offset;

        // カメラの位置をスムーズに追従
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);

        // カメラがプレイヤーを常に見るようにする
        transform.LookAt(player.position);
    }
}
/*
 この `CameraFollow` スクリプトは、Unity の `UnityEngine` 名前空間に属するいくつかのクラスを使用しています。以下に、使用されている `UnityEngine` クラスを詳細に解説します。

### 使用されている `UnityEngine` クラスの詳細

#### 1. `Transform`
- **説明**: `Transform` は、ゲームオブジェクトの位置、回転、スケールを表すクラスです。すべてのゲームオブジェクトは `Transform` コンポーネントを持っています。
- **使用箇所**: `player` フィールドと `transform` プロパティで使用。
  ```csharp
  public Transform player; // プレイヤーのTransform
  ...
  offset = transform.position - player.position;
  ...
  transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
  ...
  transform.LookAt(player.position);
  ```

#### 2. `Vector3`
- **説明**: `Vector3` は、3次元ベクトル（x, y, z）を表す構造体です。位置、方向、速度などを表現するために使用されます。
- **使用箇所**: `offset`, `currentVelocity`, `targetPosition` の計算と、`transform.position` の設定に使用。
  ```csharp
  public Vector3 offset; // カメラのオフセット
  private Vector3 currentVelocity;
  ...
  Vector3 targetPosition = player.position + rotation * offset;
  ```

#### 3. `Quaternion`
- **説明**: `Quaternion` は、回転を表現するための構造体です。回転をオイラー角や回転軸と角度として表現することができます。
- **使用箇所**: カメラの回転の計算に使用。
  ```csharp
  Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);
  ```

#### 4. `Cursor`
- **説明**: `Cursor` クラスは、マウスカーソルの表示状態や位置を制御するための静的クラスです。
- **使用箇所**: マウスカーソルを画面中央に固定するために使用。
  ```csharp
  Cursor.lockState = CursorLockMode.Locked;
  ```

#### 5. `CursorLockMode`
- **説明**: `CursorLockMode` は、カーソルのロック状態を定義する列挙型です。`Locked`、`Confined`、`None` の3つのモードがあります。
- **使用箇所**: マウスカーソルを画面中央に固定するための設定に使用。
  ```csharp
  Cursor.lockState = CursorLockMode.Locked;
  ```

#### 6. `Mathf`
- **説明**: `Mathf` クラスは、数学的な関数や定数を提供する静的クラスです。三角関数、補間、絶対値、クランプなどの関数が含まれます。
- **使用箇所**: カメラの垂直回転範囲を制限するために使用。
  ```csharp
  rotationY = Mathf.Clamp(rotationY, -35, 60);
  ```

### クラスごとの詳細な説明と使用例

#### `Transform`
- **役割**: ゲームオブジェクトの位置、回転、スケールを管理。
- **使用例**:
  ```csharp
  public Transform player; // プレイヤーのTransform
  ...
  offset = transform.position - player.position;
  transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
  transform.LookAt(player.position);
  ```

#### `Vector3`
- **役割**: 3次元のベクトルデータを扱う。
- **使用例**:
  ```csharp
  public Vector3 offset; // カメラのオフセット
  private Vector3 currentVelocity; // カメラの現在の速度
  ...
  Vector3 targetPosition = player.position + rotation * offset; // 目標位置を計算
  ```

#### `Quaternion`
- **役割**: 回転を表現するための構造体。
- **使用例**:
  ```csharp
  Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0); // 回転を計算
  ```

#### `Cursor`
- **役割**: マウスカーソルの制御。
- **使用例**:
  ```csharp
  Cursor.lockState = CursorLockMode.Locked; // マウスカーソルをロック
  ```

#### `CursorLockMode`
- **役割**: カーソルのロックモードを定義。
- **使用例**:
  ```csharp
  Cursor.lockState = CursorLockMode.Locked; // カーソルロックモードを設定
  ```

#### `Mathf`
- **役割**: 数学的な関数や定数を提供。
- **使用例**:
  ```csharp
  rotationY = Mathf.Clamp(rotationY, -35, 60); // 回転範囲を制限
  ```

このスクリプトでは、これらのクラスを使用して、カメラがプレイヤーを追従し、マウス入力に基づいて回転する動作を実現しています。
 */
