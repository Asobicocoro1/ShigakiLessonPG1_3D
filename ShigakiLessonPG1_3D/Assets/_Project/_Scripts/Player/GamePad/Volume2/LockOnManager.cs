using UnityEngine;

public class LockOnManager : MonoBehaviour
{
    [SerializeField] private LayerMask grappleLayer;    // �O���b�v���|�C���g�̃��C���[
    [SerializeField] private float lockOnRange = 50f;   // ���b�N�I���͈�
    [SerializeField] private Transform indicator;       // ���b�N�I����Ԃ������C���W�P�[�^�[

    private Transform currentTarget;

    public Transform CurrentTarget => currentTarget;    // ���݂̃��b�N�I���Ώۂ����J

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
            if (indicator != null)
            {
                // �ʒu�ƃX�P�[���̒���
                indicator.position = currentTarget.position + Vector3.up * 1.5f;
                indicator.localScale = Vector3.one * 0.03f; // �K�v�ɉ����Ē���

                indicator.gameObject.SetActive(true);
                Debug.Log($"Indicator activated at position: {indicator.position}");
            }
            else
            {
                Debug.LogWarning("Indicator is not assigned in LockOnManager");
            }

            Camera mainCamera = Camera.main;
            if (mainCamera != null)
            {
                indicator.LookAt(mainCamera.transform);
            }
        }
        else
        {
            if (indicator != null)
            {
                indicator.gameObject.SetActive(false);
                Debug.Log("Indicator deactivated");
            }
        }
    }

}
