using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public FollowPlayerCamera followPlayerCamera; // プレイヤー追従カメラ
    public FixedCamera fixedCamera; // 固定カメラ

    private enum CameraMode { FollowPlayer, Fixed }
    private CameraMode currentCameraMode;

    void Start()
    {
        // デフォルトのカメラモードを設定
        SetCameraMode(CameraMode.FollowPlayer);
    }

    void Update()
    {
        // キー入力でカメラモードを切り替え
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (currentCameraMode == CameraMode.FollowPlayer)
            {
                SetCameraMode(CameraMode.Fixed);
            }
            else
            {
                SetCameraMode(CameraMode.FollowPlayer);
            }
        }
    }

    private void SetCameraMode(CameraMode mode)
    {
        currentCameraMode = mode;

        // 各カメラの有効/無効を切り替え
        switch (mode)
        {
            case CameraMode.FollowPlayer:
                followPlayerCamera.enabled = true;
                fixedCamera.enabled = false;
                break;
            case CameraMode.Fixed:
                followPlayerCamera.enabled = false;
                fixedCamera.enabled = true;
                break;
        }
    }
}

