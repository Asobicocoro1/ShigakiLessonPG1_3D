using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // �v���C���[��Transform
    public Vector3 offset; // �J�����̃I�t�Z�b�g
    public float sensitivity = 5.0f; // �J�����̉�]���x
    public float smoothTime = 0.1f; // �J�����̃X���[�Y�Ǐ]����

    private Vector3 currentVelocity;
    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    void Start()
    {
        offset = transform.position - player.position;
        Cursor.lockState = CursorLockMode.Locked; // �}�E�X�J�[�\������ʒ����ɌŒ�
    }

    void LateUpdate()
    {
        // �}�E�X�̓��͂��擾
        rotationX += Input.GetAxis("Mouse X") * sensitivity;
        rotationY -= Input.GetAxis("Mouse Y") * sensitivity;
        rotationY = Mathf.Clamp(rotationY, -35, 60); // ���������̉�]�͈͂𐧌�

        // �v���C���[�𒆐S�ɃJ��������]
        Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);
        Vector3 targetPosition = player.position + rotation * offset;

        // �J�����̈ʒu���X���[�Y�ɒǏ]
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);

        // �J�������v���C���[����Ɍ���悤�ɂ���
        transform.LookAt(player.position);
    }
}
