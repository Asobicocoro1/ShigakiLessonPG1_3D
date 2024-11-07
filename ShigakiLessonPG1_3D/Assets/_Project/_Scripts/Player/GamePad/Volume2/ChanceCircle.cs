using UnityEngine;

public class ChanceCircle : MonoBehaviour
{
    [SerializeField] private GameObject chanceCirclePrefab; // �`�����X�T�[�N���̃v���n�u
    private LockOnManager lockOnManager; // LockOnManager�i�^�[�Q�b�g�Ǘ��N���X�j
    private GameObject activeCircle; // �A�N�e�B�u�ȃ`�����X�T�[�N��

    private void Start()
    {
        // LockOnManager���V�[�������猟�����Ď擾
        lockOnManager = FindObjectOfType<LockOnManager>();

        // �`�����X�T�[�N���̃C���X�^���X���쐬���A��\���ɐݒ�
        activeCircle = Instantiate(chanceCirclePrefab);
        activeCircle.SetActive(false);
    }

    private void Update()
    {
        // ���݂̃^�[�Q�b�g���擾
        Transform target = lockOnManager.CurrentTarget;

        if (target != null)
        {
            // �^�[�Q�b�g�����݂���ꍇ�A�`�����X�T�[�N�����^�[�Q�b�g�̈ʒu�ɐݒ肵�\��
            activeCircle.transform.position = target.position;
            activeCircle.SetActive(true);
        }
        else
        {
            // �^�[�Q�b�g�����݂��Ȃ��ꍇ�A�`�����X�T�[�N�����\��
            activeCircle.SetActive(false);
        }
    }
}
