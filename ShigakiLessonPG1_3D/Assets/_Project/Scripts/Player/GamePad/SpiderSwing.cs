using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderSwing : MonoBehaviour
{
    // �E���C���[�p��SpringJoint�iprotected�ɕύX�j
    protected SpringJoint rightSpringJoint;
    // �����C���[�p��SpringJoint�iprotected�ɕύX�j
    protected SpringJoint leftSpringJoint;

    public LayerMask grappleLayer; // �O���b�v���\�ȃI�u�W�F�N�g�̃��C���[
    public Transform rightGrappleOrigin; // �E���C���[�����˂����N�_
    public Transform leftGrappleOrigin;  // �����C���[�����˂����N�_
    public float maxGrappleDistance = 50f; // ���C���[���͂��ő勗��
    public LineRenderer rightLineRenderer; // �E���C���[�`��p
    public LineRenderer leftLineRenderer;  // �����C���[�`��p
    public float swingForce = 10f; // �X�C���O�̗�

    private Rigidbody playerRigidbody; // �v���C���[��Rigidbody
    private Vector3 rightGrapplePoint; // �E���C���[�̈����|����n�_
    private Vector3 leftGrapplePoint;  // �����C���[�̈����|����n�_

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>(); // �v���C���[��Rigidbody���擾
    }

    private void Update()
    {
        HandleGrappleInput(); // �O���b�v���̓��͏���
        DrawRope(); // ���C���[�̕`�揈��
    }

    // �O���b�v���̓��͏���
    private void HandleGrappleInput()
    {
        // GamepadInputManager���g���ĉE���C���[�p�{�^�����`�F�b�N
        if (GamepadInputManager.Instance.GetButtonDown("GrappleRight"))
        {
            TryGrapple(rightGrappleOrigin, ref rightSpringJoint, ref rightGrapplePoint, rightLineRenderer);
        }

        // GamepadInputManager���g���č����C���[�p�{�^�����`�F�b�N
        if (GamepadInputManager.Instance.GetButtonDown("GrappleLeft"))
        {
            TryGrapple(leftGrappleOrigin, ref leftSpringJoint, ref leftGrapplePoint, leftLineRenderer);
        }

        // ���C���[����
        if (GamepadInputManager.Instance.GetButtonUp("GrappleRight"))
        {
            StopGrapple(ref rightSpringJoint, rightLineRenderer);
        }
        if (GamepadInputManager.Instance.GetButtonUp("GrappleLeft"))
        {
            StopGrapple(ref leftSpringJoint, leftLineRenderer);
        }
    }

    // ���C���[���ˏ���
    private void TryGrapple(Transform grappleOrigin, ref SpringJoint springJoint, ref Vector3 grapplePoint, LineRenderer lineRenderer)
    {
        RaycastHit hit;
        // SphereCast���g���ċ߂��̃I�u�W�F�N�g�𔻒�
        if (Physics.SphereCast(grappleOrigin.position, 2f, grappleOrigin.forward, out hit, maxGrappleDistance, grappleLayer))
        {
            grapplePoint = hit.point; // ���C���[�̈����|����n�_���擾
            StartGrapple(grappleOrigin, ref springJoint, grapplePoint, lineRenderer);
        }
        else
        {
            Debug.Log("�O���b�v���|�C���g��������܂���ł����B");
        }
    }

    // �O���b�v�����J�n���郁�\�b�h
    private void StartGrapple(Transform grappleOrigin, ref SpringJoint springJoint, Vector3 grapplePoint, LineRenderer lineRenderer)
    {
        springJoint = gameObject.AddComponent<SpringJoint>(); // SpringJoint��ǉ�
        springJoint.autoConfigureConnectedAnchor = false; // �蓮�Őڑ��ʒu��ݒ�
        springJoint.connectedAnchor = grapplePoint; // ���C���[�̈����|����n�_��ݒ�

        float distanceToGrapplePoint = Vector3.Distance(grappleOrigin.position, grapplePoint); // ���C���[�̋������v�Z

        // SpringJoint�̐ݒ�i�X�C���O�̒��͂Ȃǂ𒲐��j
        springJoint.maxDistance = distanceToGrapplePoint * 0.8f; // ���C���[�̍ő勗��
        springJoint.minDistance = distanceToGrapplePoint * 0.25f; // ���C���[�̍ŏ�����
        springJoint.spring = 4.5f;  // �X�v�����O�̗́i�����񂹂�́j
        springJoint.damper = 7f;    // �����́i�X�C���O�̃X���[�Y���𒲐��j
        springJoint.massScale = 4.5f; // �v���C���[�̎��ʂɑ΂���e���𒲐�

        lineRenderer.positionCount = 2; // ���C���[�̕`��̂��߂̒��_��ݒ�
    }

    // �O���b�v�����������郁�\�b�h
    private void StopGrapple(ref SpringJoint springJoint, LineRenderer lineRenderer)
    {
        if (springJoint != null)
        {
            Destroy(springJoint); // SpringJoint��j�󂵂ă��C���[������
        }
        lineRenderer.positionCount = 0; // ���C���[�`����~
    }

    // ���C���[�̕`�揈��
    private void DrawRope()
    {
        // �E���C���[�����݂���ꍇ�A�`��
        if (rightSpringJoint != null)
        {
            rightLineRenderer.SetPosition(0, rightGrappleOrigin.position); // �n�_
            rightLineRenderer.SetPosition(1, rightGrapplePoint);           // �I�_
        }

        // �����C���[�����݂���ꍇ�A�`��
        if (leftSpringJoint != null)
        {
            leftLineRenderer.SetPosition(0, leftGrappleOrigin.position);   // �n�_
            leftLineRenderer.SetPosition(1, leftGrapplePoint);             // �I�_
        }
    }
}


