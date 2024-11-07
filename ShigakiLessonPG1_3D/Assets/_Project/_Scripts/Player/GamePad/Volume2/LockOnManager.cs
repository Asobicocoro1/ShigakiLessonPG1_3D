using UnityEngine;

public class LockOnManager : MonoBehaviour
{
    [SerializeField] private LayerMask grappleLayer;    // グラップルポイントのレイヤー
    [SerializeField] private float lockOnRange = 50f;   // ロックオン可能な範囲
    [SerializeField] private Transform indicator;       // ロックオン状態を示すインジケーター（UIなど）

    private Transform currentTarget; // 現在のロックオン対象

    public Transform CurrentTarget => currentTarget;    // 現在のロックオン対象を外部から取得できるように公開

    private void Update()
    {
        GetNearestGrapplePoint(); // 最も近いグラップルポイントを取得
        UpdateIndicatorPosition(); // インジケーターの位置を更新
    }

    public Transform GetNearestGrapplePoint()
    {
        // 現在の位置を中心にロックオン範囲内のグラップルポイントを取得
        Collider[] grapplePoints = Physics.OverlapSphere(transform.position, lockOnRange, grappleLayer);
        float closestDistance = Mathf.Infinity; // 初期値は無限大
        Transform nearest = null; // 最も近いポイントを格納するための変数

        foreach (var point in grapplePoints)
        {
            // 各ポイントとの距離を計算
            float distance = Vector3.Distance(transform.position, point.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance; // より近い距離を更新
                nearest = point.transform; // 最も近いポイントを更新
            }
        }

        currentTarget = nearest; // 最も近いターゲットを設定
        if (currentTarget != null)
        {
            Debug.Log($"Locked onto target: {currentTarget.name}"); // デバッグログにロックオン対象の名前を出力
        }
        return currentTarget; // ロックオン対象を返す
    }

    private void UpdateIndicatorPosition()
    {
        if (currentTarget != null)
        {
            if (indicator != null)
            {
                // インジケーターの位置をターゲットの少し上に設定
                indicator.position = currentTarget.position + Vector3.up * 1.5f;
                // インジケーターのサイズを設定（必要に応じて調整）
                indicator.localScale = Vector3.one * 0.03f;

                indicator.gameObject.SetActive(true); // インジケーターを表示
                Debug.Log($"Indicator activated at position: {indicator.position}"); // デバッグログで位置を確認
            }
            else
            {
                Debug.LogWarning("Indicator is not assigned in LockOnManager"); // インジケーターが未設定の場合の警告
            }

            // カメラの方向にインジケーターを向ける
            Camera mainCamera = Camera.main;
            if (mainCamera != null)
            {
                indicator.LookAt(mainCamera.transform); // カメラを注視するように回転
            }
        }
        else
        {
            if (indicator != null)
            {
                indicator.gameObject.SetActive(false); // ターゲットがない場合はインジケーターを非表示
                Debug.Log("Indicator deactivated"); // デバッグログで非表示を確認
            }
        }
    }
}
