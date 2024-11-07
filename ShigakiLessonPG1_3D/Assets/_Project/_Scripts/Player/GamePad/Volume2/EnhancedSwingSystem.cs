using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnhancedSwingSystem : MonoBehaviour
{
    [Header("Swing Settings")]
    [SerializeField] private float swingForce = 1000; // スイング時の力
    private float maxSwingSpeed = 30f; // スイング中の最大速度
    private float springStrength = 900f; // スプリングの強度
    [SerializeField] private float damping = 7f; // ダンパー（減衰）の強さ

    [Header("Wire Visual Settings")]
    [SerializeField] private Color wireColor = Color.yellow; // ワイヤーの色

    private SpringJoint swingJoint; // スイング用のSpringJoint
    private Rigidbody rb; // プレイヤーのRigidbody
    private bool isSwinging; // 現在スイング中かどうか
    private Transform grapplePoint; // グラップル先のポイント
    private LockOnManager lockOnManager; // LockOnManager（ターゲット管理）
    private LineRenderer lineRenderer; // ワイヤーの視覚表現用

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        lockOnManager = FindObjectOfType<LockOnManager>(); // LockOnManagerを検索して取得

        // LineRendererの初期設定
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = wireColor;
        lineRenderer.endColor = wireColor;
        lineRenderer.positionCount = 0; // 初期状態ではポジションを0に設定
        lineRenderer.enabled = false; // 初期状態では無効化
    }

    private void Update()
    {
        // LockOnManagerから現在のターゲットを取得
        grapplePoint = lockOnManager.CurrentTarget;

        // GamepadInputManagerを使ってボタン入力を取得
        if (grapplePoint != null && GamepadInputManager.Instance.GetButtonDown("Grapple"))
        {
            StartSwing(); // スイング開始
        }
        if (GamepadInputManager.Instance.GetButtonUp("Grapple"))
        {
            StopSwing(); // スイング終了
        }

        if (isSwinging)
        {
            UpdateLineRenderer(); // ワイヤーのLineRendererを更新
        }
    }

    private void StartSwing()
    {
        if (grapplePoint != null)
        {
            isSwinging = true; // スイング状態を有効化

            // スイング用のSpringJointを作成して設定
            swingJoint = gameObject.AddComponent<SpringJoint>();
            swingJoint.autoConfigureConnectedAnchor = false;
            swingJoint.connectedAnchor = grapplePoint.position; // グラップル先を設定
            swingJoint.spring = springStrength; // スプリングの強度を設定
            swingJoint.damper = damping; // ダンパーの強度を設定

            // プレイヤーとグラップル先の距離を計算
            float distance = Vector3.Distance(transform.position, grapplePoint.position);
            swingJoint.maxDistance = distance * 0.8f; // 最大距離を設定
            swingJoint.minDistance = distance * 0.2f; // 最小距離を設定

            // LineRendererを有効化
            lineRenderer.positionCount = 2;
            lineRenderer.enabled = true;
        }
    }

    private void StopSwing()
    {
        if (swingJoint)
        {
            Destroy(swingJoint); // SpringJointを破棄
            isSwinging = false; // スイング状態を無効化
            lineRenderer.positionCount = 0; // LineRendererをリセット
            lineRenderer.enabled = false; // LineRendererを無効化
        }
    }

    private void UpdateLineRenderer()
    {
        if (lineRenderer.positionCount >= 2)
        {
            // LineRendererの両端の位置を更新
            lineRenderer.SetPosition(0, transform.position); // プレイヤーの位置
            lineRenderer.SetPosition(1, grapplePoint.position); // グラップル先の位置
        }
    }
}
