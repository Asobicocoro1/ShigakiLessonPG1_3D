using UnityEngine;

public class GamePadCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraTarget;
    [SerializeField] private float rightStickSensitivity = 100f;
    [SerializeField] private float normalDistance = 5.0f;
    [SerializeField] private float swingDistance = 8.0f;
    [SerializeField] private Vector3 cameraOffset = new Vector3(0, 1.5f, 0);
    [SerializeField] private float smoothTime = 0.1f;
    [SerializeField] private float zoomSpeed = 2f;
    [SerializeField] private Vector3 lookAtOffset = new Vector3(0, 1.0f, 0);

    private Vector3 currentVelocity;
    private float pitch = 0f;
    private float yaw = 0f;
    private float currentDistance;
    private SpiderSwing swingController;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        currentDistance = normalDistance;
        swingController = FindObjectOfType<SpiderSwing>();
    }

    private void Update()
    {
        HandleCameraInput();
        HandleZoom();
    }

    private void LateUpdate()
    {
        FollowPlayer();
    }

    private void HandleCameraInput()
    {
        float lookX = GamepadInputManager.Instance.GetAxis("LookHorizontal") * rightStickSensitivity * Time.deltaTime;
        float lookY = GamepadInputManager.Instance.GetAxis("LookVertical") * rightStickSensitivity * Time.deltaTime;

        yaw += lookX;
        pitch -= lookY;
        pitch = Mathf.Clamp(pitch, -35f, 60f);
    }

    private void HandleZoom()
    {
        if (swingController.RightSpringJoint != null || swingController.LeftSpringJoint != null)
        {
            currentDistance = Mathf.Lerp(currentDistance, swingDistance, Time.deltaTime * zoomSpeed);
        }
        else
        {
            currentDistance = Mathf.Lerp(currentDistance, normalDistance, Time.deltaTime * zoomSpeed);
        }
    }

    private void FollowPlayer()
    {
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 targetPosition = cameraTarget.position + rotation * (cameraOffset - Vector3.forward * currentDistance);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
        Vector3 lookAtPosition = cameraTarget.position + lookAtOffset;
        transform.LookAt(lookAtPosition);
    }
}