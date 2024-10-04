using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player; // プレイヤーのTransform
    [SerializeField] private float mouseSensitivity = 100f; // マウス感度
    [SerializeField] private float distance = 5.0f; // プレイヤーとの距離
    [SerializeField] private Vector3 cameraOffset = new Vector3(0, 1.5f, 0); // カメラの位置オフセット
    [SerializeField] private Vector3 lookAtOffset = new Vector3(0, 1.0f, 0); // 注視点のオフセット（プレイヤーの少し上を見る）
    [SerializeField] private float smoothTime = 0.1f; // カメラのスムーズ追従時間

    private Vector3 currentVelocity;
    private float pitch = 0f; // 垂直方向の回転
    private float yaw = 0f; // 水平方向の回転

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // カーソルをロック
    }

    void Update()
    {
        HandleCameraInput(); // マウスやコントローラーからの入力を処理
        HandleZoom(); // ズームイン・ズームアウトの処理
    }

    void LateUpdate()
    {
        FollowPlayer(); // プレイヤーの追従処理
    }

    private void HandleCameraInput()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -35f, 60f); // ピッチの制限
    }

    private void HandleZoom()
    {
        // マウスホイールまたはコントローラの入力でズーム調整
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        distance -= scrollInput;
        distance = Mathf.Clamp(distance, 2.0f, 10.0f); // 距離を制限
    }

    private void FollowPlayer()
    {
        // カメラの回転と位置を計算（プレイヤーの位置に基づく）
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        // カメラ位置をプレイヤーの後ろ側に配置し、カメラのオフセットを追加
        Vector3 targetPosition = player.position + rotation * (cameraOffset - Vector3.forward * distance);

        // カメラの位置をスムーズに追従
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);

        // カメラの注視点をプレイヤーの少し上に設定（lookAtOffsetで調整）
        Vector3 lookAtPosition = player.position + lookAtOffset;
        transform.LookAt(lookAtPosition); // カメラがプレイヤーを常に注視
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
  `
``
オイラー角（Euler angles）は、三次元ユークリッド空間中の2つの直交座標系の関係を表現する方法の一つです。これは、剛体の回転姿勢を独立な３つの角度で表す方法であり、力学やコンピュータグラフィックスでよく使われています¹。オイラー角は、以下の３つの角度で構成されます：

1. **機首方位角（Heading Angle）**: 飛行機の機首がどの方向を向いているかを表す角度です。
2. **ピッチ角（Attitude Angle）**: 飛行機の機体が上下方向をどのように傾いているかを表す角度です。
3. **バンク角（Bank Angle）**: 飛行機の機体が左右方向に傾いているかを表す角度です。

これらの角度を組み合わせて、剛体の回転姿勢を記述します。オイラー角は便利で分かりやすいため、航空工学やコンピュータグラフィックスで広く用いられています。特異姿勢やジンバルロックといった注意点もありますが、基本的な理解はこのようなものです。¹²


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
