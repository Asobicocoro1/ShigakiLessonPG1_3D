using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderSwing : MonoBehaviour
{
    // 右ワイヤー用のSpringJoint（protectedに変更）
    protected SpringJoint rightSpringJoint;
    // 左ワイヤー用のSpringJoint（protectedに変更）
    protected SpringJoint leftSpringJoint;

    public LayerMask grappleLayer; // グラップル可能なオブジェクトのレイヤー
    public Transform rightGrappleOrigin; // 右ワイヤーが発射される起点
    public Transform leftGrappleOrigin;  // 左ワイヤーが発射される起点
    public float maxGrappleDistance = 50f; // ワイヤーが届く最大距離
    public LineRenderer rightLineRenderer; // 右ワイヤー描画用
    public LineRenderer leftLineRenderer;  // 左ワイヤー描画用
    public float swingForce = 10f; // スイングの力

    private Rigidbody playerRigidbody; // プレイヤーのRigidbody
    private Vector3 rightGrapplePoint; // 右ワイヤーの引っ掛かり地点
    private Vector3 leftGrapplePoint;  // 左ワイヤーの引っ掛かり地点

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>(); // プレイヤーのRigidbodyを取得
    }

    private void Update()
    {
        HandleGrappleInput(); // グラップルの入力処理
        DrawRope(); // ワイヤーの描画処理
    }

    // グラップルの入力処理
    private void HandleGrappleInput()
    {
        // GamepadInputManagerを使って右ワイヤー用ボタンをチェック
        if (GamepadInputManager.Instance.GetButtonDown("GrappleRight"))
        {
            TryGrapple(rightGrappleOrigin, ref rightSpringJoint, ref rightGrapplePoint, rightLineRenderer);
        }

        // GamepadInputManagerを使って左ワイヤー用ボタンをチェック
        if (GamepadInputManager.Instance.GetButtonDown("GrappleLeft"))
        {
            TryGrapple(leftGrappleOrigin, ref leftSpringJoint, ref leftGrapplePoint, leftLineRenderer);
        }

        // ワイヤー解除
        if (GamepadInputManager.Instance.GetButtonUp("GrappleRight"))
        {
            StopGrapple(ref rightSpringJoint, rightLineRenderer);
        }
        if (GamepadInputManager.Instance.GetButtonUp("GrappleLeft"))
        {
            StopGrapple(ref leftSpringJoint, leftLineRenderer);
        }
    }

    // ワイヤー発射処理
    private void TryGrapple(Transform grappleOrigin, ref SpringJoint springJoint, ref Vector3 grapplePoint, LineRenderer lineRenderer)
    {
        RaycastHit hit;
        // SphereCastを使って近くのオブジェクトを判定
        if (Physics.SphereCast(grappleOrigin.position, 2f, grappleOrigin.forward, out hit, maxGrappleDistance, grappleLayer))
        {
            grapplePoint = hit.point; // ワイヤーの引っ掛かり地点を取得
            StartGrapple(grappleOrigin, ref springJoint, grapplePoint, lineRenderer);
        }
        else
        {
            Debug.Log("グラップルポイントが見つかりませんでした。");
        }
    }

    // グラップルを開始するメソッド
    private void StartGrapple(Transform grappleOrigin, ref SpringJoint springJoint, Vector3 grapplePoint, LineRenderer lineRenderer)
    {
        springJoint = gameObject.AddComponent<SpringJoint>(); // SpringJointを追加
        springJoint.autoConfigureConnectedAnchor = false; // 手動で接続位置を設定
        springJoint.connectedAnchor = grapplePoint; // ワイヤーの引っ掛かり地点を設定

        float distanceToGrapplePoint = Vector3.Distance(grappleOrigin.position, grapplePoint); // ワイヤーの距離を計算

        // SpringJointの設定（スイングの張力などを調整）
        springJoint.maxDistance = distanceToGrapplePoint * 0.8f; // ワイヤーの最大距離
        springJoint.minDistance = distanceToGrapplePoint * 0.25f; // ワイヤーの最小距離
        springJoint.spring = 4.5f;  // スプリングの力（引き寄せる力）
        springJoint.damper = 7f;    // 減衰力（スイングのスムーズさを調整）
        springJoint.massScale = 4.5f; // プレイヤーの質量に対する影響を調整

        lineRenderer.positionCount = 2; // ワイヤーの描画のための頂点を設定
    }

    // グラップルを解除するメソッド
    private void StopGrapple(ref SpringJoint springJoint, LineRenderer lineRenderer)
    {
        if (springJoint != null)
        {
            Destroy(springJoint); // SpringJointを破壊してワイヤーを解除
        }
        lineRenderer.positionCount = 0; // ワイヤー描画を停止
    }

    // ワイヤーの描画処理
    private void DrawRope()
    {
        // 右ワイヤーが存在する場合、描画
        if (rightSpringJoint != null)
        {
            rightLineRenderer.SetPosition(0, rightGrappleOrigin.position); // 始点
            rightLineRenderer.SetPosition(1, rightGrapplePoint);           // 終点
        }

        // 左ワイヤーが存在する場合、描画
        if (leftSpringJoint != null)
        {
            leftLineRenderer.SetPosition(0, leftGrappleOrigin.position);   // 始点
            leftLineRenderer.SetPosition(1, leftGrapplePoint);             // 終点
        }
    }
}


