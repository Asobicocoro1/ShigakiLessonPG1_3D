using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    private Rigidbody rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (GameManager.instance.IsGameOver) // プロパティを使用して状態を確認
            return;

        Move();
        Jump();
        Attack();
    }

    void Move()
    {
        float move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector3(move * moveSpeed, rb.velocity.y, 0);
        animator.SetFloat("Speed", Mathf.Abs(move));
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("Jump");
        }
    }

    void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Punch");
        }
        if (Input.GetButtonDown("Fire2"))
        {
            animator.SetTrigger("Kick");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameManager.instance.ReduceHealth(10);
        }
    }
}





