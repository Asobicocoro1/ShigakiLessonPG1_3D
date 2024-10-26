using System.Collections;
using UnityEngine;

public class SpiderSwing : MonoBehaviour
{
    // SpringJoint�̐ݒ�p�p�����[�^�i�C���X�y�N�^�[�Œ����\�j
     private float maxDistanceFactor = 1.2f;   // �ő勗���{��
     private float minDistanceFactor = 0.25f;  // �ŏ������{��
     private float swingSpringForce = 300f;    // �X�C���O�̃X�v�����O��
     private float swingDamper = 7f;           // �X�C���O�̌�����
     private float massScale = 4.5f;           // ���ʃX�P�[��

    // �X�C���O���̉����Ƒ��x����
     private float swingForce = 500f;          // �X�C���O������
     private float maxSwingSpeed = 28f;        // �ő�X�C���O���x

    // ���C���[�A�N�V�����֘A�p�����[�^
    public LayerMask grappleLayer;                             // �O���b�v���\�ȃ��C���[
    public Transform rightGrappleOrigin;                       // �E���C���[�̔��˒n�_
    public Transform leftGrappleOrigin;                        // �����C���[�̔��˒n�_
    public float maxGrappleDistance = 50f;                     // �O���b�v�������̍ő�l
    public LineRenderer rightLineRenderer;                     // �E���C���[��LineRenderer
    public LineRenderer leftLineRenderer;                      // �����C���[��LineRenderer
    public GameObject swingTargetPrefab;                       // �O���b�v���|�C���g�̉����I�u�W�F�N�g

    // �����ϐ��iSpringJoint�ƃX�C���O�p�I�u�W�F�N�g�j
    protected SpringJoint rightSpringJoint;
    protected SpringJoint leftSpringJoint;
    private Rigidbody playerRigidbody;
    private Vector3 rightGrapplePoint;
    private Vector3 leftGrapplePoint;
    private GameObject rightSwingTarget;
    private GameObject leftSwingTarget;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();

