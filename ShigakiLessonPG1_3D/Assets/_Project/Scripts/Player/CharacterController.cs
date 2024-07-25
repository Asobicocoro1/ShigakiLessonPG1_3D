using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Animator animator; // Animator �R���|�[�l���g
    public float walkSpeed = 2.0f; // ���s���x
    public float runSpeed = 6.0f; // ���s���x
    public float backwardSpeed = 2.0f; // ��i���x
    public float jumpForce = 5.0f; // �W�����v��

    private bool isJumping = false; // �W�����v�����ǂ����̃t���O
    private Rigidbody rb; // Rigidbody �R���|�[�l���g
    private Transform cameraTransform; // ���C���J������Transform
    private Quaternion forwardRotation; // �O�i���̌���

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody �R���|�[�l���g���擾
        cameraTransform = Camera.main.transform; // ���C���J������Transform���擾
        forwardRotation = transform.rotation; // �����̌�����ۑ�
    }

    void Update()
    {
        // �ړ��ƃW�����v�̏������Ăяo��
        HandleMovement(); // �ړ��̏���
        HandleJump(); // �W�����v�̏���
    }

    void HandleMovement()
    {
        float speed = 0f; // �ړ����x
        Vector3 movementDirection = Vector3.zero; // �ړ�����

        // �J�����̑O�����ƉE��������Ɉړ��������v�Z
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0f; // ���������̉e�������O
        right.y = 0f; // ���������̉e�������O
        forward.Normalize(); // ���K��
        right.Normalize(); // ���K��

        // �L�[�{�[�h�̓��͂��擾
        if (Input.GetKey(KeyCode.W))
        {
            // �V�t�g�L�[��������Ă���ꍇ�͑��s�A����ȊO�͕��s
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = 1f; // Running
            }
            else
            {
                speed = 0.5f; // Walking
            }
            movementDirection += forward;
            forwardRotation = Quaternion.LookRotation(movementDirection, Vector3.up); // �O�i���̌������X�V
        }
        if (Input.GetKey(KeyCode.S))
        {
            speed = -0.5f; // ��i
            movementDirection -= forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            // �V�t�g�L�[��������Ă���ꍇ�͍����s�A����ȊO�͍����s
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = 1f; // Running Left
            }
            else
            {
                speed = 0.5f; // Walking Left
            }
            movementDirection -= right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            // �V�t�g�L�[��������Ă���ꍇ�͉E���s�A����ȊO�͉E���s
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = 1f; // Running Right
            }
            else
            {
                speed = 0.5f; // Walking Right
            }
            movementDirection += right;
        }

        // �΂߈ړ��̏���
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift))
        {
            speed = 1f; // ���O�΂߃_�b�V��
            movementDirection = forward + right * -1;
        }
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift))
        {
            speed = 1f; // �E�O�΂߃_�b�V��
            movementDirection = forward + right;
        }

        // �ړ��̎��s
        if (movementDirection != Vector3.zero)
        {
            movementDirection.Normalize(); // �ړ������̐��K��
            float actualSpeed = Mathf.Abs(speed) == 1f ? runSpeed : walkSpeed; // ���ۂ̈ړ����x
            transform.Translate(movementDirection * actualSpeed * Time.deltaTime, Space.World);

            // �O�i�܂��͉��ړ��̏ꍇ�̂݃��[�e�[�V������ύX
            if (speed > 0 || speed == 0.5f)
            {
                // �v���C���[�̌������ړ������ɑ����ɐݒ�
                transform.rotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            }
            // ��i�̏ꍇ�͑O�i���̌������g�p
            else if (speed < 0)
            {
                transform.rotation = forwardRotation;
            }

            // �A�j���[�V�����p�����[�^�[��ݒ�
            animator.SetFloat("Speed", speed);
        }
        else
        {
            animator.SetFloat("Speed", 0f);
        }
    }

    void HandleJump()
    {
        // �W�����v
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            animator.SetTrigger("JumpStart");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // �n�ʂƂ̏Փ˂����m���ăW�����v��Ԃ����Z�b�g
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            animator.SetTrigger("JumpEnd");
        }
    }
}
