using UnityEngine;

public class ChanceCircle : MonoBehaviour
{
    [SerializeField] private GameObject chanceCirclePrefab; // チャンスサークルのプレハブ
    private LockOnManager lockOnManager; // LockOnManager（ターゲット管理クラス）
    private GameObject activeCircle; // アクティブなチャンスサークル

    private void Start()
    {
        // LockOnManagerをシーン内から検索して取得
        lockOnManager = FindObjectOfType<LockOnManager>();

        // チャンスサークルのインスタンスを作成し、非表示に設定
        activeCircle = Instantiate(chanceCirclePrefab);
        activeCircle.SetActive(false);
    }

    private void Update()
    {
        // 現在のターゲットを取得
        Transform target = lockOnManager.CurrentTarget;

        if (target != null)
        {
            // ターゲットが存在する場合、チャンスサークルをターゲットの位置に設定し表示
            activeCircle.transform.position = target.position;
            activeCircle.SetActive(true);
        }
        else
        {
            // ターゲットが存在しない場合、チャンスサークルを非表示
            activeCircle.SetActive(false);
        }
    }
}
