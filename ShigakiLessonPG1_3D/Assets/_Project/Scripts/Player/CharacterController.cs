using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Animator animator; // Animator �R���|�[�l���g
    public float walkSpeed = 2.0f;
    public float runSpeed = 6.0f;
    public float backwardSpeed = 2.0f; // ��i���x
    public float jumpForce = 5.0f;

    private bool isJumping = false;
    private Rigidbody rb;
    private Transform cameraTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform; // ���C���J������Transform���擾
    }

    void Update()
    {
        float speed = 0f;
        Vector3 movementDirection = Vector3.zero;

        // �J�����̑O�����ƉE��������Ɉړ��������v�Z
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0f; // ���������̉e�������O
        right.y = 0f; // ���������̉e�������O
        forward.Normalize();
        right.Normalize();

        // �L�[�{�[�h�̓��͂��擾
        if (Input.GetKey(KeyCode.W))
        {
            speed = Input.GetKey(KeyCode.LeftShift) ? 1f : 0.5f; // Running or Walking
            movementDirection += forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            speed = -0.5f; // Walking Backward
            movementDirection -= forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            speed = 0.5f; // Walking Left
            movementDirection -= right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            speed = 0.5f; // Walking Right
            movementDirection += right;
        }

        // �ړ��̎��s
        if (movementDirection != Vector3.zero)
        {
            movementDirection.Normalize();
            float actualSpeed = Mathf.Abs(speed) > 0.5f ? runSpeed : walkSpeed;
            transform.Translate(movementDirection * actualSpeed * Time.deltaTime, Space.World);

            // �v���C���[�̌������ړ������ɑ����ɐݒ�
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = toRotation;

            // �A�j���[�V�����p�����[�^�[��ݒ�
            animator.SetFloat("Speed", speed);
        }
        else
        {
            animator.SetFloat("Speed", 0f);
        }

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
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            animator.SetTrigger("JumpEnd");
        }
    }
}
