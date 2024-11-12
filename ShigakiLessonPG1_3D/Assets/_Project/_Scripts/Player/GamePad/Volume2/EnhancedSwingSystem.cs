using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnhancedSwingSystem : MonoBehaviour
{
    [Header("Swing Settings")]
    [SerializeField] private float swingForce = 10;
    private float maxSwingSpeed = 5f;
    private float springStrength = 30;
    [SerializeField] private float damping = 5f;

    [Header("Wire Visual Settings")]
    [SerializeField] private Color wireColor = Color.yellow;

    private SpringJoint swingJoint;
    private Rigidbody rb;
    private bool isSwinging;
    private Transform grapplePoint; // 現在のグラップルポイント
    private Transform lastGrapplePoint; // 前回のグラップルポイント
    private LockOnManager lockOnManager;
    private LineRenderer lineRenderer;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        lockOnManager = FindObjectOfType<LockOnManager>();

        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = wireColor;
        lineRenderer.endColor = wireColor;
        lineRenderer.positionCount = 0;
        lineRenderer.enabled = false;
    }

    private void Update()
    {
        grapplePoint = lockOnManager.CurrentTarget;

        if (GamepadInputManager.Instance.GetButtonDown("Grapple"))
        {
            if (grapplePoint != null)
            {
                StartSwing();
            }
        }

        if (GamepadInputManager.Instance.GetButtonUp("Grapple"))
        {
            StopSwing();
        }

        if (isSwinging)
        {
            UpdateLineRenderer();
        }
    }

    private void StartSwing()
    {
        if (grapplePoint != null)
        {
            isSwinging = true;
            lockOnManager.SetSwinging(true); // スイング開始を通知

            // 現在のターゲットを固定
            swingJoint = gameObject.AddComponent<SpringJoint>();
            swingJoint.autoConfigureConnectedAnchor = false;
            swingJoint.connectedAnchor = grapplePoint.position;
            swingJoint.spring = springStrength;
            swingJoint.damper = damping;

            float distance = Vector3.Distance(transform.position, grapplePoint.position);
            swingJoint.maxDistance = distance * 0.5f;
            swingJoint.minDistance = distance * 0.1f;

            lineRenderer.positionCount = 2;
            lineRenderer.enabled = true;
        }
    }

    private void StopSwing()
    {
        if (swingJoint)
        {
            Destroy(swingJoint);
            isSwinging = false;

            // 前回のグラップルポイントを記録
            lastGrapplePoint = grapplePoint;

            // 次のターゲットを取得
            Transform nextGrapplePoint = lockOnManager.GetNearestGrapplePointExcluding(lastGrapplePoint);
            if (nextGrapplePoint != null && nextGrapplePoint != lastGrapplePoint)
            {
                grapplePoint = nextGrapplePoint;
            }

            lockOnManager.SetSwinging(false); // スイング終了を通知

            // ラインレンダラーのリセット
            lineRenderer.positionCount = 0;
            lineRenderer.enabled = false;

            // UIの更新を呼び出す
            UpdateLockOnUI();
        }
    }

    private void UpdateLockOnUI()
    {
        if (grapplePoint != null)
        {
            // グラップルポイントにロックオンUIを追従
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(grapplePoint.position);
            lockOnManager.Indicator.position = screenPosition; // UIの位置を更新
            lockOnManager.Indicator.gameObject.SetActive(true); // UIを表示
        }
        else
        {
            lockOnManager.Indicator.gameObject.SetActive(false); // ターゲットがない場合は非表示
        }
    }


    private void UpdateLineRenderer()
    {
        if (lineRenderer.positionCount >= 2)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, grapplePoint.position);
        }
    }
}
