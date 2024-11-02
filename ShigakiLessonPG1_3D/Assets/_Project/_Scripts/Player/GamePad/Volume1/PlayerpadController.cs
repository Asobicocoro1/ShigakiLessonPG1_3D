using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerpadController : MonoBehaviour
{
    [SerializeField] private Animator animator; // Animator コンポーネント
    [SerializeField] private float walkSpeed = 2.0f; // 歩行速度
    [SerializeField] private float runSpeed = 6.0f; // 走行速度
    //[SerializeField] private float backwardSpeed = 1.5f; // 後進速度
    [SerializeField] private float slideDuration = 1.0f; // スライディングの持続時間

    private bool isSliding = false; // スライディング中かどうかのフラグ
    private Rigidbody rb; // Rigidbody コンポーネント
    private Transform cameraTransform; // カメラのTransform
    private float slideStartTime; // スライディングが開始された時刻
    private float originalYPosition; // スライディング開始時のY座標を記録

    private float walkTime = 0f; // 歩き始めた時間を記録
    private bool isRunning = false; // ダッシュ中かどうかのフラグ

    [SerializeField] private BoxCollider normalCollider; // 通常時のBoxCollider
    [SerializeField] private BoxCollider slideCollider;  // スライディング時のBoxCollider

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody コンポーネントを取得
        cameraTransform = Camera.main.transform; // メインカメラのTransformを取得

        // 初期状態ではスライディング用のBoxColliderは無効にしておく
        slideCollider.enabled = false;
    }

    void Update()
    {
        if (!isSliding)
        {
            HandleMovement(); // 通常の移動処理
        }

        HandleSlide(); // スライディング処理
    }

    private void HandleMovement()
    {
        float speed = GetMovementSpeed(); // 速度を取得
        Vector3 movementDirection = CalculateMovementDirection(speed); // 移動方向を取得

        if (movementDirection != Vector3.zero)
        {
            MoveCharacter(movementDirection, speed);
        }
        else
        {
            // 移動していないときはアニメーションを停止
            animator.SetFloat("Speed", 0f);
            walkTime = 0f; // 歩き時間をリセット
            isRunning = false; // ダッシュをリセット
        }
    }

    private void HandleSlide()
    {
        bool isSlidingInput = GamepadInputManager.Instance.GetButtonDown("Slide"); // スライディングを開始する入力を取得

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
        originalYPosition = transform.position.y; // スライディング開始時のY座標を記録

        animator.SetTrigger("Slide");
        animator.applyRootMotion = false; // Root Motionを無効化

        // 通常のBoxColliderを無効にし、スライディング用のBoxColliderを有効にする
        normalCollider.enabled = false;
        slideCollider.enabled = true;
    }

    private void EndSlide()
    {
        isSliding = false;

        // スライディング用のBoxColliderを無効にし、通常のBoxColliderを有効にする
        normalCollider.enabled = true;
        slideCollider.enabled = false;

        animator.applyRootMotion = true; // Root Motionを再度有効化
    }

    // カメラの方向を基に移動方向を計算するメソッド
    private Vector3 CalculateMovementDirection(float speed)
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0f; // 上下方向の回転を無視
        right.y = 0f;

        //forward.Normalize();
        //right.Normalize();

        Vector3 direction = Vector3.zero;

        float moveX = GamepadInputManager.Instance.GetAxis("MoveHorizontal");
        float moveY = GamepadInputManager.Instance.GetAxis("MoveVertical");

        if (moveY > 0) direction += forward; // 前進
        if (moveY < 0) direction -= forward; // 後退

        if (moveX < 0) direction -= right;   // 左移動
        if (moveX > 0) direction += right;   // 右移動

        return direction;
    }

    // キャラクターを移動させるメソッド
    private void MoveCharacter(Vector3 direction, float speed)
    {
        // 歩き時間を更新
        if (!isRunning && direction != Vector3.zero)
        {
            walkTime += Time.deltaTime; // 歩行時間を加算
        }

        // 2秒間歩行し続けたらダッシュに切り替え
        if (walkTime >= 2f && !isRunning)
        {
            isRunning = true; // ダッシュを開始
            walkTime = 0f; // 歩行時間をリセット
        }

        // ダッシュ時にはランスピード、通常時にはウォークスピード
        float actualSpeed = isRunning ? runSpeed : walkSpeed;

        // キャラクターを移動させる
        transform.Translate(direction * actualSpeed * Time.deltaTime, Space.World);

        // カメラのY軸方向にキャラクターの回転を合わせる
        Vector3 lookDirection = new Vector3(direction.x, 0, direction.z);

        if (lookDirection != Vector3.zero)
        {
            // X軸の回転を除外してキャラクターを回転させる
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }

        // 速度に関係なく前進のアニメーションを再生
        animator.SetFloat("Speed", Mathf.Abs(actualSpeed));

        // 前進のみを表示するため、Zスケールを常に正にする
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 1f);
    }

    // 走行、歩行に応じて移動速度を返すメソッド
    private float GetMovementSpeed()
    {
        float verticalAxis = GamepadInputManager.Instance.GetAxis("MoveVertical");

        if (verticalAxis != 0)
        {
            return 0.5f; // 通常の歩行速度を返す
        }

        return 0f; // 動いていない場合
    }
}