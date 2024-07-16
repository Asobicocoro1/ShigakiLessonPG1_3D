using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private float horizontalInput;
    private float verticalInput;

    private Animator animator;
    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // ���͂̎擾
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // �L�����N�^�[�̈ړ�
        Vector3 move = new Vector3(horizontalInput, 0, verticalInput) * moveSpeed * Time.deltaTime;
        transform.Translate(move, Space.Self);

        // Animator�p�����[�^�[�̐ݒ�
        float speed = new Vector3(horizontalInput, 0, verticalInput).magnitude;
        animator.SetFloat("speed", speed);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        // �W�����v�֘A�̃p�����[�^�[�ݒ�
        animator.SetBool("isJumping", !isGrounded);
        animator.SetBool("isGrounded", isGrounded);
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    // �A�j���[�V�����C�x���g�n���h��
    public void OnLand()
    {
        // ���n���̏����������ɋL�q���܂�
        Debug.Log("���n���܂����I");
        isGrounded = true;
    }

    // �A�j���[�V�����C�x���g�n���h��
    public void OnFootstep()
    {
        // �����̏����������ɋL�q���܂�
        Debug.Log("��������܂����I");
        // �����̌��ʉ����Đ�����ꍇ�Ȃ�
    }
}



