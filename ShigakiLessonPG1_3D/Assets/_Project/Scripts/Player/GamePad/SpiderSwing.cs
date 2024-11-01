using UnityEngine;

public class SpiderSwing : MonoBehaviour
{
    [SerializeField] private float swingForce = 600f; // スイング時の推進力。スイング中の移動速度を決定
    [SerializeField] private float maxSwingSpeed = 35f; // スイング中の最大速度
    [SerializeField] private float swingSlowDownFactor = 0.95f; // スイング時の速度減衰率
    [SerializeField] private float springStrength = 300f; // スプリングの力（引っ張り強度）
    [SerializeField] private float damping = 7f; // スプリングの減衰力。引っ張りの「揺れ」の抑制に使用
    [SerializeField] private float maxDistanceFactor = 1.2f; // スイング開始時の最大距離倍率
    [SerializeField] private float minDistanceFactor = 0.5f; // スイング開始時の最小距離倍率

    public LayerMask grappleLayer; // ワイヤーを引っ掛けられるオブジェクトのレイヤーを指定
    public Transform grappleOrigin; // ワイヤー発射位置（プレイヤーからの相対位置などを指定する）
    public LineRenderer lineRenderer; // ワイヤーを描画するためのLineRendererコンポーネント
    public GameObject swingTargetPrefab; // ターゲットポイントのビジュアルオブジェクト（目標地点を可視化）

    protected SpringJoint rightSpringJoint; // 右ワイヤー用のSpringJoint（物理的なワイヤーの挙動を設定）
    protected SpringJoint leftSpringJoint; // 左ワイヤー用のSpringJoint（左右のワイヤー使用が想定されている場合）

    public bool IsSwinging { get; private set; } = false; // スイング中かどうかの状態を外部から参照できるプロパティ

    private Rigidbody playerRigidbody; // プレイヤーのRigidbodyコンポーネント。物理的な移動や力の適用に使用
    private Vector3 grapplePoint; // ワイヤーが接続される目標地点の座標
    private GameObject swingTarget; // 目標地点を示すオブジェクト（スイング中に目標地点を可視化）

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>(); // プレイヤーのRigidbodyを取得し、スイング時の力の加算に使用
        swingTarget = Instantiate(swingTargetPrefab); // 目標地点の可視化オブジェクトを生成
        swingTarget.SetActive(false); // 初期状態では非表示
    }

    private void Update()
    {
        // Fire5ボタンが押された場合にスイングを開始し、離された場合に終了
        if (Input.GetButtonDown("Fire5")) StartSwing();
        if (Input.GetButtonUp("Fire5")) StopSwing();
    }

    // スイングを開始するメソッド
    private void StartSwing()
    {
        // グラップルポイントが見つかった場合にスイング開始
        if (TryFindGrapplePoint())
        {
            IsSwinging = true; // スイング状態を開始
            rightSpringJoint = gameObject.AddComponent<SpringJoint>(); // 右ワイヤー用のSpringJointコンポーネントを追加
            rightSpringJoint.autoConfigureConnectedAnchor = false; // 自動設定をオフにしてカスタム設定を使用
            rightSpringJoint.connectedAnchor = grapplePoint; // グラップルポイントをSpringJointの接続先として設定

            // ワイヤーの最大/最小距離を設定
            float distanceToGrapplePoint = Vector3.Distance(grappleOrigin.position, grapplePoint);
            rightSpringJoint.maxDistance = distanceToGrapplePoint * maxDistanceFactor; // 接続時の最大距離を倍率で調整
            rightSpringJoint.minDistance = distanceToGrapplePoint * minDistanceFactor; // 接続時の最小距離を倍率で調整
            rightSpringJoint.spring = springStrength; // スプリングの強度（引っ張り力）を設定
            rightSpringJoint.damper = damping; // スプリングの減衰力（揺れの抑制）を設定

            lineRenderer.positionCount = 2; // ワイヤーの描画を設定
            swingTarget.transform.position = grapplePoint; // 目標地点の位置を更新
            swingTarget.SetActive(true); // 目標地点の可視化オブジェクトを表示
        }
    }

    // スイングを停止するメソッド
    private void StopSwing()
    {
        if (rightSpringJoint != null)
        {
            Destroy(rightSpringJoint); // SpringJointを削除してスイングを終了
            IsSwinging = false; // スイング状態をリセット
        }
        lineRenderer.positionCount = 0; // ワイヤーの描画をクリア
        swingTarget.SetActive(false); // 目標地点の可視化オブジェクトを非表示に
    }

    private void FixedUpdate()
    {
        if (IsSwinging)
        {
            ApplySwingForce(); // スイング中はSwingForceを適用して移動を制御
        }
    }

    // スイングの目標地点を見つけるメソッド
    private bool TryFindGrapplePoint()
    {
        RaycastHit hit;
        Vector3[] directions = {
            grappleOrigin.up + grappleOrigin.forward, // 前方斜め上方向
            grappleOrigin.up - grappleOrigin.right,   // 右斜め上方向
            grappleOrigin.up + grappleOrigin.right    // 左斜め上方向
        };

        // 各方向に向かってSphereCastを実行し、障害物があるかチェック
        foreach (var dir in directions)
        {
            if (Physics.SphereCast(grappleOrigin.position, 1f, dir, out hit, 50f, grappleLayer))
            {
                grapplePoint = hit.point; // ヒットした地点をグラップルポイントとして設定
                return true; // グラップルポイントの取得に成功
            }
        }

        return false; // グラップルポイントが見つからなかった場合
    }

    // スイング中の移動力を適用するメソッド
    private void ApplySwingForce()
    {
        // プレイヤーが設定した最大速度未満の場合にスイング力を加える
        if (playerRigidbody.velocity.magnitude < maxSwingSpeed)
        {
            Vector3 direction = (grapplePoint - transform.position).normalized; // グラップルポイントに向かう方向を計算
            playerRigidbody.AddForce(direction * swingForce, ForceMode.Acceleration); // 計算した方向にスイング力を適用
        }
        else
        {
            // 最大速度を超えた場合、速度を減速
            playerRigidbody.velocity *= swingSlowDownFactor;
        }
    }
}
