using System.Collections;
using UnityEngine;

public class SpiderSwing : MonoBehaviour
{
    [SerializeField] private float swingForce = 600f;
    [SerializeField] private float maxSwingSpeed = 35f;
    [SerializeField] private float swingSlowDownFactor = 0.95f;
    [SerializeField] private float springStrength = 300f;
    [SerializeField] private float damping = 7f;
    [SerializeField] private float maxDistanceFactor = 1.2f;
    [SerializeField] private float minDistanceFactor = 0.5f;

    public LayerMask grappleLayer;
    public Transform grappleOrigin;
    public LineRenderer lineRenderer;
    public GameObject swingTargetPrefab;

    protected SpringJoint rightSpringJoint;
    protected SpringJoint leftSpringJoint;

    // 新しく追加したプロパティ
    public SpringJoint RightSpringJoint => rightSpringJoint; // プロパティとして右ワイヤーを公開
    public SpringJoint LeftSpringJoint => leftSpringJoint;   // プロパティとして左ワイヤーを公開

    private Rigidbody playerRigidbody;
    private Vector3 grapplePoint;
    private GameObject swingTarget;
    private bool isSwinging = false;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        swingTarget = Instantiate(swingTargetPrefab);
        swingTarget.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire5")) StartSwing();
        if (Input.GetButtonUp("Fire5")) StopSwing();
    }

    private void StartSwing()
    {
        if (TryFindGrapplePoint())
        {
            isSwinging = true;
            rightSpringJoint = gameObject.AddComponent<SpringJoint>();
            rightSpringJoint.autoConfigureConnectedAnchor = false;
            rightSpringJoint.connectedAnchor = grapplePoint;

            float distanceToGrapplePoint = Vector3.Distance(grappleOrigin.position, grapplePoint);
            rightSpringJoint.maxDistance = distanceToGrapplePoint * maxDistanceFactor;
            rightSpringJoint.minDistance = distanceToGrapplePoint * minDistanceFactor;
            rightSpringJoint.spring = springStrength;
            rightSpringJoint.damper = damping;

            lineRenderer.positionCount = 2;
            swingTarget.transform.position = grapplePoint;
            swingTarget.SetActive(true);
        }
    }

    private void StopSwing()
    {
        if (rightSpringJoint != null)
        {
            Destroy(rightSpringJoint);
            isSwinging = false;
        }
        lineRenderer.positionCount = 0;
        swingTarget.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (isSwinging)
        {
            ApplySwingForce();
        }
    }

    private bool TryFindGrapplePoint()
    {
        RaycastHit hit;
        Vector3[] directions = {
            grappleOrigin.up + grappleOrigin.forward,
            grappleOrigin.up - grappleOrigin.right,
            grappleOrigin.up + grappleOrigin.right
        };

        foreach (var dir in directions)
        {
            if (Physics.SphereCast(grappleOrigin.position, 1f, dir, out hit, 50f, grappleLayer))
            {
                grapplePoint = hit.point;
                return true;
            }
        }

        return false;
    }

    private void ApplySwingForce()
    {
        if (playerRigidbody.velocity.magnitude < maxSwingSpeed)
        {
            Vector3 direction = (grapplePoint - transform.position).normalized;
            playerRigidbody.AddForce(direction * swingForce, ForceMode.Acceleration);
        }
        else
        {
            playerRigidbody.velocity *= swingSlowDownFactor;
        }
    }
}