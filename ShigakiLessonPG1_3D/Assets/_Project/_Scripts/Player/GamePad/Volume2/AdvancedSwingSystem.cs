using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AdvancedSwingSystem : MonoBehaviour
{
    [Header("Swing Parameters")]
    [SerializeField] private float swingForce = 500f;
    [SerializeField] private float maxSwingSpeed = 30f;
    [SerializeField] private float swingSlowdownFactor = 0.98f;
    [SerializeField] private float springStrength = 300f;
    [SerializeField] private float damping = 5f;

    [Header("Swing Distance Settings")]
    [SerializeField] private float maxGrappleDistance = 50f;
    [SerializeField] private float maxDistanceFactor = 1.3f;
    [SerializeField] private float minDistanceFactor = 0.7f;

    [Header("Combo System")]
    [SerializeField] private int maxComboCount = 3;
    [SerializeField] private float comboTimeLimit = 2f;

    [Header("Debug and Visualization")]
    [SerializeField] private bool debugMode = true;
    [SerializeField] private Color grapplePointColor = Color.red;
    [SerializeField] private Color lineColor = Color.yellow;

    private SpringJoint swingJoint;
    private Rigidbody rb;
    private Vector3 grapplePoint;
    private bool isSwinging;
    private int comboCount;
    private float comboTimer;
    private LineRenderer lineRenderer;
    private GamepadInputManager inputManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        inputManager = GamepadInputManager.Instance;
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;
        lineRenderer.enabled = false;
    }

    private void Update()
    {
        if (inputManager.GetButtonDown("GrappleRight"))
        {
            StartSwing();
        }
        if (inputManager.GetButtonUp("GrappleRight"))
        {
            StopSwing();
        }

        UpdateComboTimer();
        UpdateLineRenderer();
    }

    private void StartSwing()
    {
        if (FindGrapplePoint())
        {
            isSwinging = true;
            comboCount++;
            comboTimer = comboTimeLimit;

            swingJoint = gameObject.AddComponent<SpringJoint>();
            swingJoint.autoConfigureConnectedAnchor = false;
            swingJoint.connectedAnchor = grapplePoint;
            swingJoint.spring = springStrength;
            swingJoint.damper = damping;

            float distance = Vector3.Distance(transform.position, grapplePoint);
            swingJoint.maxDistance = distance * maxDistanceFactor;
            swingJoint.minDistance = distance * minDistanceFactor;

            lineRenderer.enabled = true;
        }
    }

    private void StopSwing()
    {
        if (swingJoint)
        {
            Destroy(swingJoint);
            isSwinging = false;
            lineRenderer.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        if (isSwinging)
        {
            ApplySwingForce();
            CheckMaxSpeed();
        }
    }

    private bool FindGrapplePoint()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxGrappleDistance))
        {
            grapplePoint = hit.point;
            if (debugMode)
            {
                Debug.DrawLine(transform.position, grapplePoint, grapplePointColor, 1.0f);
            }
            return true;
        }
        return false;
    }

    private void ApplySwingForce()
    {
        Vector3 direction = (grapplePoint - transform.position).normalized;
        if (rb.velocity.magnitude < maxSwingSpeed)
        {
            rb.AddForce(direction * swingForce, ForceMode.Acceleration);
        }
        else
        {
            rb.velocity *= swingSlowdownFactor;
        }
    }

    private void CheckMaxSpeed()
    {
        if (rb.velocity.magnitude > maxSwingSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSwingSpeed;
        }
    }

    private void UpdateComboTimer()
    {
        if (comboTimer > 0)
        {
            comboTimer -= Time.deltaTime;
            if (comboTimer <= 0)
            {
                comboCount = 0;
            }
        }
    }

    private void UpdateLineRenderer()
    {
        if (isSwinging)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, grapplePoint);
        }
    }

    private void OnDrawGizmos()
    {
        if (debugMode && isSwinging)
        {
            Gizmos.color = grapplePointColor;
            Gizmos.DrawWireSphere(grapplePoint, 0.5f);
        }
    }

    public int GetComboCount()
    {
        return comboCount;
    }

    public bool IsSwinging()
    {
        return isSwinging;
    }
}
