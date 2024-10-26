using System.Collections;
using UnityEngine;

public class SpiderSwing : MonoBehaviour
{
    // SpringJointの設定用パラメータ（インスペクターで調整可能）
     private float maxDistanceFactor = 1.2f;   // 最大距離倍率
     private float minDistanceFactor = 0.25f;  // 最小距離倍率
     private float swingSpringForce = 300f;    // スイングのスプリング力
     private float swingDamper = 7f;           // スイングの減衰力
     private float massScale = 4.5f;           // 質量スケール

    // スイング時の加速と速度制限
     private float swingForce = 500f;          // スイング加速力
     private float maxSwingSpeed = 28f;        // 最大スイング速度

    // ワイヤーアクション関連パラメータ
    public LayerMask grappleLayer;                             // グラップル可能なレイヤー
    public Transform rightGrappleOrigin;                       // 右ワイヤーの発射地点
    public Transform leftGrappleOrigin;                        // 左ワイヤーの発射地点
    public float maxGrappleDistance = 50f;                     // グラップル距離の最大値
    public LineRenderer rightLineRenderer;                     // 右ワイヤーのLineRenderer
    public LineRenderer leftLineRenderer;                      // 左ワイヤーのLineRenderer
    public GameObject swingTargetPrefab;                       // グラップルポイントの可視化オブジェクト

    // 内部変数（SpringJointとスイング用オブジェクト）
    protected SpringJoint rightSpringJoint;
    protected SpringJoint leftSpringJoint;
    private Rigidbody playerRigidbody;
    private Vector3 rightGrapplePoint;
    private Vector3 leftGrapplePoint;
    private GameObject rightSwingTarget;
    private GameObject leftSwingTarget;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();

        // グラップルターゲットの初期設定（常に表示）
        rightSwingTarget = Instantiate(swingTargetPrefab);
        leftSwingTarget = Instantiate(swingTargetPrefab);
        rightSwingTarget.SetActive(true);
        leftSwingTarget.SetActive(true);
    }

    private void Update()
    {
        HandleGrappleInput(); // グラップル入力の処理
        DrawRope();           // ワイヤー描画
        ApplySwingForce();    // スイング力の適用
    }

    // グラップルの入力を処理
    private void HandleGrappleInput()
    {
        if (Input.GetButtonDown("Fire5"))
        {
            TryMultipleDirectionGrapple(rightGrappleOrigin, ref rightSpringJoint, ref rightGrapplePoint, rightLineRenderer, rightSwingTarget);
        }

        if (Input.GetButtonDown("Fire6"))
        {
            TryMultipleDirectionGrapple(leftGrappleOrigin, ref leftSpringJoint, ref leftGrapplePoint, leftLineRenderer, leftSwingTarget);
        }

        if (Input.GetButtonUp("Fire5"))
        {
            StopGrapple(ref rightSpringJoint, rightLineRenderer, rightSwingTarget);
        }

        if (Input.GetButtonUp("Fire6"))
        {
            StopGrapple(ref leftSpringJoint, leftLineRenderer, leftSwingTarget);
        }
    }

    // スイングの加速力を適用
    private void ApplySwingForce()
    {
        if (rightSpringJoint != null || leftSpringJoint != null)
        {
            // 左スティックの入力を基にスイング方向の力を適用
            float horizontalInput = GamepadInputManager.Instance.GetAxis("MoveHorizontal");
            float verticalInput = GamepadInputManager.Instance.GetAxis("MoveVertical");

            Vector3 swingDirection = new Vector3(horizontalInput, 0, verticalInput);
            swingDirection = Camera.main.transform.TransformDirection(swingDirection);
            swingDirection.y = 0;

            playerRigidbody.AddForce(swingDirection * swingForce, ForceMode.Acceleration);

            // 最大速度を制限
            if (playerRigidbody.velocity.magnitude > maxSwingSpeed)
            {
                playerRigidbody.velocity = playerRigidbody.velocity.normalized * maxSwingSpeed;
            }
        }
    }

    // さまざまな方向にキャストしてグラップル可能なポイントを探す
    private bool TryMultipleDirectionGrapple(Transform grappleOrigin, ref SpringJoint springJoint, ref Vector3 grapplePoint, LineRenderer lineRenderer, GameObject swingTarget)
    {
        return TryGrappleInDirection(grappleOrigin, grappleOrigin.up - grappleOrigin.forward, ref springJoint, ref grapplePoint, lineRenderer, swingTarget) ||
               TryGrappleInDirection(grappleOrigin, grappleOrigin.up + Camera.main.transform.forward, ref springJoint, ref grapplePoint, lineRenderer, swingTarget) ||
               TryGrappleInDirection(grappleOrigin, Camera.main.transform.forward - grappleOrigin.forward, ref springJoint, ref grapplePoint, lineRenderer, swingTarget);
    }

    // 指定した方向にグラップルを試行
    private bool TryGrappleInDirection(Transform grappleOrigin, Vector3 direction, ref SpringJoint springJoint, ref Vector3 grapplePoint, LineRenderer lineRenderer, GameObject swingTarget)
    {
        RaycastHit hit;

        if (Physics.SphereCast(grappleOrigin.position, 2f, direction, out hit, maxGrappleDistance, grappleLayer))
        {
            grapplePoint = hit.point;
            StartGrapple(grappleOrigin, ref springJoint, grapplePoint, lineRenderer, swingTarget);
            return true;
        }
        else
        {
            swingTarget.SetActive(false);
            return false;
        }
    }

    // スイングを開始
    private void StartGrapple(Transform grappleOrigin, ref SpringJoint springJoint, Vector3 grapplePoint, LineRenderer lineRenderer, GameObject swingTarget)
    {
        springJoint = gameObject.AddComponent<SpringJoint>();
        springJoint.autoConfigureConnectedAnchor = false;
        springJoint.connectedAnchor = grapplePoint;

        float distanceToGrapplePoint = Vector3.Distance(grappleOrigin.position, grapplePoint);
        springJoint.maxDistance = distanceToGrapplePoint * maxDistanceFactor;
        springJoint.minDistance = distanceToGrapplePoint * minDistanceFactor;
        springJoint.spring = swingSpringForce;
        springJoint.damper = swingDamper;
        springJoint.massScale = massScale;

        lineRenderer.positionCount = 2;
        swingTarget.transform.position = grapplePoint;
        swingTarget.SetActive(true);
    }

    // スイングを停止
    private void StopGrapple(ref SpringJoint springJoint, LineRenderer lineRenderer, GameObject swingTarget)
    {
        if (springJoint != null)
        {
            Destroy(springJoint);
        }
        lineRenderer.positionCount = 0;
        swingTarget.SetActive(false);
    }

    // ワイヤーの描画
    private void DrawRope()
    {
        if (rightSpringJoint != null)
        {
            rightLineRenderer.positionCount = 2;
            rightLineRenderer.SetPosition(0, rightGrappleOrigin.position);
            rightLineRenderer.SetPosition(1, rightGrapplePoint);
        }
        else
        {
            rightLineRenderer.positionCount = 0;
        }

        if (leftSpringJoint != null)
        {
            leftLineRenderer.positionCount = 2;
            leftLineRenderer.SetPosition(0, leftGrappleOrigin.position);
            leftLineRenderer.SetPosition(1, leftGrapplePoint);
        }
        else
        {
            leftLineRenderer.positionCount = 0;
        }
    }
}
