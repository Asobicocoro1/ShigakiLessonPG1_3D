using UnityEngine;

public class PlayerpadController : MonoBehaviour
{
    [SerializeField] private Animator animator; // アニメーターコンポーネント（アニメーション管理用）
    [SerializeField] private float walkSpeed = 2.0f; // 歩行速度
    [SerializeField] private float runSpeed = 6.0f; // 走行速度
    [SerializeField] private float slideDuration = 1.0f; // スライディングの持続時間

    private bool isSliding = false; // スライディング中かどうかのフラグ
    private Rigidbody rb; // Rigidbodyコンポーネント（物理挙動を管理）
    private Transform cameraTransform; // カメラのTransform
    private float slideStartTime; // スライディング開始時の時間を記録
    private bool isRunning = false; // ダッシュ中かどうかのフラグ

    [SerializeField] private BoxCollider normalCollider; // 通常時のBoxCollider
    [SerializeField] private BoxCollider slideCollider; // スライディング時のBoxCollider

    private SpiderSwing swingController; // SpiderSwingコンポーネント（スイング動作管理用）

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbodyコンポーネントを取得
        cameraTransform = Camera.main.transform; // メインカメラのTransformを取得
        swingController = GetComponent<SpiderSwing>(); // SpiderSwingコンポーネントを取得
        slideCollider.enabled = false; // スライディング用のColliderを初期状態で無効に
    }

    void Update()
    {
        // スイング中でない、かつスライディング中でない場合に移動を処理
        if (!isSliding && (swingController == null || !swingController.IsSwinging))
        {
            HandleMovement();
        }
        HandleSlide(); // スライディングの処理
    }

    private void HandleMovement()
    {
        float speed = GetMovementSpeed(); // 移動速度を取得
        Vector3 movementDirection = CalculateMovementDirection(speed); // 移動方向を計算

        if (movementDirection != Vector3.zero)
        {
            MoveCharacter(movementDirection, speed); // キャラクターを移動させる
        }
        else
        {
            // 移動していない場合、アニメーションを停止
            animator.SetFloat("Speed", 0f);
            isRunning = false; // ダッシュ状態をリセット
        }
    }

    private void HandleSlide()
    {
        bool isSlidingInput = GamepadInputManager.Instance.GetButtonDown("Slide"); // スライディングの入力取得

        // スライディング開始
        if (isSlidingInput && !isSliding)
        {
            StartSlide();
        }

        // スライディング終了
        if (isSliding && Time.time - slideStartTime > slideDuration)
        {
            EndSlide();
        }
    }

    private void StartSlide()
    {
        isSliding = true;
        slideStartTime = Time.time; // スライディング開始時間を記録
        animator.SetTrigger("Slide"); // スライディングのアニメーションをトリガー
        animator.applyRootMotion = false; // Root Motionを無効化
        normalCollider.enabled = false; // 通常Colliderを無効化
        slideCollider.enabled = true; // スライディング用Colliderを有効化
    }

    private void EndSlide()
    {
        isSliding = false;
        normalCollider.enabled = true; // 通常Colliderを有効化
        slideCollider.enabled = false; // スライディング用Colliderを無効化
        animator.applyRootMotion = true; // Root Motionを再度有効化
    }

    // カメラの方向を基に移動方向を計算
    private Vector3 CalculateMovementDirection(float speed)
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0f; // Y軸の回転を無視（上下方向の回転を無効化）
        right.y = 0f;

        Vector3 direction = Vector3.zero;
        float moveX = GamepadInputManager.Instance.GetAxis("MoveHorizontal"); // 横方向の入力
        float moveY = GamepadInputManager.Instance.GetAxis("MoveVertical"); // 縦方向の入力

        if (moveY > 0) direction += forward; // 前進
        if (moveY < 0) direction -= forward; // 後退
        if (moveX < 0) direction -= right; // 左移動
        if (moveX > 0) direction += right; // 右移動

        return direction;
    }

    // キャラクターを移動させる
    private void MoveCharacter(Vector3 direction, float speed)
    {
        float actualSpeed = isRunning ? runSpeed : walkSpeed;
        transform.Translate(direction * actualSpeed * Time.deltaTime, Space.World); // キャラクターの移動処理

        // キャラクターの回転を移動方向に合わせる
        Vector3 lookDirection = new Vector3(direction.x, 0, direction.z);
        if (lookDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }

        animator.SetFloat("Speed", Mathf.Abs(actualSpeed)); // アニメーション速度の更新
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 1f); // Z軸スケールを固定
    }

    // 移動速度を返す
    private float GetMovementSpeed()
    {
        float verticalAxis = GamepadInputManager.Instance.GetAxis("MoveVertical");
        return verticalAxis != 0 ? 0.5f : 0f; // 移動があるときのみ速度を返す
    }
}
