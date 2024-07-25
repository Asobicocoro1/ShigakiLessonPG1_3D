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
    public string nextLevelName = "NextGamePlay"; // ���̃X�e�[�W�̃V�[����

    // ���̃X�e�[�W�����[�h
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevelName);
    }

    // ���C�����j���[�ɖ߂�
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
    // ���݂̃X�e�[�W�����g���C
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // ���C�����j���[�ɖ߂�
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
    public Material[] skyboxes; // �X�J�C�{�b�N�X�̔z��
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
        DynamicGI.UpdateEnvironment(); // �O���[�o���C���~�l�[�V�����̍X�V
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
        // ���͂̎擾
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // �L�����N�^�[�̈ړ�
        Vector3 move = new Vector3(horizontalInput, 0, verticalInput) * moveSpeed * Time.deltaTime;
        transform.Translate(move, Space.Self);

        // Animator�p�����[�^�[�̐ݒ�
        float speed = new Vector3(horizontalInput, 0, verticalInput).magnitude;
        animator.SetFloat("speed", speed);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        // �W�����v�֘A�̃p�����[�^�[�ݒ�
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

    // �A�j���[�V�����C�x���g�n���h��
    public void OnLand()
    {
        // ���n���̏����������ɋL�q���܂�
        Debug.Log("���n���܂����I");
        isGrounded = true;
    }

    // �A�j���[�V�����C�x���g�n���h��
    public void OnFootstep()
    {
        // �����̏����������ɋL�q���܂�
        Debug.Log("��������܂����I");
        // �����̌��ʉ����Đ�����ꍇ�Ȃ�
    }
}
 */
