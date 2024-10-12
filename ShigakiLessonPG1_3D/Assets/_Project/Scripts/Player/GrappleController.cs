using System.Collections;
using UnityEngine;

public class GrappleController : MonoBehaviour
{
    [SerializeField] private Transform grappleOrigin; // ���C���[���˂̋N�_
    [SerializeField] private LineRenderer lineRenderer; // ���C���[�`��p
    [SerializeField] private LayerMask grappleLayer; // �O���b�v���\�ȃ��C���[
    [SerializeField] private float maxGrappleDistance = 30f; // ���C���[���͂��ő勗��

    private Grapple grappleSystem; // Grapple�N���X�̃C���X�^���X

    private void Start()
    {
        // Grapple�N���X�̃C���X�^���X���쐬���A������
        grappleSystem = new Grapple(transform, Camera.main.transform, grappleOrigin, lineRenderer, grappleLayer, maxGrappleDistance);
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
