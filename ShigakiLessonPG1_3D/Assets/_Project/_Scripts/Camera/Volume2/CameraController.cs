using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Target Settings")]
    [SerializeField] private Transform cameraTarget; // カメラが追従するターゲット
    [SerializeField] private float sensitivity = 100f; // カメラ操作の感度
    [SerializeField] private float normalDistance = 5f; // 通常時のカメラ距離
    [SerializeField] private float swingDistance = 8f; // スイング時のカメラ距離
    [SerializeField] private Vector3 cameraOffset = new Vector3(0, 1.5f, 0); // カメラのオフセット
    [SerializeField] private float smoothTime = 0.1f; // カメラ追従のスムーズさを制御する時間
    [SerializeField] private Vector3 lookAtOffset = new Vector3(0, 1.0f, 0); // ターゲットの注視点オフセット

    private Vector3 currentVelocity; // SmoothDamp用の現在速度
    private float pitch = 0f; // カメラの垂直回転角
    private float yaw = 0f; // カメラの水平回転角
    private float currentDistance; // 現在のカメラ距離

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // マウスカーソルをロック
        currentDistance = normalDistance; // 初期カメラ距離を設定
    }

    private void Update()
    {
        HandleCameraInput(); // カメラの入力処理
    }

    private void LateUpdate()
    {
        FollowPlayer(); // プレイヤーの追従を処理
    }

    private void HandleCameraInput()
    {
        // ゲームパッド入力からカメラの回転を取得
        float lookX = GamepadInputManager.Instance.GetAxis("LookHorizontal") * sensitivity * Time.deltaTime;
        float lookY = GamepadInputManager.Instance.GetAxis("LookVertical") * sensitivity * Time.deltaTime;

        yaw += lookX; // 水平方向の回転
        pitch -= lookY; // 垂直方向の回転
        pitch = Mathf.Clamp(pitch, -35f, 60f); // ピッチを一定範囲に制限
    }

    private void FollowPlayer()
    {
        // カメラの回転を計算
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        // ターゲット位置に基づき、カメラの目標位置を計算
        Vector3 targetPosition = cameraTarget.position + rotation * (cameraOffset - Vector3.forward * currentDistance);
        // カメラの現在位置をスムーズに目標位置へ移動
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
        // ターゲットを注視
        transform.LookAt(cameraTarget.position + lookAtOffset);
    }

    public void AdjustForSwing(bool isSwinging)
    {
        // スイング状態に応じてカメラの距離を調整
        currentDistance = isSwinging ? swingDistance : normalDistance;
    }
}
