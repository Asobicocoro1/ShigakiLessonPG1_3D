using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
/*
 public class ClearController : MonoBehaviour
{
    public string nextLevelName = "NextGamePlay"; // 次のステージのシーン名

    // 次のステージをロード
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevelName);
    }

    // メインメニューに戻る
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
---------------------------------------------------------------------------------------
public class EnemyController : MonoBehaviour
{
    public float speed = 3.0f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Vector3 direction = player.position - transform.position;
        direction.Normalize();
        transform.position += direction * speed * Time.deltaTime;
    }
}
---------------------------------------------------------------------------------------
public class GameOverController : MonoBehaviour
{
    // 現在のステージをリトライ
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // メインメニューに戻る
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
---------------------------------------------------------------------------------------
public class GoalController : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.GameClear();
        }
    }
}
---------------------------------------------------------------------------------------
public class LoadingController : MonoBehaviour
{
    public Slider progressBar;

    void Start()
    {
        string nextSceneName = GameManager.instance.nextScene;
        StartCoroutine(LoadAsync(nextSceneName));
    }

    IEnumerator LoadAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            progressBar.value = operation.progress;
            yield return null;
        }
    }
}
---------------------------------------------------------------------------------------
public class TimedSkyboxChanger : MonoBehaviour
{
    public Material[] skyboxes; // スカイボックスの配列
    private int currentSkyboxIndex = 0;
    private float changeInterval = 12.0f;
    private float timer = 0.0f;

    void Start()
    {
        if (skyboxes.Length > 0)
        {
            RenderSettings.skybox = skyboxes[currentSkyboxIndex];
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= changeInterval)
        {
            timer = 0.0f;
            ChangeSkybox();
        }
    }

    void ChangeSkybox()
    {
        currentSkyboxIndex = (currentSkyboxIndex + 1) % skyboxes.Length;
        RenderSettings.skybox = skyboxes[currentSkyboxIndex];
        DynamicGI.UpdateEnvironment(); // グローバルイルミネーションの更新
    }
}
---------------------------------------------------------------------------------------
public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.instance.LoadScene("Loading", "GamePlay");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
---------------------------------------------------------------------------------------
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private float horizontalInput;
    private float verticalInput;

    private Animator animator;
    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 入力の取得
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // キャラクターの移動
        Vector3 move = new Vector3(horizontalInput, 0, verticalInput) * moveSpeed * Time.deltaTime;
        transform.Translate(move, Space.Self);

        // Animatorパラメーターの設定
        float speed = new Vector3(horizontalInput, 0, verticalInput).magnitude;
        animator.SetFloat("speed", speed);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        // ジャンプ関連のパラメーター設定
        animator.SetBool("isJumping", !isGrounded);
        animator.SetBool("isGrounded", isGrounded);
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    // アニメーションイベントハンドラ
    public void OnLand()
    {
        // 着地時の処理をここに記述します
        Debug.Log("着地しました！");
        isGrounded = true;
    }

    // アニメーションイベントハンドラ
    public void OnFootstep()
    {
        // 足音の処理をここに記述します
        Debug.Log("足音が鳴りました！");
        // 足音の効果音を再生する場合など
    }
}
 */
