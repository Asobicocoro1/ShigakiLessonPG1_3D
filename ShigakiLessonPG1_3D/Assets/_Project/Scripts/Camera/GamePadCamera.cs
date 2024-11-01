using UnityEngine;

public class GamePadCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraTarget; // カメラが追従する対象
    [SerializeField] private float rightStickSensitivity = 100f; // 右スティックの感度
    [SerializeField] private float normalDistance = 5.0f; // 通常時のカメラ距離
    [SerializeField] private float swingDistance = 8.0f; // スイング時のカメラ距離
    [SerializeField] private Vector3 cameraOffset = new Vector3(0, 1.5f, 0); // カメラのオフセット
    [SerializeField] private float smoothTime = 0.1f; // カメラ追従のスムーズさ
    [SerializeField] private float zoomSpeed = 2f; // カメラのズームスピード
    [SerializeField] private Vector3 lookAtOffset = new Vector3(0, 1.0f, 0); // 注視点のオフセット

    private Vector3 currentVelocity; // カメラの移動速度
    private float pitch = 0f; // 垂直方向の回転
    private float yaw = 0f; // 水平方向の回転
    private float currentDistance; // 現在のカメラ距離
    private SpiderSwing swingController; // SpiderSwingコンポーネント

    // スイング状態を取得するプロパティ
    public bool IsSwinging { get; private set; } = false;

    void Start()
    {
        // カーソルをロックして非表示に設定
        Cursor.lockState = CursorLockMode.Locked;
        currentDistance = normalDistance; // カメラの初期距離を通常距離に設定
        swingController = FindObjectOfType<SpiderSwing>(); // シーン内のSpiderSwingコンポーネントを取得
    }

    void Update()
    {
        HandleCameraInput(); // カメラの回転処理
        HandleZoom(); // スイング時のズーム処理
    }

    private void HandleCameraInput()
    {
        // 右スティックの入力でカメラの回転を操作
        float lookX = GamepadInputManager.Instance.GetAxis("LookHorizontal") * rightStickSensitivity * Time.deltaTime;
        float lookY = GamepadInputManager.Instance.GetAxis("LookVertical") * rightStickSensitivity * Time.deltaTime;

        yaw += lookX; // 水平方向の回転を更新
        pitch -= lookY; // 垂直方向の回転を更新
        pitch = Mathf.Clamp(pitch, -35f, 60f); // カメラの垂直回転角度を制限
    }

    private void HandleZoom()
    {
        // スイング中はスイング距離、そうでない場合は通常距離にカメラを設定
        if (swingController != null && swingController.IsSwinging)
        {
            currentDistance = Mathf.Lerp(currentDistance, swingDistance, Time.deltaTime * zoomSpeed);
        }
        else
        {
            currentDistance = Mathf.Lerp(currentDistance, normalDistance, Time.deltaTime * zoomSpeed);
        }
    }

    private void LateUpdate()
    {
        FollowPlayer(); // プレイヤーを追従するカメラ処理
    }

    private void FollowPlayer()
    {
        // カメラの回転を計算
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        // 追従対象位置にオフセットを追加してカメラ位置を計算
        Vector3 targetPosition = cameraTarget.position + rotation * (cameraOffset - Vector3.forward * currentDistance);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);

        // カメラが注視する位置を設定
        Vector3 lookAtPosition = cameraTarget.position + lookAtOffset;
        transform.LookAt(lookAtPosition); // カメラを注視点に向ける
    }
}
