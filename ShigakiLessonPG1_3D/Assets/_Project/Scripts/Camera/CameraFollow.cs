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
