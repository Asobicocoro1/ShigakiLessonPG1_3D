using System.Collections;
using UnityEngine;

public class GrappleController : MonoBehaviour
{
    [SerializeField] private Transform grappleOrigin; // ワイヤー発射の起点
    [SerializeField] private LineRenderer lineRenderer; // ワイヤー描画用
    [SerializeField] private LayerMask grappleLayer; // グラップル可能なレイヤー
    [SerializeField] private float maxGrappleDistance = 30f; // ワイヤーが届く最大距離

    private Grapple grappleSystem; // Grappleクラスのインスタンス

    private void Start()
    {
        // Grappleクラスのインスタンスを作成し、初期化
        grappleSystem = new Grapple(transform, Camera.main.transform, grappleOrigin, lineRenderer, grappleLayer, maxGrappleDistance);
    }

    private void Update()
    {
        // 右クリックでワイヤー発射
        if (Input.GetMouseButtonDown(1)) // 1は右クリック
        {
            grappleSystem.TryStartGrapple();
        }

        // 右クリックを離したらワイヤー解除
        if (Input.GetMouseButtonUp(1))
        {
            grappleSystem.StopGrapple();
        }

        // ワイヤーが発射されている間は描画
        grappleSystem.DrawRope();
    }
}
