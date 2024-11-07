using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnhancedSwingSystem : MonoBehaviour
{
    [Header("Swing Settings")]
    [SerializeField] private float swingForce = 1000; // �X�C���O���̗�
    private float maxSwingSpeed = 30f; // �X�C���O���̍ő呬�x
    private float springStrength = 900f; // �X�v�����O�̋��x
    [SerializeField] private float damping = 7f; // �_���p�[�i�����j�̋���

    [Header("Wire Visual Settings")]
    [SerializeField] private Color wireColor = Color.yellow; // ���C���[�̐F

    private SpringJoint swingJoint; // �X�C���O�p��SpringJoint
    private Rigidbody rb; // �v���C���[��Rigidbody
    private bool isSwinging; // ���݃X�C���O�����ǂ���
    private Transform grapplePoint; // �O���b�v����̃|�C���g
    private LockOnManager lockOnManager; // LockOnManager�i�^�[�Q�b�g�Ǘ��j
    private LineRenderer lineRenderer; // ���C���[�̎��o�\���p

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        lockOnManager = FindObjectOfType<LockOnManager>(); // LockOnManager���������Ď擾

        // LineRenderer�̏����ݒ�
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = wireColor;
        lineRenderer.endColor = wireColor;
        lineRenderer.positionCount = 0; // ������Ԃł̓|�W�V������0�ɐݒ�
        lineRenderer.enabled = false; // ������Ԃł͖�����
    }

    private void Update()
    {
        // LockOnManager���猻�݂̃^�[�Q�b�g���擾
        grapplePoint = lockOnManager.CurrentTarget;

        // GamepadInputManager���g���ă{�^�����͂��擾
        if (grapplePoint != null && GamepadInputManager.Instance.GetButtonDown("Grapple"))
        {
            StartSwing(); // �X�C���O�J�n
        }
        if (GamepadInputManager.Instance.GetButtonUp("Grapple"))
        {
            StopSwing(); // �X�C���O�I��
        }

        if (isSwinging)
        {
            UpdateLineRenderer(); // ���C���[��LineRenderer���X�V
        }
    }

    private void StartSwing()
    {
        if (grapplePoint != null)
        {
            isSwinging = true; // �X�C���O��Ԃ�L����

            // �X�C���O�p��SpringJoint���쐬���Đݒ�
            swingJoint = gameObject.AddComponent<SpringJoint>();
            swingJoint.autoConfigureConnectedAnchor = false;
            swingJoint.connectedAnchor = grapplePoint.position; // �O���b�v�����ݒ�
            swingJoint.spring = springStrength; // �X�v�����O�̋��x��ݒ�
            swingJoint.damper = damping; // �_���p�[�̋��x��ݒ�

            // �v���C���[�ƃO���b�v����̋������v�Z
            float distance = Vector3.Distance(transform.position, grapplePoint.position);
            swingJoint.maxDistance = distance * 0.8f; // �ő勗����ݒ�
            swingJoint.minDistance = distance * 0.2f; // �ŏ�������ݒ�

            // LineRenderer��L����
            lineRenderer.positionCount = 2;
            lineRenderer.enabled = true;
        }
    }

    private void StopSwing()
    {
        if (swingJoint)
        {
            Destroy(swingJoint); // SpringJoint��j��
            isSwinging = false; // �X�C���O��Ԃ𖳌���
            lineRenderer.positionCount = 0; // LineRenderer�����Z�b�g
            lineRenderer.enabled = false; // LineRenderer�𖳌���
        }
    }

    private void UpdateLineRenderer()
    {
        if (lineRenderer.positionCount >= 2)
        {
            // LineRenderer�̗��[�̈ʒu���X�V
            lineRenderer.SetPosition(0, transform.position); // �v���C���[�̈ʒu
            lineRenderer.SetPosition(1, grapplePoint.position); // �O���b�v����̈ʒu
        }
    }
}
