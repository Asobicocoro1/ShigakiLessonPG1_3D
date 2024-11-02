using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnhancedSwingSystem : MonoBehaviour
{
    [Header("Swing Settings")]
    [SerializeField] private float swingForce = 500f;
    private float maxSwingSpeed = 30f;
    private float springStrength = 300f;
    [SerializeField] private float damping = 5f;

    [Header("Wire Visual Settings")]
    [SerializeField] private Color wireColor = Color.yellow;

    private SpringJoint swingJoint;
    private Rigidbody rb;
    private bool isSwinging;
    private Transform grapplePoint;
    private LockOnManager lockOnManager;
    private LineRenderer lineRenderer;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        lockOnManager = FindObjectOfType<LockOnManager>();

        lineRenderer = gameObject.AddComponent<LineRenderer>();
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

        if (grapplePoint != null && Input.GetButtonDown("Grapple"))
        {
            StartSwing();
        }
        if (Input.GetButtonUp("Grapple"))
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

            swingJoint = gameObject.AddComponent<SpringJoint>();
            swingJoint.autoConfigureConnectedAnchor = false;
            swingJoint.connectedAnchor = grapplePoint.position;
            swingJoint.spring = springStrength;
            swingJoint.damper = damping;

            float distance = Vector3.Distance(transform.position, grapplePoint.position);
            swingJoint.maxDistance = distance * 1.3f;
            swingJoint.minDistance = distance * 0.7f;

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
            lineRenderer.positionCount = 0;
            lineRenderer.enabled = false;
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
