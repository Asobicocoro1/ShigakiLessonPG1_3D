using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Target Settings")]
    [SerializeField] private Transform cameraTarget; // �J�������Ǐ]����^�[�Q�b�g
    [SerializeField] private float sensitivity = 100f; // �J��������̊��x
    [SerializeField] private float normalDistance = 5f; // �ʏ펞�̃J��������
    [SerializeField] private float swingDistance = 8f; // �X�C���O���̃J��������
    [SerializeField] private Vector3 cameraOffset = new Vector3(0, 1.5f, 0); // �J�����̃I�t�Z�b�g
    [SerializeField] private float smoothTime = 0.1f; // �J�����Ǐ]�̃X���[�Y���𐧌䂷�鎞��
    [SerializeField] private Vector3 lookAtOffset = new Vector3(0, 1.0f, 0); // �^�[�Q�b�g�̒����_�I�t�Z�b�g

    private Vector3 currentVelocity; // SmoothDamp�p�̌��ݑ��x
    private float pitch = 0f; // �J�����̐�����]�p
    private float yaw = 0f; // �J�����̐�����]�p
    private float currentDistance; // ���݂̃J��������

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // �}�E�X�J�[�\�������b�N
        currentDistance = normalDistance; // �����J����������ݒ�
    }

    private void Update()
    {
        HandleCameraInput(); // �J�����̓��͏���
    }

    private void LateUpdate()
    {
        FollowPlayer(); // �v���C���[�̒Ǐ]������
    }

    private void HandleCameraInput()
    {
        // �Q�[���p�b�h���͂���J�����̉�]���擾
        float lookX = GamepadInputManager.Instance.GetAxis("LookHorizontal") * sensitivity * Time.deltaTime;
        float lookY = GamepadInputManager.Instance.GetAxis("LookVertical") * sensitivity * Time.deltaTime;

        yaw += lookX; // ���������̉�]
        pitch -= lookY; // ���������̉�]
        pitch = Mathf.Clamp(pitch, -35f, 60f); // �s�b�`�����͈͂ɐ���
    }

    private void FollowPlayer()
    {
        // �J�����̉�]���v�Z
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        // �^�[�Q�b�g�ʒu�Ɋ�Â��A�J�����̖ڕW�ʒu���v�Z
        Vector3 targetPosition = cameraTarget.position + rotation * (cameraOffset - Vector3.forward * currentDistance);
        // �J�����̌��݈ʒu���X���[�Y�ɖڕW�ʒu�ֈړ�
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
        // �^�[�Q�b�g�𒍎�
        transform.LookAt(cameraTarget.position + lookAtOffset);
    }

    public void AdjustForSwing(bool isSwinging)
    {
        // �X�C���O��Ԃɉ����ăJ�����̋����𒲐�
        currentDistance = isSwinging ? swingDistance : normalDistance;
    }
}