        // �O���b�v���^�[�Q�b�g�̏����ݒ�i��ɕ\���j
        rightSwingTarget = Instantiate(swingTargetPrefab);
        leftSwingTarget = Instantiate(swingTargetPrefab);
        rightSwingTarget.SetActive(true);
        leftSwingTarget.SetActive(true);
    }

    private void Update()
    {
        HandleGrappleInput(); // �O���b�v�����͂̏���
        DrawRope();           // ���C���[�`��
        ApplySwingForce();    // �X�C���O�͂̓K�p
    }

    // �O���b�v���̓��͂�����
    private void HandleGrappleInput()
    {
        if (Input.GetButtonDown("Fire5"))
        {
            TryMultipleDirectionGrapple(rightGrappleOrigin, ref rightSpringJoint, ref rightGrapplePoint, rightLineRenderer, rightSwingTarget);
        }

        if (Input.GetButtonDown("Fire6"))
        {
            TryMultipleDirectionGrapple(leftGrappleOrigin, ref leftSpringJoint, ref leftGrapplePoint, leftLineRenderer, leftSwingTarget);
        }

        if (Input.GetButtonUp("Fire5"))
        {
            StopGrapple(ref rightSpringJoint, rightLineRenderer, rightSwingTarget);
        }

        if (Input.GetButtonUp("Fire6"))
        {
            StopGrapple(ref leftSpringJoint, leftLineRenderer, leftSwingTarget);
        }
    }

    // �X�C���O�̉����͂�K�p
    private void ApplySwingForce()
    {
        if (rightSpringJoint != null || leftSpringJoint != null)
        {
            // ���X�e�B�b�N�̓��͂���ɃX�C���O�����̗͂�K�p
            float horizontalInput = GamepadInputManager.Instance.GetAxis("MoveHorizontal");
            float verticalInput = GamepadInputManager.Instance.GetAxis("MoveVertical");

            Vector3 swingDirection = new Vector3(horizontalInput, 0, verticalInput);
            swingDirection = Camera.main.transform.TransformDirection(swingDirection);
            swingDirection.y = 0;

            playerRigidbody.AddForce(swingDirection * swingForce, ForceMode.Acceleration);

            // �ő呬�x�𐧌�
            if (playerRigidbody.velocity.magnitude > maxSwingSpeed)
            {
                playerRigidbody.velocity = playerRigidbody.velocity.normalized * maxSwingSpeed;
            }
        }
    }

    // ���܂��܂ȕ����ɃL���X�g���ăO���b�v���\�ȃ|�C���g��T��
    private bool TryMultipleDirectionGrapple(Transform grappleOrigin, ref SpringJoint springJoint, ref Vector3 grapplePoint, LineRenderer lineRenderer, GameObject swingTarget)
    {
        return TryGrappleInDirection(grappleOrigin, grappleOrigin.up - grappleOrigin.forward, ref springJoint, ref grapplePoint, lineRenderer, swingTarget) ||
               TryGrappleInDirection(grappleOrigin, grappleOrigin.up + Camera.main.transform.forward, ref springJoint, ref grapplePoint, lineRenderer, swingTarget) ||
               TryGrappleInDirection(grappleOrigin, Camera.main.transform.forward - grappleOrigin.forward, ref springJoint, ref grapplePoint, lineRenderer, swingTarget);
    }

    // �w�肵�������ɃO���b�v�������s
    private bool TryGrappleInDirection(Transform grappleOrigin, Vector3 direction, ref SpringJoint springJoint, ref Vector3 grapplePoint, LineRenderer lineRenderer, GameObject swingTarget)
    {
        RaycastHit hit;

        if (Physics.SphereCast(grappleOrigin.position, 2f, direction, out hit, maxGrappleDistance, grappleLayer))
        {
            grapplePoint = hit.point;
            StartGrapple(grappleOrigin, ref springJoint, grapplePoint, lineRenderer, swingTarget);
            return true;
        }
        else
        {
            swingTarget.SetActive(false);
            return false;
        }
    }

    // �X�C���O���J�n
    private void StartGrapple(Transform grappleOrigin, ref SpringJoint springJoint, Vector3 grapplePoint, LineRenderer lineRenderer, GameObject swingTarget)
    {
        springJoint = gameObject.AddComponent<SpringJoint>();
        springJoint.autoConfigureConnectedAnchor = false;
        springJoint.connectedAnchor = grapplePoint;

        float distanceToGrapplePoint = Vector3.Distance(grappleOrigin.position, grapplePoint);
        springJoint.maxDistance = distanceToGrapplePoint * maxDistanceFactor;
        springJoint.minDistance = distanceToGrapplePoint * minDistanceFactor;
        springJoint.spring = swingSpringForce;
        springJoint.damper = swingDamper;
        springJoint.massScale = massScale;

        lineRenderer.positionCount = 2;
        swingTarget.transform.position = grapplePoint;
        swingTarget.SetActive(true);
    }

    // �X�C���O���~
    private void StopGrapple(ref SpringJoint springJoint, LineRenderer lineRenderer, GameObject swingTarget)
    {
        if (springJoint != null)
        {
            Destroy(springJoint);
        }
        lineRenderer.positionCount = 0;
        swingTarget.SetActive(false);
    }

    // ���C���[�̕`��
    private void DrawRope()
    {
        if (rightSpringJoint != null)
        {
            rightLineRenderer.positionCount = 2;
            rightLineRenderer.SetPosition(0, rightGrappleOrigin.position);
            rightLineRenderer.SetPosition(1, rightGrapplePoint);
        }
        else
        {
            rightLineRenderer.positionCount = 0;
        }

        if (leftSpringJoint != null)
        {
            leftLineRenderer.positionCount = 2;
            leftLineRenderer.SetPosition(0, leftGrappleOrigin.position);
            leftLineRenderer.SetPosition(1, leftGrapplePoint);
        }
        else
        {
            leftLineRenderer.positionCount = 0;
        }
    }
}
