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
        // 入力の取得
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // キャラクターの移動
        Vector3 move = new Vector3(horizontalInput, 0, verticalInput) * moveSpeed * Time.deltaTime;
        transform.Translate(move, Space.Self);

        // Animatorパラメーターの設定
        float speed = new Vector3(horizontalInput, 0, verticalInput).magnitude;
        animator.SetFloat("speed", speed);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        // ジャンプ関連のパラメーター設定
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

    // アニメーションイベントハンドラ
    public void OnLand()
    {
        // 着地時の処理をここに記述します
        Debug.Log("着地しました！");
        isGrounded = true;
    }

    // アニメーションイベントハンドラ
    public void OnFootstep()
    {
        // 足音の処理をここに記述します
        Debug.Log("足音が鳴りました！");
        // 足音の効果音を再生する場合など
    }
}



