using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Animator animator; // Animator コンポーネント
    public float walkSpeed = 2.0f;
    public float runSpeed = 6.0f;
    public float backwardSpeed = 2.0f; // 後進速度
    public float jumpForce = 5.0f;

    private bool isJumping = false;
    private Rigidbody rb;
    private Transform cameraTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform; // メインカメラのTransformを取得
    }

    void Update()
    {
        float speed = 0f;
        Vector3 movementDirection = Vector3.zero;

        // カメラの前方向と右方向を基準に移動方向を計算
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0f; // 垂直方向の影響を除外
        right.y = 0f; // 垂直方向の影響を除外
        forward.Normalize();
        right.Normalize();

        // キーボードの入力を取得
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

        // 移動の実行
        if (movementDirection != Vector3.zero)
        {
            movementDirection.Normalize();
            float actualSpeed = Mathf.Abs(speed) > 0.5f ? runSpeed : walkSpeed;
            transform.Translate(movementDirection * actualSpeed * Time.deltaTime, Space.World);

            // プレイヤーの向きを移動方向に即座に設定
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = toRotation;

            // アニメーションパラメーターを設定
            animator.SetFloat("Speed", speed);
        }
        else
        {
            animator.SetFloat("Speed", 0f);
        }

        // ジャンプ
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
