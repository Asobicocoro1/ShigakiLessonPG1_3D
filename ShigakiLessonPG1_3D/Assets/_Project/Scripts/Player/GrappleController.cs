using UnityEngine;

public class GrappleController : MonoBehaviour
{
    [SerializeField] private Transform grappleOrigin; // ���C���[���˂̋N�_
    [SerializeField] private LineRenderer lineRenderer; // ���C���[�`��p
    [SerializeField] private LayerMask grappleLayer; // �O���b�v���\�ȃ��C���[
    [SerializeField] private float maxGrappleDistance = 30f; // ���C���[���͂��ő勗��

    private Grapple grappleSystem; // Grapple�N���X�̃C���X�^���X
    private Rigidbody playerRigidbody; // �v���C���[��Rigidbody

    // IsGrappling�̃v���p�e�B�����J���āACameraFollow ����A�N�Z�X�\�ɂ���
    public bool IsGrappling => grappleSystem != null && grappleSystem.IsGrappling;

    private void Start()
    {
        // �v���C���[�� Rigidbody ���擾
        playerRigidbody = GetComponent<Rigidbody>();

        // Grapple�N���X�̃C���X�^���X���쐬���A������
        grappleSystem = new Grapple(playerRigidbody, Camera.main.transform, grappleOrigin, lineRenderer, grappleLayer, maxGrappleDistance);
    }

    private void Update()
    {
        // �E�N���b�N�Ń��C���[����
        if (Input.GetMouseButtonDown(1)) // 1�͉E�N���b�N
        {
            grappleSystem.TryStartGrapple();
        }

        // �E�N���b�N�𗣂����烏�C���[����
        if (Input.GetMouseButtonUp(1))
        {
            grappleSystem.StopGrapple();
        }

        // ���C���[�����˂���Ă���Ԃ͕`��
        grappleSystem.DrawRope();
    }
}
