using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePadCamera : MonoBehaviour
{
    [SerializeField] private Transform player; // �v���C���[��Transform
    [SerializeField] private float rightStickSensitivity = 500f; // �E�X�e�B�b�N���x
    [SerializeField] private float normalDistance = 5.0f; // �ʏ펞�̋���
    [SerializeField] private float swingDistance = 8.0f; // �X�C���O���̋���
    [SerializeField] private Vector3 cameraOffset = new Vector3(0, 1.5f, 0); // �J�����̈ʒu�I�t�Z�b�g
    [SerializeField] private Vector3 lookAtOffset = new Vector3(0, 1.0f, 0); // �����_�̃I�t�Z�b�g
    [SerializeField] private float smoothTime = 0.1f; // �J�����̃X���[�Y�Ǐ]����

    private Vector3 currentVelocity;
    private float pitch = 0f; // ���������̉�]�iX���j
    private float yaw = 0f; // ���������̉�]�iY���j
    private float currentDistance; // ���݂̃J��������

    private GrappleController grappleController; // �O���b�v����Ԃ��Ď�

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // �J�[�\�������b�N
        currentDistance = normalDistance; // �ʏ�̋����������l�ɐݒ�
        grappleController = player.GetComponent<GrappleController>(); // �v���C���[�̃O���b�v���R���g���[���[���擾
    }

    void Update()
    {
        HandleCameraInput(); // �E�X�e�B�b�N����̓��͂�����
        HandleZoom(); // �Y�[���C���E�Y�[���A�E�g�̏���
    }

    void LateUpdate()
    {
        FollowPlayer(); // �v���C���[�̒Ǐ]����
    }

    // �E�X�e�B�b�N�̓��͂Ɋ�Â��ăJ�����̉�]������
    private void HandleCameraInput()
    {
        // �E�X�e�B�b�N�̍��E���͂�Y���i���[�j�𐧌�
        float lookX = GamepadInputManager.Instance.GetAxis("LookHorizontal") * rightStickSensitivity * Time.deltaTime;
        // �E�X�e�B�b�N�̑O����͂�X���i�s�b�`�j�𐧌�
        float lookY = GamepadInputManager.Instance.GetAxis("LookVertical") * rightStickSensitivity * Time.deltaTime;

        // �f�o�b�O�o��
        Debug.Log($"LookHorizontal: {lookX}, LookVertical: {lookY}");

        yaw += lookX; // ���E�̃X�e�B�b�N�����Y���i���[�j��ύX
        pitch -= lookY; // �O��̃X�e�B�b�N�����X���i�s�b�`�j��ύX
        pitch = Mathf.Clamp(pitch, -35f, 60f); // �s�b�`�͈̔͂𐧌�
    }

    private void HandleZoom()
    {
        // �O���b�v�����͋������������A�ʏ펞�ɂ͌��ɖ߂�
        if (grappleController != null && grappleController.IsGrappling)
        {
            currentDistance = Mathf.Lerp(currentDistance, swingDistance, Time.deltaTime * smoothTime); // �X�C���O���ɃJ����������
        }
        else
        {
            currentDistance = Mathf.Lerp(currentDistance, normalDistance, Time.deltaTime * smoothTime); // �ʏ틗���ɖ߂�
        }
    }

    private void FollowPlayer()
    {
        // �J�����̉�]�ƈʒu���v�Z�i�v���C���[�̈ʒu�Ɋ�Â��j
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        // �J�����ʒu���v���C���[�̌�둤�ɔz�u���A�J�����̃I�t�Z�b�g��ǉ�
        Vector3 targetPosition = player.position + rotation * (cameraOffset - Vector3.forward * currentDistance);

        // �J�����̈ʒu���X���[�Y�ɒǏ]
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);

        // �J�����̒����_���v���C���[�̏�����ɐݒ�ilookAtOffset�Œ����j
        Vector3 lookAtPosition = player.position + lookAtOffset;
        transform.LookAt(lookAtPosition); // �J�������v���C���[����ɒ���
    }
}
