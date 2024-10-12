using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple
{
    // �O���b�v�����O�����ǂ������m�F����v���p�e�B
    public bool IsGrappling { get; private set; } = false;

    private SpringJoint joint; // �v���C���[�������񂹂邽�߂�SpringJoint
    private LineRenderer lineRenderer; // ���C���[�̕`��Ɏg�p����LineRenderer
    private Transform playerTransform; // �v���C���[��Transform
    private Transform cameraTransform; // �J������Transform
    private Transform grappleOrigin; // ���C���[�����˂����N�_��Transform
    private Vector3 grapplePoint; // ���C���[������������|�C���g
    private LayerMask grappleLayer; // �O���b�v���\�ȃI�u�W�F�N�g�̃��C���[�}�X�N
    private float maxGrappleDistance; // ���C���[���͂��ő勗��

    // �R���X�g���N�^: Grapple�N���X������������
    public Grapple(Transform player, Transform camera, Transform grappleOrigin, LineRenderer lineRenderer, LayerMask grappleLayer, float maxDistance)
    {
        this.playerTransform = player;
        this.cameraTransform = camera;
        this.grappleOrigin = grappleOrigin;
        this.lineRenderer = lineRenderer;
        this.grappleLayer = grappleLayer;
        this.maxGrappleDistance = maxDistance;
    }

    // ���C���[�𔭎˂��悤�Ƃ���֐�
    public void TryStartGrapple()
    {
        RaycastHit hit;
        // �J�����̑O���Ƀ��C�L���X�g���΂��A�O���b�v���\�ȃ|�C���g�����邩���m�F
        if (Physics.Raycast(grappleOrigin.position, cameraTransform.forward, out hit, maxGrappleDistance, grappleLayer))
        {
            StartGrapple(hit.point); // �O���b�v���|�C���g������������A���C���[���J�n
        }
    }

    // ���C���[���˂̏���
    private void StartGrapple(Vector3 hitPoint)
    {
        grapplePoint = hitPoint; // �O���b�v������ꏊ��ݒ�
        IsGrappling = true; // �O���b�v�����O���ɐݒ�

        // �v���C���[��SpringJoint��ǉ����A�����񂹂���悤�ɂ���
        joint = playerTransform.gameObject.AddComponent<SpringJoint>();
        joint.autoConfigureConnectedAnchor = false; // �蓮�Őڑ��ʒu��ݒ�
        joint.connectedAnchor = grapplePoint; // �O���b�v���|�C���g�ɐڑ�

        float distanceToGrapplePoint = Vector3.Distance(grappleOrigin.position, grapplePoint); // �������v�Z

        // SpringJoint�̐ݒ�i�X�C���O�̊��o�𒲐��j
        joint.maxDistance = distanceToGrapplePoint * 0.8f; // ���C���[�̍ő勗��
        joint.minDistance = distanceToGrapplePoint * 0.25f; // ���C���[�̍ŏ�����
        joint.spring = 4.5f;  // �X�v�����O�̗́i�����قǋ�������������j
        joint.damper = 7f;    // �����́i�����قǃX���[�Y�ɓ���j
        joint.massScale = 4.5f; // ���ʂ̃X�P�[�����O�i�v���C���[�̎��ʂɉe���j

        // LineRenderer�̕`�揀��
        lineRenderer.positionCount = 2; // �n�_�ƏI�_��ݒ�
    }

    // ���C���[���������鏈��
    public void StopGrapple()
    {
        if (joint != null)
        {
            GameObject.Destroy(joint); // SpringJoint��j�󂵁A�v���C���[�����
        }
        IsGrappling = false; // �O���b�v���I��
        lineRenderer.positionCount = 0; // ���C���[�̕`����~
    }

    // ���C���[��`�悷�鏈��
    public void DrawRope()
    {
        if (!IsGrappling) return; // �O���b�v�����O���łȂ��ꍇ�͕`�悵�Ȃ�

        // ���C���[�̎n�_�ƏI�_��ݒ�
        lineRenderer.SetPosition(0, grappleOrigin.position); // ���C���[�̔��ˈʒu
        lineRenderer.SetPosition(1, grapplePoint); // ���C���[�̐ڑ��|�C���g
    }
}

