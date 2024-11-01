using UnityEngine;

public class SpiderSwing : MonoBehaviour
{
    [SerializeField] private float swingForce = 600f; // �X�C���O���̐��i�́B�X�C���O���̈ړ����x������
    [SerializeField] private float maxSwingSpeed = 35f; // �X�C���O���̍ő呬�x
    [SerializeField] private float swingSlowDownFactor = 0.95f; // �X�C���O���̑��x������
    [SerializeField] private float springStrength = 300f; // �X�v�����O�̗́i�������苭�x�j
    [SerializeField] private float damping = 7f; // �X�v�����O�̌����́B��������́u�h��v�̗}���Ɏg�p
    [SerializeField] private float maxDistanceFactor = 1.2f; // �X�C���O�J�n���̍ő勗���{��
    [SerializeField] private float minDistanceFactor = 0.5f; // �X�C���O�J�n���̍ŏ������{��

    public LayerMask grappleLayer; // ���C���[�������|������I�u�W�F�N�g�̃��C���[���w��
    public Transform grappleOrigin; // ���C���[���ˈʒu�i�v���C���[����̑��Έʒu�Ȃǂ��w�肷��j
    public LineRenderer lineRenderer; // ���C���[��`�悷�邽�߂�LineRenderer�R���|�[�l���g
    public GameObject swingTargetPrefab; // �^�[�Q�b�g�|�C���g�̃r�W���A���I�u�W�F�N�g�i�ڕW�n�_�������j

    protected SpringJoint rightSpringJoint; // �E���C���[�p��SpringJoint�i�����I�ȃ��C���[�̋�����ݒ�j
    protected SpringJoint leftSpringJoint; // �����C���[�p��SpringJoint�i���E�̃��C���[�g�p���z�肳��Ă���ꍇ�j

    public bool IsSwinging { get; private set; } = false; // �X�C���O�����ǂ����̏�Ԃ��O������Q�Ƃł���v���p�e�B

    private Rigidbody playerRigidbody; // �v���C���[��Rigidbody�R���|�[�l���g�B�����I�Ȉړ���͂̓K�p�Ɏg�p
    private Vector3 grapplePoint; // ���C���[���ڑ������ڕW�n�_�̍��W
    private GameObject swingTarget; // �ڕW�n�_�������I�u�W�F�N�g�i�X�C���O���ɖڕW�n�_�������j

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>(); // �v���C���[��Rigidbody���擾���A�X�C���O���̗͂̉��Z�Ɏg�p
        swingTarget = Instantiate(swingTargetPrefab); // �ڕW�n�_�̉����I�u�W�F�N�g�𐶐�
        swingTarget.SetActive(false); // ������Ԃł͔�\��
    }

    private void Update()
    {
        // Fire5�{�^���������ꂽ�ꍇ�ɃX�C���O���J�n���A�����ꂽ�ꍇ�ɏI��
        if (Input.GetButtonDown("Fire5")) StartSwing();
        if (Input.GetButtonUp("Fire5")) StopSwing();
    }

    // �X�C���O���J�n���郁�\�b�h
    private void StartSwing()
    {
        // �O���b�v���|�C���g�����������ꍇ�ɃX�C���O�J�n
        if (TryFindGrapplePoint())
        {
            IsSwinging = true; // �X�C���O��Ԃ��J�n
            rightSpringJoint = gameObject.AddComponent<SpringJoint>(); // �E���C���[�p��SpringJoint�R���|�[�l���g��ǉ�
            rightSpringJoint.autoConfigureConnectedAnchor = false; // �����ݒ���I�t�ɂ��ăJ�X�^���ݒ���g�p
            rightSpringJoint.connectedAnchor = grapplePoint; // �O���b�v���|�C���g��SpringJoint�̐ڑ���Ƃ��Đݒ�

            // ���C���[�̍ő�/�ŏ�������ݒ�
            float distanceToGrapplePoint = Vector3.Distance(grappleOrigin.position, grapplePoint);
            rightSpringJoint.maxDistance = distanceToGrapplePoint * maxDistanceFactor; // �ڑ����̍ő勗����{���Œ���
            rightSpringJoint.minDistance = distanceToGrapplePoint * minDistanceFactor; // �ڑ����̍ŏ�������{���Œ���
            rightSpringJoint.spring = springStrength; // �X�v�����O�̋��x�i��������́j��ݒ�
            rightSpringJoint.damper = damping; // �X�v�����O�̌����́i�h��̗}���j��ݒ�

            lineRenderer.positionCount = 2; // ���C���[�̕`���ݒ�
            swingTarget.transform.position = grapplePoint; // �ڕW�n�_�̈ʒu���X�V
            swingTarget.SetActive(true); // �ڕW�n�_�̉����I�u�W�F�N�g��\��
        }
    }

    // �X�C���O���~���郁�\�b�h
    private void StopSwing()
    {
        if (rightSpringJoint != null)
        {
            Destroy(rightSpringJoint); // SpringJoint���폜���ăX�C���O���I��
            IsSwinging = false; // �X�C���O��Ԃ����Z�b�g
        }
        lineRenderer.positionCount = 0; // ���C���[�̕`����N���A
        swingTarget.SetActive(false); // �ڕW�n�_�̉����I�u�W�F�N�g���\����
    }

    private void FixedUpdate()
    {
        if (IsSwinging)
        {
            ApplySwingForce(); // �X�C���O����SwingForce��K�p���Ĉړ��𐧌�
        }
    }

    // �X�C���O�̖ڕW�n�_�������郁�\�b�h
    private bool TryFindGrapplePoint()
    {
        RaycastHit hit;
        Vector3[] directions = {
            grappleOrigin.up + grappleOrigin.forward, // �O���΂ߏ����
            grappleOrigin.up - grappleOrigin.right,   // �E�΂ߏ����
            grappleOrigin.up + grappleOrigin.right    // ���΂ߏ����
        };

        // �e�����Ɍ�������SphereCast�����s���A��Q�������邩�`�F�b�N
        foreach (var dir in directions)
        {
            if (Physics.SphereCast(grappleOrigin.position, 1f, dir, out hit, 50f, grappleLayer))
            {
                grapplePoint = hit.point; // �q�b�g�����n�_���O���b�v���|�C���g�Ƃ��Đݒ�
                return true; // �O���b�v���|�C���g�̎擾�ɐ���
            }
        }

        return false; // �O���b�v���|�C���g��������Ȃ������ꍇ
    }

    // �X�C���O���̈ړ��͂�K�p���郁�\�b�h
    private void ApplySwingForce()
    {
        // �v���C���[���ݒ肵���ő呬�x�����̏ꍇ�ɃX�C���O�͂�������
        if (playerRigidbody.velocity.magnitude < maxSwingSpeed)
        {
            Vector3 direction = (grapplePoint - transform.position).normalized; // �O���b�v���|�C���g�Ɍ������������v�Z
            playerRigidbody.AddForce(direction * swingForce, ForceMode.Acceleration); // �v�Z���������ɃX�C���O�͂�K�p
        }
        else
        {
            // �ő呬�x�𒴂����ꍇ�A���x������
            playerRigidbody.velocity *= swingSlowDownFactor;
        }
    }
}
