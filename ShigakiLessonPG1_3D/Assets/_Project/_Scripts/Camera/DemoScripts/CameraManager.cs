using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public FollowPlayerCamera followPlayerCamera; // プレイヤー追従カメラ
    public FixedCamera fixedCamera; // 固定カメラ

    private enum CameraMode { FollowPlayer, Fixed }
    private CameraMode currentCameraMode;

    private void Start()
    {
        SetCameraMode(CameraMode.FollowPlayer); // デフォルトでプレイヤー追従カメラに設定
    }

    private void Update()
    {
        // キー入力でカメラモードを切り替え
        if (Input.GetKeyDown(KeyCode.C))
        {
            SetCameraMode(currentCameraMode == CameraMode.FollowPlayer ? CameraMode.Fixed : CameraMode.FollowPlayer);
        }
    }

    private void SetCameraMode(CameraMode mode)
    {
        currentCameraMode = mode;

        // カメラモードに応じてカメラを有効/無効にする
        followPlayerCamera.enabled = (mode == CameraMode.FollowPlayer);
        fixedCamera.enabled = (mode == CameraMode.Fixed);
    }
}
