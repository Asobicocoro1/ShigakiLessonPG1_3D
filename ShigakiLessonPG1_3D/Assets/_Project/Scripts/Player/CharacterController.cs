using System;
using System.Collections;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private Animator animator; // Animator �R���|�[�l���g
    [SerializeField] private float walkSpeed = 2.0f; // ���s���x
    [SerializeField] private float runSpeed = 6.0f; // ���s���x
    [SerializeField] private float backwardSpeed = 1.5f; // ��i���x
    [SerializeField] private float slideDuration = 1.0f; // �X���C�f�B���O�̎�������

    private bool isSliding = false; // �X���C�f�B���O�����ǂ����̃t���O
    private Rigidbody rb; // Rigidbody �R���|�[�l���g
    private Transform cameraTransform; // �J������Transform
    private float slideStartTime; // �X���C�f�B���O���J�n���ꂽ����
    private float originalYPosition; // �X���C�f�B���O�J�n����Y���W���L�^

    [SerializeField] private BoxCollider normalCollider; // �ʏ펞��BoxCollider
    [SerializeField] private BoxCollider slideCollider;  // �X���C�f�B���O����BoxCollider

    public event Action OnSlide;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody �R���|�[�l���g���擾
        cameraTransform = Camera.main.transform; // ���C���J������Transform���擾

        // ������Ԃł̓X���C�f�B���O�p��BoxCollider�͖����ɂ��Ă���
        slideCollider.enabled = false;
    }

    void Update()
    {
        if (!isSliding)
        {
            HandleMovement(); // �ʏ�̈ړ�����
        }

        HandleSlide(); // �X���C�f�B���O����
    }

    private void HandleMovement()
    {
        float speed = GetMovementSpeed(); // ���x���擾
        Vector3 movementDirection = CalculateMovementDirection(speed); // �ړ��������擾

        if (movementDirection != Vector3.zero)
        {
            MoveCharacter(movementDirection, speed);
        }
        else
        {
            animator.SetFloat("Speed", 0f); // �ړ����Ă��Ȃ��Ƃ��̓A�j���[�V�������~
        }
    }

    private void HandleSlide()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isSliding)
        {
            StartSlide();
        }

        if (isSliding && Time.time - slideStartTime > slideDuration)
        {
            EndSlide();
        }

        if (isSliding)
        {
            LockYPosition(); // �X���C�f�B���O����Y���W���Œ�
        }
    }

    private void StartSlide()
    {
        isSliding = true;
        slideStartTime = Time.time;
        originalYPosition = transform.position.y; // �X���C�f�B���O�J�n����Y���W���L�^

        animator.SetTrigger("Slide");
        animator.applyRootMotion = false; // Root Motion�𖳌���
        OnSlide?.Invoke();

        // �ʏ��BoxCollider�𖳌��ɂ��A�X���C�f�B���O�p��BoxCollider��L���ɂ���
        normalCollider.enabled = false;
        slideCollider.enabled = true;
    }

    private void EndSlide()
    {
        isSliding = false;

        // �X���C�f�B���O�p��BoxCollider�𖳌��ɂ��A�ʏ��BoxCollider��L���ɂ���
        normalCollider.enabled = true;
        slideCollider.enabled = false;

        animator.applyRootMotion = true; // Root Motion���ēx�L����
    }

    // Y���̈ʒu���Œ肷�郁�\�b�h
    private void LockYPosition()
    {
        Vector3 position = transform.position;
        position.y = originalYPosition; // �J�n����Y���W���ێ�
        transform.position = position;
    }

    // ���s�A���s�A��ނɉ����Ĉړ����x��Ԃ����\�b�h
    private float GetMovementSpeed()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            return 1f; // �V�t�g�L�[�������Ȃ���O�i�ő��s
        }
        if (Input.GetKey(KeyCode.S))
        {
            return -0.5f; // ��ގ��̃X�s�[�h�͌�i���x
        }
        return 0.5f; // �ʏ�̕��s���x
    }

    // �J�����̕�������Ɉړ��������v�Z���郁�\�b�h
    private Vector3 CalculateMovementDirection(float speed)
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0f; // �㉺�����̉�]�𖳎�
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 direction = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) direction += forward; // �O�i
        if (Input.GetKey(KeyCode.S)) direction -= forward; // ���

        if (Input.GetKey(KeyCode.A)) direction -= right;   // ���ړ�
        if (Input.GetKey(KeyCode.D)) direction += right;   // �E�ړ�

        return direction;
    }

    // �L�����N�^�[���ړ������郁�\�b�h
    private void MoveCharacter(Vector3 direction, float speed)
    {
        float actualSpeed = speed < 0 ? backwardSpeed : (Mathf.Abs(speed) == 1f ? runSpeed : walkSpeed);
        transform.Translate(direction.normalized * actualSpeed * Time.deltaTime, Space.World);

        if (speed < 0)
        {
            transform.rotation = Quaternion.LookRotation(cameraTransform.forward, Vector3.up); // ��ގ�
        }
        else if (speed > 0 || speed == 0.5f)
        {
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up); // �O�i��
        }

        animator.SetFloat("Speed", speed < 0 ? -1f : Mathf.Abs(speed));
    }
}
