using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Target Settings")]
    [SerializeField] private Transform cameraTarget;
    [SerializeField] private float sensitivity = 100f;
    [SerializeField] private float normalDistance = 5f;
    [SerializeField] private float swingDistance = 8f;
    [SerializeField] private Vector3 cameraOffset = new Vector3(0, 1.5f, 0);
    [SerializeField] private float smoothTime = 0.1f;
    [SerializeField] private Vector3 lookAtOffset = new Vector3(0, 1.0f, 0);

    private Vector3 currentVelocity;
    private float pitch = 0f;
    private float yaw = 0f;
    private float currentDistance;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        currentDistance = normalDistance;
    }

    private void Update()
    {
        HandleCameraInput();
    }

    private void LateUpdate()
    {
        FollowPlayer();
    }

    private void HandleCameraInput()
    {
        float lookX = GamepadInputManager.Instance.GetAxis("LookHorizontal") * sensitivity * Time.deltaTime;
        float lookY = GamepadInputManager.Instance.GetAxis("LookVertical") * sensitivity * Time.deltaTime;

        yaw += lookX;
        pitch -= lookY;
        pitch = Mathf.Clamp(pitch, -35f, 60f);
    }

    private void FollowPlayer()
    {
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 targetPosition = cameraTarget.position + rotation * (cameraOffset - Vector3.forward * currentDistance);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
        transform.LookAt(cameraTarget.position + lookAtOffset);
    }

    public void AdjustForSwing(bool isSwinging)
    {
        currentDistance = isSwinging ? swingDistance : normalDistance;
    }
}
