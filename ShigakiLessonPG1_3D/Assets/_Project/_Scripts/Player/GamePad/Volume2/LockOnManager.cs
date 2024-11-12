using UnityEngine;
using System;
using System.Linq;

public class LockOnManager : MonoBehaviour
{
    [SerializeField] private LayerMask grappleLayer;
    [SerializeField] private float lockOnRange = 50f;
    [SerializeField] private RectTransform indicator;
    [SerializeField] private Vector2 indicatorSize = new Vector2(50f, 50f);
    public RectTransform Indicator => indicator;

    private Transform currentTarget;
    private Transform lastTarget;
    private bool isSwinging = false;

    public Transform CurrentTarget => currentTarget;

    // ターゲット変更イベント
    public event Action<Transform> OnTargetChanged;

    private void Update()
    {
        if (!isSwinging)
        {
            var newTarget = GetNearestGrapplePointExcluding(lastTarget);
            if (newTarget != currentTarget)
            {
                currentTarget = newTarget;
                OnTargetChanged?.Invoke(currentTarget); // ターゲット変更を通知
            }
        }

        UpdateIndicatorPosition();
    }

    public Transform GetNearestGrapplePointExcluding(Transform excludedPoint)
    {
        Collider[] grapplePoints = Physics.OverlapSphere(transform.position, lockOnRange, grappleLayer);

        var sortedPoints = grapplePoints
            .Select(point => new { Transform = point.transform, Distance = Vector3.Distance(transform.position, point.transform.position) })
            .OrderBy(x => x.Distance)
            .ToList();

        foreach (var point in sortedPoints)
        {
            if (point.Transform != excludedPoint)
            {
                return point.Transform;
            }
        }

        return null;
    }

    public void SetSwinging(bool swinging)
    {
        isSwinging = swinging;
        if (!swinging && currentTarget != null)
        {
            lastTarget = currentTarget;
        }
    }

    private void UpdateIndicatorPosition()
    {
        if (indicator != null)
        {
            if (currentTarget != null)
            {
                Vector3 screenPos = Camera.main.WorldToScreenPoint(currentTarget.position + Vector3.up * 1.5f);
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    indicator.parent as RectTransform,
                    screenPos,
                    Camera.main,
                    out Vector2 localPoint
                );

                indicator.localPosition = localPoint;
                indicator.sizeDelta = indicatorSize;
                indicator.gameObject.SetActive(true);
            }
            else
            {
                indicator.gameObject.SetActive(false);
            }
        }
    }
}
