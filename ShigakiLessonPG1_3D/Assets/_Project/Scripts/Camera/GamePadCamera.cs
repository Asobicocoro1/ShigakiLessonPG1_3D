using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePadCamera : SpiderSwing
{
    
    [SerializeField] private Transform cameraTarget; // �J�����̒Ǐ]�^�[�Q�b�g
    [SerializeField] private float rightStickSensitivity = 100f; // �E�X�e�B�b�N���x
    [SerializeField] private float normalDistance = 5.0f; // �ʏ펞�̃J�����ƃv���C���[�̋���
    [SerializeField] private float swingDistance = 8.0f; // �X�C���O���̃J�����ƃv���C���[�̋���
    [SerializeField] private Vector3 cameraOffset = new Vector3(0, 1.5f, 0); // �J�����̃I�t�Z�b�g
    [SerializeField] private float smoothTime = 0.1f; // �J�����Ǐ]�̃X���[�Y��
    [SerializeField] private float zoomSpeed = 2f; // �J�����̃Y�[���X�s�[�h
    [SerializeField] private Vector3 lookAtOffset = new Vector3(0, 1.0f, 0); // �J���������_�̃I�t�Z�b�g

    private Vector3 currentVelocity;
    private float pitch = 0f; // ���������̉�]�iX���j
    private float yaw = 0f; // ���������̉�]�iY���j
    private float currentDistance; // ���݂̃J�����ƃv���C���[�̋���

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // �J�[�\�������b�N
        currentDistance = normalDistance; // �ʏ�̃J����������ݒ�
    }

    private void Update()
    {
        HandleCameraInput(); // �Q�[���p�b�h�̓��͏���
        HandleZoom(); // �J�����̃Y�[������
    }

    private void LateUpdate()
    {
        FollowPlayer(); // �v���C���[�̒Ǐ]����
    }

    // �J�������͏����i�E�X�e�B�b�N�j
    private void HandleCameraInput()
    {
        float lookX = GamepadInputManager.Instance.GetAxis("LookHorizontal") * rightStickSensitivity * Time.deltaTime;
        float lookY = GamepadInputManager.Instance.GetAxis("LookVertical") * rightStickSensitivity * Time.deltaTime;

        yaw += lookX;
        pitch -= lookY;

        pitch = Mathf.Clamp(pitch, -35f, 60f);
    }

    // �J�����̃Y�[�������i�X�C���O���̃J�������������j
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

    // �v���C���[��Ǐ]����J�����̓���
    private void FollowPlayer()
    {
        // �J�����̉�]���v�Z�i�v���C���[�̈ʒu����ɃJ�����̃��[�ƃs�b�`��K�p�j
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        // �v���C���[�̈ʒu�ɃI�t�Z�b�g��ǉ����āA�J�����̈ʒu��ݒ�
        Vector3 targetPosition = cameraTarget.position + rotation * (cameraOffset - Vector3.forward * currentDistance);

        // �J�������X���[�Y�ɒǏ]������
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);

        // �J�������v���C���[�̏�����𒍎�����悤�ɁA�����_�̃I�t�Z�b�g�𔽉f
        Vector3 lookAtPosition = cameraTarget.position + lookAtOffset;
        transform.LookAt(lookAtPosition);
    }
}
