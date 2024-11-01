using UnityEngine;

public class PlayerpadController : MonoBehaviour
{
    [SerializeField] private Animator animator; // �A�j���[�^�[�R���|�[�l���g�i�A�j���[�V�����Ǘ��p�j
    [SerializeField] private float walkSpeed = 2.0f; // ���s���x
    [SerializeField] private float runSpeed = 6.0f; // ���s���x
    [SerializeField] private float slideDuration = 1.0f; // �X���C�f�B���O�̎�������

    private bool isSliding = false; // �X���C�f�B���O�����ǂ����̃t���O
    private Rigidbody rb; // Rigidbody�R���|�[�l���g�i�����������Ǘ��j
    private Transform cameraTransform; // �J������Transform
    private float slideStartTime; // �X���C�f�B���O�J�n���̎��Ԃ��L�^
    private bool isRunning = false; // �_�b�V�������ǂ����̃t���O

    [SerializeField] private BoxCollider normalCollider; // �ʏ펞��BoxCollider
    [SerializeField] private BoxCollider slideCollider; // �X���C�f�B���O����BoxCollider

    private SpiderSwing swingController; // SpiderSwing�R���|�[�l���g�i�X�C���O����Ǘ��p�j

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody�R���|�[�l���g���擾
        cameraTransform = Camera.main.transform; // ���C���J������Transform���擾
        swingController = GetComponent<SpiderSwing>(); // SpiderSwing�R���|�[�l���g���擾
        slideCollider.enabled = false; // �X���C�f�B���O�p��Collider��������ԂŖ�����
    }

    void Update()
    {
        // �X�C���O���łȂ��A���X���C�f�B���O���łȂ��ꍇ�Ɉړ�������
        if (!isSliding && (swingController == null || !swingController.IsSwinging))
        {
            HandleMovement();
        }
        HandleSlide(); // �X���C�f�B���O�̏���
    }

    private void HandleMovement()
    {
        float speed = GetMovementSpeed(); // �ړ����x���擾
        Vector3 movementDirection = CalculateMovementDirection(speed); // �ړ��������v�Z

        if (movementDirection != Vector3.zero)
        {
            MoveCharacter(movementDirection, speed); // �L�����N�^�[���ړ�������
        }
        else
        {
            // �ړ����Ă��Ȃ��ꍇ�A�A�j���[�V�������~
            animator.SetFloat("Speed", 0f);
            isRunning = false; // �_�b�V����Ԃ����Z�b�g
        }
    }

    private void HandleSlide()
    {
        bool isSlidingInput = GamepadInputManager.Instance.GetButtonDown("Slide"); // �X���C�f�B���O�̓��͎擾

        // �X���C�f�B���O�J�n
        if (isSlidingInput && !isSliding)
        {
            StartSlide();
        }

        // �X���C�f�B���O�I��
        if (isSliding && Time.time - slideStartTime > slideDuration)
        {
            EndSlide();
        }
    }

    private void StartSlide()
    {
        isSliding = true;
        slideStartTime = Time.time; // �X���C�f�B���O�J�n���Ԃ��L�^
        animator.SetTrigger("Slide"); // �X���C�f�B���O�̃A�j���[�V�������g���K�[
        animator.applyRootMotion = false; // Root Motion�𖳌���
        normalCollider.enabled = false; // �ʏ�Collider�𖳌���
        slideCollider.enabled = true; // �X���C�f�B���O�pCollider��L����
    }

    private void EndSlide()
    {
        isSliding = false;
        normalCollider.enabled = true; // �ʏ�Collider��L����
        slideCollider.enabled = false; // �X���C�f�B���O�pCollider�𖳌���
        animator.applyRootMotion = true; // Root Motion���ēx�L����
    }

    // �J�����̕�������Ɉړ��������v�Z
    private Vector3 CalculateMovementDirection(float speed)
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0f; // Y���̉�]�𖳎��i�㉺�����̉�]�𖳌����j
        right.y = 0f;

        Vector3 direction = Vector3.zero;
        float moveX = GamepadInputManager.Instance.GetAxis("MoveHorizontal"); // �������̓���
        float moveY = GamepadInputManager.Instance.GetAxis("MoveVertical"); // �c�����̓���

        if (moveY > 0) direction += forward; // �O�i
        if (moveY < 0) direction -= forward; // ���
        if (moveX < 0) direction -= right; // ���ړ�
        if (moveX > 0) direction += right; // �E�ړ�

        return direction;
    }

    // �L�����N�^�[���ړ�������
    private void MoveCharacter(Vector3 direction, float speed)
    {
        float actualSpeed = isRunning ? runSpeed : walkSpeed;
        transform.Translate(direction * actualSpeed * Time.deltaTime, Space.World); // �L�����N�^�[�̈ړ�����

        // �L�����N�^�[�̉�]���ړ������ɍ��킹��
        Vector3 lookDirection = new Vector3(direction.x, 0, direction.z);
        if (lookDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }

        animator.SetFloat("Speed", Mathf.Abs(actualSpeed)); // �A�j���[�V�������x�̍X�V
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 1f); // Z���X�P�[�����Œ�
    }

    // �ړ����x��Ԃ�
    private float GetMovementSpeed()
    {
        float verticalAxis = GamepadInputManager.Instance.GetAxis("MoveVertical");
        return verticalAxis != 0 ? 0.5f : 0f; // �ړ�������Ƃ��̂ݑ��x��Ԃ�
    }
}
