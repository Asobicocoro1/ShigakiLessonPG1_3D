using UnityEngine;

public class MyCharacterController : MonoBehaviour
{
    [SerializeField] private Animator animator; // Animator コンポーネント
    [SerializeField] private float walkSpeed = 2.0f; // 歩行速度
    [SerializeField] private float runSpeed = 6.0f; // 走行速度
    [SerializeField] private float backwardSpeed = 1.5f; // 後進速度
    [SerializeField] private float slideDuration = 1.0f; // スライディングの持続時間

    private bool isSliding = false; // スライディング中かどうかのフラグ
    private Rigidbody rb; // Rigidbody コンポーネント
    private Transform cameraTransform; // カメラのTransform
    private float slideStartTime; // スライディングが開始された時刻
    private float originalYPosition; // スライディング開始時のY座標を記録

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
            animator.SetFloat("Speed", 0f); // 移動していないときはアニメーションを停止
        }
    }

    private void HandleSlide()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isSliding)
        {
            StartSlide();
        }

        if (isSliding && Time.time - slideStartTime > slideDuration)
        {
            EndSlide();
        }

        if (isSliding)
        {
            LockYPosition(); // スライディング中のY座標を固定
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

    // Y軸の位置を固定するメソッド
    private void LockYPosition()
    {
        Vector3 position = transform.position;
        position.y = originalYPosition; // 開始時のY座標を維持
        transform.position = position;
    }

    // 走行、歩行、後退に応じて移動速度を返すメソッド
    private float GetMovementSpeed()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            return 1f; // シフトキーを押しながら前進で走行
        }
        if (Input.GetKey(KeyCode.S))
        {
            return -0.5f; // 後退時のスピードは後進速度
        }
        return 0.5f; // 通常の歩行速度
    }

    // カメラの方向を基に移動方向を計算するメソッド
    private Vector3 CalculateMovementDirection(float speed)
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0f; // 上下方向の回転を無視
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 direction = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) direction += forward; // 前進
        if (Input.GetKey(KeyCode.S)) direction -= forward; // 後退

        if (Input.GetKey(KeyCode.A)) direction -= right;   // 左移動
        if (Input.GetKey(KeyCode.D)) direction += right;   // 右移動

        return direction;
    }

    // キャラクターを移動させるメソッド
    private void MoveCharacter(Vector3 direction, float speed)
    {
        float actualSpeed = speed < 0 ? backwardSpeed : (Mathf.Abs(speed) == 1f ? runSpeed : walkSpeed);

        // キャラクターを移動させる
        transform.Translate(direction.normalized * actualSpeed * Time.deltaTime, Space.World);

        // カメラのY軸方向にキャラクターの回転を合わせる
        Vector3 lookDirection = new Vector3(direction.x, 0, direction.z); // X軸回転を無視する

        if (lookDirection != Vector3.zero)
        {
            // X軸の回転を除外してキャラクターを回転させる
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }

        // アニメーション速度を設定
        animator.SetFloat("Speed", speed < 0 ? -1f : Mathf.Abs(speed));

        // 後退時にZスケールを反転
        if (speed < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, -1f);
        }
        else
        {
            // 前進時には通常のスケールに戻す
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 1f);
        }
    }
}
