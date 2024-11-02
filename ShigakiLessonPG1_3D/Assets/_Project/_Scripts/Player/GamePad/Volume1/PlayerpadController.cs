using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerpadController : MonoBehaviour
{
    [SerializeField] private Animator animator; // Animator �R���|�[�l���g
    [SerializeField] private float walkSpeed = 2.0f; // ���s���x
    [SerializeField] private float runSpeed = 6.0f; // ���s���x
    //[SerializeField] private float backwardSpeed = 1.5f; // ��i���x
    [SerializeField] private float slideDuration = 1.0f; // �X���C�f�B���O�̎�������

    private bool isSliding = false; // �X���C�f�B���O�����ǂ����̃t���O
    private Rigidbody rb; // Rigidbody �R���|�[�l���g
    private Transform cameraTransform; // �J������Transform
    private float slideStartTime; // �X���C�f�B���O���J�n���ꂽ����
    private float originalYPosition; // �X���C�f�B���O�J�n����Y���W���L�^

    private float walkTime = 0f; // �����n�߂����Ԃ��L�^
    private bool isRunning = false; // �_�b�V�������ǂ����̃t���O

    [SerializeField] private BoxCollider normalCollider; // �ʏ펞��BoxCollider
    [SerializeField] private BoxCollider slideCollider;  // �X���C�f�B���O����BoxCollider

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
            // �ړ����Ă��Ȃ��Ƃ��̓A�j���[�V�������~
            animator.SetFloat("Speed", 0f);
            walkTime = 0f; // �������Ԃ����Z�b�g
            isRunning = false; // �_�b�V�������Z�b�g
        }
    }

    private void HandleSlide()
    {
        bool isSlidingInput = GamepadInputManager.Instance.GetButtonDown("Slide"); // �X���C�f�B���O���J�n������͂��擾

        if (isSlidingInput && !isSliding)
        {
            StartSlide();
        }

        if (isSliding && Time.time - slideStartTime > slideDuration)
        {
            EndSlide();
        }
    }

    private void StartSlide()
    {
        isSliding = true;
        slideStartTime = Time.time;
        originalYPosition = transform.position.y; // �X���C�f�B���O�J�n����Y���W���L�^

        animator.SetTrigger("Slide");
        animator.applyRootMotion = false; // Root Motion�𖳌���

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

    // �J�����̕�������Ɉړ��������v�Z���郁�\�b�h
    private Vector3 CalculateMovementDirection(float speed)
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0f; // �㉺�����̉�]�𖳎�
        right.y = 0f;

        //forward.Normalize();
        //right.Normalize();

        Vector3 direction = Vector3.zero;

        float moveX = GamepadInputManager.Instance.GetAxis("MoveHorizontal");
        float moveY = GamepadInputManager.Instance.GetAxis("MoveVertical");

        if (moveY > 0) direction += forward; // �O�i
        if (moveY < 0) direction -= forward; // ���

        if (moveX < 0) direction -= right;   // ���ړ�
        if (moveX > 0) direction += right;   // �E�ړ�

        return direction;
    }

    // �L�����N�^�[���ړ������郁�\�b�h
    private void MoveCharacter(Vector3 direction, float speed)
    {
        // �������Ԃ��X�V
        if (!isRunning && direction != Vector3.zero)
        {
            walkTime += Time.deltaTime; // ���s���Ԃ����Z
        }

        // 2�b�ԕ��s����������_�b�V���ɐ؂�ւ�
        if (walkTime >= 2f && !isRunning)
        {
            isRunning = true; // �_�b�V�����J�n
            walkTime = 0f; // ���s���Ԃ����Z�b�g
        }

        // �_�b�V�����ɂ̓����X�s�[�h�A�ʏ펞�ɂ̓E�H�[�N�X�s�[�h
        float actualSpeed = isRunning ? runSpeed : walkSpeed;

        // �L�����N�^�[���ړ�������
        transform.Translate(direction * actualSpeed * Time.deltaTime, Space.World);

        // �J������Y�������ɃL�����N�^�[�̉�]�����킹��
        Vector3 lookDirection = new Vector3(direction.x, 0, direction.z);

        if (lookDirection != Vector3.zero)
        {
            // X���̉�]�����O���ăL�����N�^�[����]������
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }

        // ���x�Ɋ֌W�Ȃ��O�i�̃A�j���[�V�������Đ�
        animator.SetFloat("Speed", Mathf.Abs(actualSpeed));

        // �O�i�݂̂�\�����邽�߁AZ�X�P�[������ɐ��ɂ���
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 1f);
    }

    // ���s�A���s�ɉ����Ĉړ����x��Ԃ����\�b�h
    private float GetMovementSpeed()
    {
        float verticalAxis = GamepadInputManager.Instance.GetAxis("MoveVertical");

        if (verticalAxis != 0)
        {
            return 0.5f; // �ʏ�̕��s���x��Ԃ�
        }

        return 0f; // �����Ă��Ȃ��ꍇ
    }
}