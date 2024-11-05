using UnityEngine;

public class LockOnManager : MonoBehaviour
{
    [SerializeField] private LayerMask grappleLayer;    // グラップルポイントのレイヤー
    [SerializeField] private float lockOnRange = 50f;   // ロックオン範囲
    [SerializeField] private Transform indicator;       // ロックオン状態を示すインジケーター

    private Transform currentTarget;

    public Transform CurrentTarget => currentTarget;    // 現在のロックオン対象を公開

    private void Update()
    {
        GetNearestGrapplePoint();
        UpdateIndicatorPosition();
    }

    public Transform GetNearestGrapplePoint()
    {
        Collider[] grapplePoints = Physics.OverlapSphere(transform.position, lockOnRange, grappleLayer);
        float closestDistance = Mathf.Infinity;
        Transform nearest = null;

        foreach (var point in grapplePoints)
        {
            float distance = Vector3.Distance(transform.position, point.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                nearest = point.transform;
            }
        }

        currentTarget = nearest;
        if (currentTarget != null)
        {
            Debug.Log($"Locked onto target: {currentTarget.name}");
        }
        return currentTarget;
    }

    private void UpdateIndicatorPosition()
    {
        if (currentTarget != null)
        {
            indicator.position = currentTarget.position + Vector3.up * 2;
            indicator.gameObject.SetActive(true);

            // インジケーターがカメラの方向を向くように調整
            Camera mainCamera = Camera.main;
            if (mainCamera != null)
            {
                indicator.LookAt(mainCamera.transform);
            }
        }
        else
        {
            indicator.gameObject.SetActive(false);
        }
    }
}
