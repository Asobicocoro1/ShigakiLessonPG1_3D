using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public FollowPlayerCamera followPlayerCamera; // �v���C���[�Ǐ]�J����
    public FixedCamera fixedCamera; // �Œ�J����

    private enum CameraMode { FollowPlayer, Fixed }
    private CameraMode currentCameraMode;

    void Start()
    {
        // �f�t�H���g�̃J�������[�h��ݒ�
        SetCameraMode(CameraMode.FollowPlayer);
    }

    void Update()
    {
        // �L�[���͂ŃJ�������[�h��؂�ւ�
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

        // �e�J�����̗L��/������؂�ւ�
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

