using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePadCamera : MonoBehaviour
{
    [SerializeField] private Transform player; // プレイヤーのTransform
    [SerializeField] private float rightStickSensitivity = 500f; // 右スティック感度
    [SerializeField] private float normalDistance = 5.0f; // 通常時の距離
    [SerializeField] private float swingDistance = 8.0f; // スイング中の距離
    [SerializeField] private Vector3 cameraOffset = new Vector3(0, 1.5f, 0); // カメラの位置オフセット
    [SerializeField] private Vector3 lookAtOffset = new Vector3(0, 1.0f, 0); // 注視点のオフセット
    [SerializeField] private float smoothTime = 0.1f; // カメラのスムーズ追従時間

    private Vector3 currentVelocity;
    private float pitch = 0f; // 垂直方向の回転（X軸）
    private float yaw = 0f; // 水平方向の回転（Y軸）
    private float currentDistance; // 現在のカメラ距離

    private GrappleController grappleController; // グラップル状態を監視

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // カーソルをロック
        currentDistance = normalDistance; // 通常の距離を初期値に設定
        grappleController = player.GetComponent<GrappleController>(); // プレイヤーのグラップルコントローラーを取得
    }

    void Update()
    {
        HandleCameraInput(); // 右スティックからの入力を処理
        HandleZoom(); // ズームイン・ズームアウトの処理
    }

    void LateUpdate()
    {
        FollowPlayer(); // プレイヤーの追従処理
    }

    // 右スティックの入力に基づいてカメラの回転を処理
    private void HandleCameraInput()
    {
        // 右スティックの左右入力でY軸（ヨー）を制御
        float lookX = GamepadInputManager.Instance.GetAxis("LookHorizontal") * rightStickSensitivity * Time.deltaTime;
        // 右スティックの前後入力でX軸（ピッチ）を制御
        float lookY = GamepadInputManager.Instance.GetAxis("LookVertical") * rightStickSensitivity * Time.deltaTime;

        // デバッグ出力
        Debug.Log($"LookHorizontal: {lookX}, LookVertical: {lookY}");

        yaw += lookX; // 左右のスティック操作でY軸（ヨー）を変更
        pitch -= lookY; // 前後のスティック操作でX軸（ピッチ）を変更
        pitch = Mathf.Clamp(pitch, -35f, 60f); // ピッチの範囲を制限
    }

    private void HandleZoom()
    {
        // グラップル中は距離を遠ざけ、通常時には元に戻す
        if (grappleController != null && grappleController.IsGrappling)
        {
            currentDistance = Mathf.Lerp(currentDistance, swingDistance, Time.deltaTime * smoothTime); // スイング中にカメラを引く
        }
        else
        {
            currentDistance = Mathf.Lerp(currentDistance, normalDistance, Time.deltaTime * smoothTime); // 通常距離に戻す
        }
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
