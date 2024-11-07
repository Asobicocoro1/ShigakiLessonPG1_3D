using UnityEngine;

public class LockOnManager : MonoBehaviour
{
    [SerializeField] private LayerMask grappleLayer;    // �O���b�v���|�C���g�̃��C���[
    [SerializeField] private float lockOnRange = 50f;   // ���b�N�I���\�Ȕ͈�
    [SerializeField] private Transform indicator;       // ���b�N�I����Ԃ������C���W�P�[�^�[�iUI�Ȃǁj

    private Transform currentTarget; // ���݂̃��b�N�I���Ώ�

    public Transform CurrentTarget => currentTarget;    // ���݂̃��b�N�I���Ώۂ��O������擾�ł���悤�Ɍ��J

    private void Update()
    {
        GetNearestGrapplePoint(); // �ł��߂��O���b�v���|�C���g���擾
        UpdateIndicatorPosition(); // �C���W�P�[�^�[�̈ʒu���X�V
    }

    public Transform GetNearestGrapplePoint()
    {
        // ���݂̈ʒu�𒆐S�Ƀ��b�N�I���͈͓��̃O���b�v���|�C���g���擾
        Collider[] grapplePoints = Physics.OverlapSphere(transform.position, lockOnRange, grappleLayer);
        float closestDistance = Mathf.Infinity; // �����l�͖�����
        Transform nearest = null; // �ł��߂��|�C���g���i�[���邽�߂̕ϐ�

        foreach (var point in grapplePoints)
        {
            // �e�|�C���g�Ƃ̋������v�Z
            float distance = Vector3.Distance(transform.position, point.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance; // ���߂��������X�V
                nearest = point.transform; // �ł��߂��|�C���g���X�V
            }
        }

        currentTarget = nearest; // �ł��߂��^�[�Q�b�g��ݒ�
        if (currentTarget != null)
        {
            Debug.Log($"Locked onto target: {currentTarget.name}"); // �f�o�b�O���O�Ƀ��b�N�I���Ώۂ̖��O���o��
        }
        return currentTarget; // ���b�N�I���Ώۂ�Ԃ�
    }

    private void UpdateIndicatorPosition()
    {
        if (currentTarget != null)
        {
            if (indicator != null)
            {
                // �C���W�P�[�^�[�̈ʒu���^�[�Q�b�g�̏�����ɐݒ�
                indicator.position = currentTarget.position + Vector3.up * 1.5f;
                // �C���W�P�[�^�[�̃T�C�Y��ݒ�i�K�v�ɉ����Ē����j
                indicator.localScale = Vector3.one * 0.03f;

                indicator.gameObject.SetActive(true); // �C���W�P�[�^�[��\��
                Debug.Log($"Indicator activated at position: {indicator.position}"); // �f�o�b�O���O�ňʒu���m�F
            }
            else
            {
                Debug.LogWarning("Indicator is not assigned in LockOnManager"); // �C���W�P�[�^�[�����ݒ�̏ꍇ�̌x��
            }

            // �J�����̕����ɃC���W�P�[�^�[��������
            Camera mainCamera = Camera.main;
            if (mainCamera != null)
            {
                indicator.LookAt(mainCamera.transform); // �J�����𒍎�����悤�ɉ�]
            }
        }
        else
        {
            if (indicator != null)
            {
                indicator.gameObject.SetActive(false); // �^�[�Q�b�g���Ȃ��ꍇ�̓C���W�P�[�^�[���\��
                Debug.Log("Indicator deactivated"); // �f�o�b�O���O�Ŕ�\�����m�F
            }
        }
    }
}
