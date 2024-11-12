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
    private Transform grapplePoint; // ���݂̃O���b�v���|�C���g
    private Transform lastGrapplePoint; // �O��̃O���b�v���|�C���g
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
            lockOnManager.SetSwinging(true); // �X�C���O�J�n��ʒm

            // ���݂̃^�[�Q�b�g���Œ�
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

            // �O��̃O���b�v���|�C���g���L�^
            lastGrapplePoint = grapplePoint;

            // ���̃^�[�Q�b�g���擾
            Transform nextGrapplePoint = lockOnManager.GetNearestGrapplePointExcluding(lastGrapplePoint);
            if (nextGrapplePoint != null && nextGrapplePoint != lastGrapplePoint)
            {
                grapplePoint = nextGrapplePoint;
            }

            lockOnManager.SetSwinging(false); // �X�C���O�I����ʒm

            // ���C�������_���[�̃��Z�b�g
            lineRenderer.positionCount = 0;
            lineRenderer.enabled = false;

            // UI�̍X�V���Ăяo��
            UpdateLockOnUI();
        }
    }

    private void UpdateLockOnUI()
    {
        if (grapplePoint != null)
        {
            // �O���b�v���|�C���g�Ƀ��b�N�I��UI��Ǐ]
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(grapplePoint.position);
            lockOnManager.Indicator.position = screenPosition; // UI�̈ʒu���X�V
            lockOnManager.Indicator.gameObject.SetActive(true); // UI��\��
        }
        else
        {
            lockOnManager.Indicator.gameObject.SetActive(false); // �^�[�Q�b�g���Ȃ��ꍇ�͔�\��
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
