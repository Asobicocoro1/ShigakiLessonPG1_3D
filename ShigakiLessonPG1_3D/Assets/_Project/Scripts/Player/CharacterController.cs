using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Animator animator; // Animator コンポーネント
    public float walkSpeed = 2.0f; // 歩行速度
    public float runSpeed = 6.0f; // 走行速度
    public float backwardSpeed = 2.0f; // 後進速度
    public float jumpForce = 5.0f; // ジャンプ力

    private bool isJumping = false; // ジャンプ中かどうかのフラグ
    private Rigidbody rb; // Rigidbody コンポーネント
    private Transform cameraTransform; // メインカメラのTransform
    private Quaternion forwardRotation; // 前進時の向き

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody コンポーネントを取得
        cameraTransform = Camera.main.transform; // メインカメラのTransformを取得
        forwardRotation = transform.rotation; // 初期の向きを保存
    }

    void Update()
    {
        // 移動とジャンプの処理を呼び出す
        HandleMovement(); // 移動の処理
        HandleJump(); // ジャンプの処理
    }

    void HandleMovement()
    {
        float speed = 0f; // 移動速度
        Vector3 movementDirection = Vector3.zero; // 移動方向

        // カメラの前方向と右方向を基準に移動方向を計算
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0f; // 垂直方向の影響を除外
        right.y = 0f; // 垂直方向の影響を除外
        forward.Normalize(); // 正規化
        right.Normalize(); // 正規化

        // キーボードの入力を取得
        if (Input.GetKey(KeyCode.W))
        {
            // シフトキーが押されている場合は走行、それ以外は歩行
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = 1f; // Running
            }
            else
            {
                speed = 0.5f; // Walking
            }
            movementDirection += forward;
            forwardRotation = Quaternion.LookRotation(movementDirection, Vector3.up); // 前進時の向きを更新
        }
        if (Input.GetKey(KeyCode.S))
        {
            speed = -0.5f; // 後進
            movementDirection -= forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            // シフトキーが押されている場合は左走行、それ以外は左歩行
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
            // シフトキーが押されている場合は右走行、それ以外は右歩行
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

        // 斜め移動の処理
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift))
        {
            speed = 1f; // 左前斜めダッシュ
            movementDirection = forward + right * -1;
        }
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift))
        {
            speed = 1f; // 右前斜めダッシュ
            movementDirection = forward + right;
        }

        // 移動の実行
        if (movementDirection != Vector3.zero)
        {
            movementDirection.Normalize(); // 移動方向の正規化
            float actualSpeed = Mathf.Abs(speed) == 1f ? runSpeed : walkSpeed; // 実際の移動速度
            transform.Translate(movementDirection * actualSpeed * Time.deltaTime, Space.World);

            // 前進または横移動の場合のみローテーションを変更
            if (speed > 0 || speed == 0.5f)
            {
                // プレイヤーの向きを移動方向に即座に設定
                transform.rotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            }
            // 後進の場合は前進時の向きを使用
            else if (speed < 0)
            {
                transform.rotation = forwardRotation;
            }

            // アニメーションパラメーターを設定
            animator.SetFloat("Speed", speed);
        }
        else
        {
            animator.SetFloat("Speed", 0f);
        }
    }

    void HandleJump()
    {
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
        // 地面との衝突を検知してジャンプ状態をリセット
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            animator.SetTrigger("JumpEnd");
        }
    }
}
