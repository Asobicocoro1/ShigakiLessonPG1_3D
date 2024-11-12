using UnityEngine;

public class ChanceCircle : MonoBehaviour
{
    [SerializeField] private GameObject chanceCirclePrefab;
    private LockOnManager lockOnManager;
    private GameObject activeCircle;

    private void Start()
    {
        lockOnManager = FindObjectOfType<LockOnManager>();
        activeCircle = Instantiate(chanceCirclePrefab);
        activeCircle.SetActive(false);

        // LockOnManager�̃^�[�Q�b�g�ύX�C�x���g���w��
        lockOnManager.OnTargetChanged += UpdateChanceCircle;
    }

    private void OnDestroy()
    {
        // �C�x���g�w�ǉ���
        if (lockOnManager != null)
        {
            lockOnManager.OnTargetChanged -= UpdateChanceCircle;
        }
    }

    private void UpdateChanceCircle(Transform newTarget)
    {
        if (newTarget != null)
        {
            activeCircle.transform.position = newTarget.position;
            activeCircle.SetActive(true);
        }
        else
        {
            activeCircle.SetActive(false);
        }
    }
}
