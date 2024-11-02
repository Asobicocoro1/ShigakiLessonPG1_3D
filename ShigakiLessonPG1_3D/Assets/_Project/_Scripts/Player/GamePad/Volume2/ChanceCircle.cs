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
    }

    private void Update()
    {
        Transform target = lockOnManager.CurrentTarget;

        if (target != null)
        {
            activeCircle.transform.position = target.position;
            activeCircle.SetActive(true);
        }
        else
        {
            activeCircle.SetActive(false);
        }
    }
}
