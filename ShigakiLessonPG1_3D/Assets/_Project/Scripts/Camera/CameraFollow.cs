using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player; // プレイヤーのTransform
    [SerializeField] private float mouseSensitivity = 100f; // マウス感度
    [SerializeField] private float normalDistance = 5.0f; // 通常時の距離
    [SerializeField] private float swingDistance = 8.0f; // スイング中の距離
    [SerializeField] private Vector3 cameraOffset = new Vector3(0, 1.5f, 0); // カメラの位置オフセット
    [SerializeField] private Vector3 lookAtOffset = new Vector3(0, 1.0f, 0); // 注視点のオフセット
    [SerializeField] private float smoothTime = 0.1f; // カメラのスムーズ追従時間

    private Vector3 currentVelocity;
    private float pitch = 0f; // 垂直方向の回転
    private float yaw = 0f; // 水平方向の回転
    private float currentDistance; // 現在のカメラ距離

   

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // カーソルをロック
        currentDistance = normalDistance; // 通常の距離を初期値に設定
       
    }

    void Update()
    {
        HandleCameraInput(); // マウスやコントローラーからの入力を処理
       
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

   

    private void FollowPlayer()
    {
        // カメラの回転と位置を計算（プレイヤーの位置に基づく）
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        // カメラ位置をプレイヤーの後ろ側に配置し、カメラのオフセットを追加
        Vector3 targetPosition = player.position + rotation * (cameraOffset - Vector3.forward * currentDistance);

        // カメラの位置をスムーズに追従
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);

        // カメラの注視点をプレイヤーの少し上に設定（lookAtOffsetで調整）
        Vector3 lookAtPosition = player.position + lookAtOffset;
        transform.LookAt(lookAtPosition); // カメラがプレイヤーを常に注視
    }
}