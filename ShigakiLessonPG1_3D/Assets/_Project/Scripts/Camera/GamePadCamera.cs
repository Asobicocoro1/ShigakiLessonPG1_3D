using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePadCamera : SpiderSwing
{
    
    [SerializeField] private Transform cameraTarget; // カメラの追従ターゲット
    [SerializeField] private float rightStickSensitivity = 100f; // 右スティック感度
    [SerializeField] private float normalDistance = 5.0f; // 通常時のカメラとプレイヤーの距離
    [SerializeField] private float swingDistance = 8.0f; // スイング中のカメラとプレイヤーの距離
    [SerializeField] private Vector3 cameraOffset = new Vector3(0, 1.5f, 0); // カメラのオフセット
    [SerializeField] private float smoothTime = 0.1f; // カメラ追従のスムーズさ
    [SerializeField] private float zoomSpeed = 2f; // カメラのズームスピード
    [SerializeField] private Vector3 lookAtOffset = new Vector3(0, 1.0f, 0); // カメラ注視点のオフセット

    private Vector3 currentVelocity;
    private float pitch = 0f; // 垂直方向の回転（X軸）
    private float yaw = 0f; // 水平方向の回転（Y軸）
    private float currentDistance; // 現在のカメラとプレイヤーの距離

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // カーソルをロック
        currentDistance = normalDistance; // 通常のカメラ距離を設定
    }

    private void Update()
    {
        HandleCameraInput(); // ゲームパッドの入力処理
        HandleZoom(); // カメラのズーム処理
    }

    private void LateUpdate()
    {
        FollowPlayer(); // プレイヤーの追従処理
    }

    // カメラ入力処理（右スティック）
    private void HandleCameraInput()
    {
        float lookX = GamepadInputManager.Instance.GetAxis("LookHorizontal") * rightStickSensitivity * Time.deltaTime;
        float lookY = GamepadInputManager.Instance.GetAxis("LookVertical") * rightStickSensitivity * Time.deltaTime;

        yaw += lookX;
        pitch -= lookY;

        pitch = Mathf.Clamp(pitch, -35f, 60f);
    }

    // カメラのズーム処理（スイング時のカメラ距離調整）
    private void HandleZoom()
    {
        if (rightSpringJoint != null || leftSpringJoint != null)
        {
            currentDistance = Mathf.Lerp(currentDistance, swingDistance, Time.deltaTime * zoomSpeed);
        }
        else
        {
            currentDistance = Mathf.Lerp(currentDistance, normalDistance, Time.deltaTime * zoomSpeed);
        }
    }

    // プレイヤーを追従するカメラの動き
    private void FollowPlayer()
    {
        // カメラの回転を計算（プレイヤーの位置を基準にカメラのヨーとピッチを適用）
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        // プレイヤーの位置にオフセットを追加して、カメラの位置を設定
        Vector3 targetPosition = cameraTarget.position + rotation * (cameraOffset - Vector3.forward * currentDistance);

        // カメラをスムーズに追従させる
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);

        // カメラがプレイヤーの少し上を注視するように、注視点のオフセットを反映
        Vector3 lookAtPosition = cameraTarget.position + lookAtOffset;
        transform.LookAt(lookAtPosition);
    }
}
