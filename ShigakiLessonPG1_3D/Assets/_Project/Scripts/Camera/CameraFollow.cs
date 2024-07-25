using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // �v���C���[��Transform
    public Vector3 offset; // �J�����̃I�t�Z�b�g
    public float sensitivity = 5.0f; // �J�����̉�]���x
    public float smoothTime = 0.1f; // �J�����̃X���[�Y�Ǐ]����

    private Vector3 currentVelocity;
    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    void Start()
    {
        offset = transform.position - player.position;
        Cursor.lockState = CursorLockMode.Locked; // �}�E�X�J�[�\������ʒ����ɌŒ�
    }

    void LateUpdate()
    {
        // �}�E�X�̓��͂��擾
        rotationX += Input.GetAxis("Mouse X") * sensitivity;
        rotationY -= Input.GetAxis("Mouse Y") * sensitivity;
        rotationY = Mathf.Clamp(rotationY, -35, 60); // ���������̉�]�͈͂𐧌�

        // �v���C���[�𒆐S�ɃJ��������]
        Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);
        Vector3 targetPosition = player.position + rotation * offset;

        // �J�����̈ʒu���X���[�Y�ɒǏ]
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);

        // �J�������v���C���[����Ɍ���悤�ɂ���
        transform.LookAt(player.position);
    }
}
/*
 ���� `CameraFollow` �X�N���v�g�́AUnity �� `UnityEngine` ���O��Ԃɑ����邢�����̃N���X���g�p���Ă��܂��B�ȉ��ɁA�g�p����Ă��� `UnityEngine` �N���X���ڍׂɉ�����܂��B

### �g�p����Ă��� `UnityEngine` �N���X�̏ڍ�

#### 1. `Transform`
- **����**: `Transform` �́A�Q�[���I�u�W�F�N�g�̈ʒu�A��]�A�X�P�[����\���N���X�ł��B���ׂẴQ�[���I�u�W�F�N�g�� `Transform` �R���|�[�l���g�������Ă��܂��B
- **�g�p�ӏ�**: `player` �t�B�[���h�� `transform` �v���p�e�B�Ŏg�p�B
  ```csharp
  public Transform player; // �v���C���[��Transform
  ...
  offset = transform.position - player.position;
  ...
  transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
  ...
  transform.LookAt(player.position);
  ```

#### 2. `Vector3`
- **����**: `Vector3` �́A3�����x�N�g���ix, y, z�j��\���\���̂ł��B�ʒu�A�����A���x�Ȃǂ�\�����邽�߂Ɏg�p����܂��B
- **�g�p�ӏ�**: `offset`, `currentVelocity`, `targetPosition` �̌v�Z�ƁA`transform.position` �̐ݒ�Ɏg�p�B
  ```csharp
  public Vector3 offset; // �J�����̃I�t�Z�b�g
  private Vector3 currentVelocity;
  ...
  Vector3 targetPosition = player.position + rotation * offset;
  ```

#### 3. `Quaternion`
- **����**: `Quaternion` �́A��]��\�����邽�߂̍\���̂ł��B��]���I�C���[�p���]���Ɗp�x�Ƃ��ĕ\�����邱�Ƃ��ł��܂��B
- **�g�p�ӏ�**: �J�����̉�]�̌v�Z�Ɏg�p�B
  ```csharp
  Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);
  ```

#### 4. `Cursor`
- **����**: `Cursor` �N���X�́A�}�E�X�J�[�\���̕\����Ԃ�ʒu�𐧌䂷�邽�߂̐ÓI�N���X�ł��B
- **�g�p�ӏ�**: �}�E�X�J�[�\������ʒ����ɌŒ肷�邽�߂Ɏg�p�B
  ```csharp
  Cursor.lockState = CursorLockMode.Locked;
  ```

#### 5. `CursorLockMode`
- **����**: `CursorLockMode` �́A�J�[�\���̃��b�N��Ԃ��`����񋓌^�ł��B`Locked`�A`Confined`�A`None` ��3�̃��[�h������܂��B
- **�g�p�ӏ�**: �}�E�X�J�[�\������ʒ����ɌŒ肷�邽�߂̐ݒ�Ɏg�p�B
  ```csharp
  Cursor.lockState = CursorLockMode.Locked;
  ```

#### 6. `Mathf`
- **����**: `Mathf` �N���X�́A���w�I�Ȋ֐���萔��񋟂���ÓI�N���X�ł��B�O�p�֐��A��ԁA��Βl�A�N�����v�Ȃǂ̊֐����܂܂�܂��B
- **�g�p�ӏ�**: �J�����̐�����]�͈͂𐧌����邽�߂Ɏg�p�B
  ```csharp
  rotationY = Mathf.Clamp(rotationY, -35, 60);
  ```

### �N���X���Ƃ̏ڍׂȐ����Ǝg�p��

#### `Transform`
- **����**: �Q�[���I�u�W�F�N�g�̈ʒu�A��]�A�X�P�[�����Ǘ��B
- **�g�p��**:
  ```csharp
  public Transform player; // �v���C���[��Transform
  ...
  offset = transform.position - player.position;
  transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
  transform.LookAt(player.position);
  ```

#### `Vector3`
- **����**: 3�����̃x�N�g���f�[�^�������B
- **�g�p��**:
  ```csharp
  public Vector3 offset; // �J�����̃I�t�Z�b�g
  private Vector3 currentVelocity; // �J�����̌��݂̑��x
  ...
  Vector3 targetPosition = player.position + rotation * offset; // �ڕW�ʒu���v�Z
  ```

#### `Quaternion`
- **����**: ��]��\�����邽�߂̍\���́B
- **�g�p��**:
  ```csharp
  Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0); // ��]���v�Z
  ```

#### `Cursor`
- **����**: �}�E�X�J�[�\���̐���B
- **�g�p��**:
  ```csharp
  Cursor.lockState = CursorLockMode.Locked; // �}�E�X�J�[�\�������b�N
  ```

#### `CursorLockMode`
- **����**: �J�[�\���̃��b�N���[�h���`�B
- **�g�p��**:
  ```csharp
  Cursor.lockState = CursorLockMode.Locked; // �J�[�\�����b�N���[�h��ݒ�
  ```

#### `Mathf`
- **����**: ���w�I�Ȋ֐���萔��񋟁B
- **�g�p��**:
  ```csharp
  rotationY = Mathf.Clamp(rotationY, -35, 60); // ��]�͈͂𐧌�
  ```

���̃X�N���v�g�ł́A�����̃N���X���g�p���āA�J�������v���C���[��Ǐ]���A�}�E�X���͂Ɋ�Â��ĉ�]���铮����������Ă��܂��B
 */
