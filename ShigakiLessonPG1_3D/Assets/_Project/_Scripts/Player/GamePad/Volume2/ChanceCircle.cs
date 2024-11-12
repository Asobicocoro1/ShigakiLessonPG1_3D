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

        // LockOnManagerのターゲット変更イベントを購読
        lockOnManager.OnTargetChanged += UpdateChanceCircle;
    }

    private void OnDestroy()
    {
        // イベント購読解除
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
