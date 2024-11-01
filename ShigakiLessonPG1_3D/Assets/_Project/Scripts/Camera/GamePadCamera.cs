using UnityEngine;

public class GamePadCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraTarget; // �J�������Ǐ]����Ώ�
    [SerializeField] private float rightStickSensitivity = 100f; // �E�X�e�B�b�N�̊��x
    [SerializeField] private float normalDistance = 5.0f; // �ʏ펞�̃J��������
    [SerializeField] private float swingDistance = 8.0f; // �X�C���O���̃J��������
    [SerializeField] private Vector3 cameraOffset = new Vector3(0, 1.5f, 0); // �J�����̃I�t�Z�b�g
    [SerializeField] private float smoothTime = 0.1f; // �J�����Ǐ]�̃X���[�Y��
    [SerializeField] private float zoomSpeed = 2f; // �J�����̃Y�[���X�s�[�h
    [SerializeField] private Vector3 lookAtOffset = new Vector3(0, 1.0f, 0); // �����_�̃I�t�Z�b�g

    private Vector3 currentVelocity; // �J�����̈ړ����x
    private float pitch = 0f; // ���������̉�]
    private float yaw = 0f; // ���������̉�]
    private float currentDistance; // ���݂̃J��������
    private SpiderSwing swingController; // SpiderSwing�R���|�[�l���g

    // �X�C���O��Ԃ��擾����v���p�e�B
    public bool IsSwinging { get; private set; } = false;

    void Start()
    {
        // �J�[�\�������b�N���Ĕ�\���ɐݒ�
        Cursor.lockState = CursorLockMode.Locked;
        currentDistance = normalDistance; // �J�����̏���������ʏ틗���ɐݒ�
        swingController = FindObjectOfType<SpiderSwing>(); // �V�[������SpiderSwing�R���|�[�l���g���擾
    }

    void Update()
    {
        HandleCameraInput(); // �J�����̉�]����
        HandleZoom(); // �X�C���O���̃Y�[������
    }

    private void HandleCameraInput()
    {
        // �E�X�e�B�b�N�̓��͂ŃJ�����̉�]�𑀍�
        float lookX = GamepadInputManager.Instance.GetAxis("LookHorizontal") * rightStickSensitivity * Time.deltaTime;
        float lookY = GamepadInputManager.Instance.GetAxis("LookVertical") * rightStickSensitivity * Time.deltaTime;

        yaw += lookX; // ���������̉�]���X�V
        pitch -= lookY; // ���������̉�]���X�V
        pitch = Mathf.Clamp(pitch, -35f, 60f); // �J�����̐�����]�p�x�𐧌�
    }

    private void HandleZoom()
    {
        // �X�C���O���̓X�C���O�����A�����łȂ��ꍇ�͒ʏ틗���ɃJ������ݒ�
        if (swingController != null && swingController.IsSwinging)
        {
            currentDistance = Mathf.Lerp(currentDistance, swingDistance, Time.deltaTime * zoomSpeed);
        }
        else
        {
            currentDistance = Mathf.Lerp(currentDistance, normalDistance, Time.deltaTime * zoomSpeed);
        }
    }

    private void LateUpdate()
    {
        FollowPlayer(); // �v���C���[��Ǐ]����J��������
    }

    private void FollowPlayer()
    {
        // �J�����̉�]���v�Z
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        // �Ǐ]�Ώۈʒu�ɃI�t�Z�b�g��ǉ����ăJ�����ʒu���v�Z
        Vector3 targetPosition = cameraTarget.position + rotation * (cameraOffset - Vector3.forward * currentDistance);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);

        // �J��������������ʒu��ݒ�
        Vector3 lookAtPosition = cameraTarget.position + lookAtOffset;
        transform.LookAt(lookAtPosition); // �J�����𒍎��_�Ɍ�����
    }
}
