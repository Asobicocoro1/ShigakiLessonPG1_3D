using UnityEngine;

public class Grapple
{
    public bool IsGrappling { get; private set; } = false;

    private SpringJoint joint; // プレイヤーを引き寄せるためのSpringJoint
    private LineRenderer lineRenderer; // ワイヤーの描画に使用するLineRenderer
    private Rigidbody playerRigidbody; // プレイヤーのRigidbody
    private Transform cameraTransform; // カメラのTransform
    private Transform grappleOrigin; // ワイヤーが発射される起点のTransform
    private Vector3 grapplePoint; // ワイヤーが引っかかるポイント
    private LayerMask grappleLayer; // グラップル可能なオブジェクトのレイヤーマスク
    private float maxGrappleDistance; // ワイヤーが届く最大距離

    // コンストラクタ: Grappleクラスを初期化
    public Grapple(Rigidbody player, Transform camera, Transform grappleOrigin, LineRenderer lineRenderer, LayerMask grappleLayer, float maxDistance)
    {
        this.playerRigidbody = player;
        this.cameraTransform = camera;
        this.grappleOrigin = grappleOrigin;
        this.lineRenderer = lineRenderer;
        this.grappleLayer = grappleLayer;
        this.maxGrappleDistance = maxDistance;
    }

    // ワイヤーを発射しようとする関数
    public void TryStartGrapple()
    {
        RaycastHit hit;
        // カメラの前方にレイキャストを飛ばし、グラップル可能なポイントがあるか確認
        if (Physics.Raycast(grappleOrigin.position, cameraTransform.forward, out hit, maxGrappleDistance, grappleLayer))
        {
            StartGrapple(hit.point); // グラップルポイントが見つかったら、ワイヤーを開始
        }
        else
        {
            Debug.Log("グラップルポイントが見つかりませんでした。");
        }
    }

    // ワイヤー発射の処理
    private void StartGrapple(Vector3 hitPoint)
    {
        grapplePoint = hitPoint; // グラップルする場所を設定
        IsGrappling = true; // グラップリング中に設定

        // プレイヤーにSpringJointを追加し、引き寄せられるようにする
        joint = playerRigidbody.gameObject.AddComponent<SpringJoint>();
        joint.autoConfigureConnectedAnchor = false; // 手動で接続位置を設定
        joint.connectedAnchor = grapplePoint; // グラップルポイントに接続

        float distanceToGrapplePoint = Vector3.Distance(grappleOrigin.position, grapplePoint); // 距離を計算

        // SpringJointの設定（スイングの感覚を調整）
        joint.maxDistance = distanceToGrapplePoint * 0.8f; // ワイヤーの最大距離
        joint.minDistance = distanceToGrapplePoint * 0.25f; // ワイヤーの最小距離
        joint.spring = 4.5f;  // スプリングの力（高いほど強く引っ張られる）
        joint.damper = 7f;    // 減衰力（高いほどスムーズに動作）
        joint.massScale = 4.5f; // 質量のスケーリング（プレイヤーの質量に影響）

        // LineRendererの描画準備
        lineRenderer.positionCount = 2; // ワイヤーの描画頂点を2つに設定
    }

    // ワイヤー解除の処理
    public void StopGrapple()
    {
        if (joint != null)
        {
            GameObject.Destroy(joint); // SpringJointを破壊し、プレイヤーを解放
        }
        IsGrappling = false;
        lineRenderer.positionCount = 0; // ワイヤーの描画を停止
    }

    // ワイヤーを描画する処理
    public void DrawRope()
    {
        if (!IsGrappling) return; // グラップリング中でない場合は描画しない

        // ワイヤーの始点と終点を設定
        lineRenderer.SetPosition(0, grappleOrigin.position); // ワイヤーの発射位置
        lineRenderer.SetPosition(1, grapplePoint); // ワイヤーの接続ポイント
    }
}
