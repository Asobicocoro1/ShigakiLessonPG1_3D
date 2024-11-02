using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 ### ������e�̐���

#### 1. `movementDirection.Normalize()`
`Normalize()`�́A�x�N�g���̒����i�}�O�j�`���[�h�j��1�ɂ���֐��ł��B�x�N�g���̕�����ۂ��Ȃ���A���̒�����W�������܂��B

- **�ړI**: �ړ������̃x�N�g���𐳋K�����A���x�v�Z�̍ۂɈ�т����ړ����������邽�߁B
- **��**: 
  ```csharp
  Vector3 movementDirection = new Vector3(3, 0, 4); // �x�N�g�� (3, 0, 4)
  movementDirection.Normalize(); // �x�N�g�� (0.6, 0, 0.8) �ɂȂ�A������ 1 �ɂȂ�
  ```

#### 2. `float actualSpeed = Mathf.Abs(speed) == 1f ? runSpeed : walkSpeed;`
�O�����Z�q���g�p���āA���ۂ̈ړ����x��ݒ肷��s�ł��B

- **�ړI**: `speed`��1�܂���-1�̏ꍇ�A���s���x`runSpeed`��ݒ肵�A����ȊO�̏ꍇ�͕��s���x`walkSpeed`��ݒ肷��B
- **��**:
  ```csharp
  float speed = 1f; // �_�b�V����Ԃ̏ꍇ
  float actualSpeed = Mathf.Abs(speed) == 1f ? runSpeed : walkSpeed; // actualSpeed��runSpeed�ɂȂ�
  
  speed = 0.5f; // ���s��Ԃ̏ꍇ
  actualSpeed = Mathf.Abs(speed) == 1f ? runSpeed : walkSpeed; // actualSpeed��walkSpeed�ɂȂ�
  ```

#### 3. `transform.Translate(movementDirection * actualSpeed * Time.deltaTime, Space.World)`
`transform.Translate`�́A�I�u�W�F�N�g�̈ʒu���w�肳�ꂽ���������ړ�������֐��ł��B

- **�ړI**: ���K�����ꂽ�����x�N�g���Ɏ��ۂ̈ړ����x�ƌo�ߎ��Ԃ��|�����킹�邱�ƂŁA�L�����N�^�[�����̑��x�Ŏw������Ɉړ�������B
- **�p�����[�^�[**:
  - `movementDirection`: ���K�����ꂽ�����x�N�g���B
  - `actualSpeed`: ���ۂ̈ړ����x�i���s���x�܂��͕��s���x�j�B
  - `Time.deltaTime`: �O�t���[������̌o�ߎ��ԁB�t���[�����[�g�Ɉˑ����Ȃ��ړ����������邽�߂Ɏg�p���܂��B
  - `Space.World`: ���E���W�n�ł̈ړ����w�肵�܂��B
- **��**:
  ```csharp
  transform.Translate(Vector3.forward * 5f); // �I�u�W�F�N�g��O����5�P�ʈړ�������
  ```

### �S�̗̂���
```csharp
movementDirection.Normalize(); // �ړ������𐳋K�����ĕ����x�N�g���̒�����1�ɂ���
float actualSpeed = Mathf.Abs(speed) == 1f ? runSpeed : walkSpeed; // ���x��1��-1�̏ꍇ�͑��s���x�A����ȊO�͕��s���x��ݒ�
transform.Translate(movementDirection * actualSpeed * Time.deltaTime, Space.World); // ���K�����ꂽ�ړ������ɑ΂��Ď��ۂ̈ړ����x���|���āA���t���[���ړ����鋗�����v�Z���A�L�����N�^�[���ړ�
```

#### �e�X�e�b�v�̖ړI

1. **`movementDirection.Normalize()`**:
   - �ړ������̃x�N�g���𐳋K�����A������ۂ��Ȃ��璷����1�ɂ��܂��B����ɂ��A�ړ����x����т��Čv�Z����܂��B

2. **`float actualSpeed = Mathf.Abs(speed) == 1f ? runSpeed : walkSpeed;`**:
   - `speed`��1�܂���-1�̏ꍇ�ɑ��s���x��ݒ肵�A����ȊO�̏ꍇ�ɕ��s���x��ݒ肵�܂��B����ɂ��A�L�����N�^�[�̈ړ����x�𓮍�ɉ����Ē������܂��B

3. **`transform.Translate(movementDirection * actualSpeed * Time.deltaTime, Space.World);`**:
   - ���K�����ꂽ�ړ������x�N�g���Ɏ��ۂ̈ړ����x�ƑO�t���[������̌o�ߎ��Ԃ��|���āA�L�����N�^�[�����̕����Ɉ��̑��x�ňړ������܂��B

���̈�A�̑���ɂ��A�L�����N�^�[�͎w�肳�ꂽ�����Ɉ�т������x�ňړ����܂��B����ɂ��A�ړ��̃X���[�Y���Ɛ��m�����ۏ؂���܂��B

Quaternion.LookRotation�̏ڍׂȉ���Ɛ���
��{�I�Ȏg����
Quaternion.LookRotation �́A�w�肳�ꂽ�������������߂̉�]���v�Z���郁�\�b�h�ł��B���̃��\�b�h�́A����̕������������߂̃N�H�[�^�j�I���𐶐����܂��B

csharp
�R�[�h���R�s�[����
Quaternion rotation = Quaternion.LookRotation(Vector3.forward);
���̃R�[�h�́AVector3.forward�i���E���W�n�ł�Z�������j��������]�𐶐����܂��B

�g�p��Əڍ�
�p�����[�^�[:

Vector3 forward: ���������������̃x�N�g���B
Vector3 upwards (�ȗ��\): ��������`����x�N�g���B�f�t�H���g��Vector3.up�B
�߂�l: �w�肳�ꂽ�������������߂̃N�H�[�^�j�I���B

���R
Quaternion.LookRotation ���g�p���邱�ƂŁA�L�����N�^�[��I�u�W�F�N�g���w�肵�������Ɍ����邱�Ƃ��ł��܂��B����ɂ��A�ړ������ɑ΂��ăL�����N�^�[�����R�ɉ�]���܂��B


### �O�����Z�q�̏ڍׂȉ���Ɛ���

�O�����Z�q�i�������Z�q�j�́A�Ȍ��ɏ���������s�����߂̍\���ł��B�ȉ��̌`���Ŏg�p���܂��F

```csharp
���� ? ��1 : ��2;
```

- **����**: �_�����B`true`�܂���`false`��Ԃ��܂��B
- **��1**: ������`true`�̏ꍇ�ɕ]������鎮�B
- **��2**: ������`false`�̏ꍇ�ɕ]������鎮�B

### ��

�ȉ��̃R�[�h�́A�O�����Z�q���g�p���āA�ϐ�`actualSpeed`�ɒl���������ł��B

```csharp
float speed = 1f; // ���̑��x�l
float runSpeed = 6.0f; // ���s���x
float walkSpeed = 2.0f; // ���s���x

float actualSpeed = Mathf.Abs(speed) == 1f ? runSpeed : walkSpeed;
```

���̗�ł́A`speed`�̐�Βl��1�̏ꍇ��`runSpeed`�������A����ȊO�̏ꍇ��`walkSpeed`�������܂��B

### �ڍׂȎg�p��Ɖ��

```csharp
float actualSpeed = Mathf.Abs(speed) == 1f ? runSpeed : walkSpeed;
```

- **`Mathf.Abs(speed)`**: `speed`�̐�Βl���v�Z���܂��B
  - ��: `speed`��-1, 1, -0.5, 0.5�̏ꍇ�A���ꂼ��1, 1, 0.5, 0.5��Ԃ��܂��B
- **`Mathf.Abs(speed) == 1f`**: ��Βl��1�ł��邩�ǂ������`�F�b�N���܂��B
  - ��Βl��1�̏ꍇ�A������`true`�ɂȂ�܂��B����ȊO�̏ꍇ�A������`false`�ɂȂ�܂��B
- **�O�����Z�q�̏�������**: `Mathf.Abs(speed) == 1f`
  - `true`�̏ꍇ: `runSpeed`��`actualSpeed`�ɑ�����܂��B
  - `false`�̏ꍇ: `walkSpeed`��`actualSpeed`�ɑ�����܂��B

### ���R

- **�O�����Z�q���g�p���闝�R**: �R�[�h���Ȍ��ɂ��A�����������s�ŕ\�����邽�߂ł��B`if-else`�����g�p��������ǂ݂₷���Ȃ�܂��B

### �܂Ƃ�

```csharp
float actualSpeed = Mathf.Abs(speed) == 1f ? runSpeed : walkSpeed;
```

1. **�����̕]��**: `Mathf.Abs(speed) == 1f`
   - `speed`�̐�Βl��1�̏ꍇ�A������`true`�ɂȂ�܂��B
   - ����ȊO�̏ꍇ�A������`false`�ɂȂ�܂��B

2. **������`true`�̏ꍇ**: `runSpeed`��`actualSpeed`�ɑ������܂��B
3. **������`false`�̏ꍇ**: `walkSpeed`��`actualSpeed`�ɑ������܂��B

���̃R�[�h�ɂ��A`speed`��1�܂���-1�̏ꍇ�ɑ��s���x���g�p���A����ȊO�̏ꍇ�ɕ��s���x���g�p���邱�Ƃ��ł��܂��B�O�����Z�q���g�����ƂŁA����������Ȍ��ɕ\�����A�R�[�h�̉ǐ������߂Ă��܂��B




�ȉ��ɁA���� `CharacterController` �X�N���v�g���Ŏg�p����Ă��� `UnityEngine` ���O��Ԃɑ�����N���X���ڍׂɉ�����܂��B

### �g�p����Ă��� `UnityEngine` �N���X�̏ڍ�

#### 1. `Animator`
- **����**: `Animator` �N���X�́A�A�j���[�V�����𐧌䂷�邽�߂̃R���|�[�l���g�ł��B�A�j���[�^�[�R���g���[������ăA�j���[�V�������Đ��A��~�A����уp�����[�^�̐ݒ���s���܂��B
- **�g�p�ӏ�**: �A�j���[�V�����̃p�����[�^�ݒ�ƃg���K�[�ݒ�B
  ```csharp
  public Animator animator; // Animator �R���|�[�l���g
  ...
  animator.SetFloat("Speed", speed); // �A�j���[�V�����p�����[�^�[��ݒ�
  animator.SetTrigger("JumpStart"); // �W�����v�A�j���[�V�������J�n
  animator.SetTrigger("JumpEnd"); // �W�����v�A�j���[�V�������I��
  ```

#### 2. `Rigidbody`
- **����**: `Rigidbody` �N���X�́A�����V�~�����[�V�����𐧌䂷�邽�߂̃R���|�[�l���g�ł��B�I�u�W�F�N�g�Ɏ��ʁA�d�́A�����I�ȗ͂�K�p���邱�Ƃ��ł��܂��B
- **�g�p�ӏ�**: �W�����v�̗͂������邽�߁B
  ```csharp
  private Rigidbody rb; // Rigidbody �R���|�[�l���g
  ...
  rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // �W�����v�̗͂�������
  ```

#### 3. `Transform`
- **����**: `Transform` �N���X�́A�Q�[���I�u�W�F�N�g�̈ʒu�A��]�A�X�P�[����\���N���X�ł��B���ׂẴQ�[���I�u�W�F�N�g�� `Transform` �R���|�[�l���g�������Ă��܂��B
- **�g�p�ӏ�**: �J�����̈ʒu�Ɖ�]�̎擾�A�L�����N�^�[�̈ʒu�Ɖ�]�̐ݒ�B
  ```csharp
  private Transform cameraTransform; // ���C���J������Transform
  ...
  cameraTransform = Camera.main.transform; // ���C���J������Transform���擾
  transform.Translate(movementDirection * actualSpeed * Time.deltaTime, Space.World); // �L�����N�^�[�̈ړ�
  transform.rotation = Quaternion.LookRotation(movementDirection, Vector3.up); // �L�����N�^�[�̉�]
  ```

#### 4. `Vector3`
- **����**: `Vector3` �N���X�́A3�����x�N�g���ix, y, z�j��\���\���̂ł��B�ʒu�A�����A���x�Ȃǂ�\�����邽�߂Ɏg�p����܂��B
- **�g�p�ӏ�**: �ړ�������W�����v�̗͂̐ݒ�Ɏg�p�B
  ```csharp
  private Vector3 currentVelocity; // �J�����̌��݂̑��x
  ...
  Vector3 forward = cameraTransform.forward; // �J�����̑O����
  Vector3 right = cameraTransform.right; // �J�����̉E����
  rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // �W�����v�̗͂�������
  ```

#### 5. `Quaternion`
- **����**: `Quaternion` �N���X�́A��]��\�����邽�߂̍\���̂ł��B��]���I�C���[�p���]���Ɗp�x�Ƃ��ĕ\�����邱�Ƃ��ł��܂��B
- **�g�p�ӏ�**: �L�����N�^�[�̉�]�̐ݒ�Ɏg�p�B
  ```csharp
  private Quaternion forwardRotation; // �O�i���̌���
  ...
  forwardRotation = Quaternion.LookRotation(movementDirection, Vector3.up); // �O�i���̌������X�V
  ```

#### 6. `Mathf`
- **����**: `Mathf` �N���X�́A���w�I�Ȋ֐���萔��񋟂���ÓI�N���X�ł��B�O�p�֐��A��ԁA��Βl�A�N�����v�Ȃǂ̊֐����܂܂�܂��B
- **�g�p�ӏ�**: �ړ����x�̐ݒ�Ɏg�p�B
  ```csharp
  float actualSpeed = Mathf.Abs(speed) == 1f ? runSpeed : walkSpeed; // ���ۂ̈ړ����x
  ```

#### 7. `Input`
- **����**: `Input` �N���X�́A���[�U�[���͂��擾���邽�߂̐ÓI�N���X�ł��B�L�[�{�[�h�A�}�E�X�A�^�b�`�A�R���g���[���Ȃǂ̓��͂��擾�ł��܂��B
- **�g�p�ӏ�**: �L�[�{�[�h���͂̎擾�Ɏg�p�B
  ```csharp
  if (Input.GetKey(KeyCode.W)) { ... } // W�L�[�̓��͂��擾
  if (Input.GetKey(KeyCode.S)) { ... } // S�L�[�̓��͂��擾
  if (Input.GetKey(KeyCode.A)) { ... } // A�L�[�̓��͂��擾
  if (Input.GetKey(KeyCode.D)) { ... } // D�L�[�̓��͂��擾
  if (Input.GetKeyDown(KeyCode.Space) && !isJumping) { ... } // �X�y�[�X�L�[�̓��͂��擾
  ```

#### 8. `Collision`
- **����**: `Collision` �N���X�́A�����Փ˃C�x���g�Ɋւ������񋟂��܂��B�Փ˂����I�u�W�F�N�g�A�Փ˓_�A�Փ˂̗͂Ȃǂ̏����擾�ł��܂��B
- **�g�p�ӏ�**: �n�ʂƂ̏Փ˂����m���ăW�����v��Ԃ����Z�b�g���邽�߂Ɏg�p�B
  ```csharp
  void OnCollisionEnter(Collision collision)
  {
      if (collision.gameObject.CompareTag("Ground"))
      {
          isJumping = false;
          animator.SetTrigger("JumpEnd");
      }
  }
  ```

### �N���X���Ƃ̏ڍׂȐ����Ǝg�p��

#### `Animator`
- **����**: �A�j���[�V�����̐���B
- **�g�p��**:
  ```csharp
  public Animator animator; // Animator �R���|�[�l���g
  ...
  animator.SetFloat("Speed", speed); // �A�j���[�V�����p�����[�^�[��ݒ�
  animator.SetTrigger("JumpStart"); // �W�����v�A�j���[�V�������J�n
  animator.SetTrigger("JumpEnd"); // �W�����v�A�j���[�V�������I��
  ```

#### `Rigidbody`
- **����**: �����V�~�����[�V�����̐���B
- **�g�p��**:
  ```csharp
  private Rigidbody rb; // Rigidbody �R���|�[�l���g
  ...
  rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // �W�����v�̗͂�������
  ```

#### `Transform`
- **����**: �Q�[���I�u�W�F�N�g�̈ʒu�A��]�A�X�P�[���̊Ǘ��B
- **�g�p��**:
  ```csharp
  private Transform cameraTransform; // ���C���J������Transform
  ...
  cameraTransform = Camera.main.transform; // ���C���J������Transform���擾
  transform.Translate(movementDirection * actualSpeed * Time.deltaTime, Space.World); // �L�����N�^�[�̈ړ�
  transform.rotation = Quaternion.LookRotation(movementDirection, Vector3.up); // �L�����N�^�[�̉�]
  ```

#### `Vector3`
- **����**: 3�����x�N�g���f�[�^�̊Ǘ��B
- **�g�p��**:
  ```csharp
  private Vector3 currentVelocity; // �J�����̌��݂̑��x
  ...
  Vector3 forward = cameraTransform.forward; // �J�����̑O����
  Vector3 right = cameraTransform.right; // �J�����̉E����
  rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // �W�����v�̗͂�������
  ```

#### `Quaternion`
- **����**: ��]�̊Ǘ��B
- **�g�p��**:
  ```csharp
  private Quaternion forwardRotation; // �O�i���̌���
  ...
  forwardRotation = Quaternion.LookRotation(movementDirection, Vector3.up); // �O�i���̌������X�V
  ```

#### `Mathf`
- **����**: ���w�I�Ȋ֐���萔�̒񋟁B
- **�g�p��**:
  ```csharp
  float actualSpeed = Mathf.Abs(speed) == 1f ? runSpeed : walkSpeed; // ���ۂ̈ړ����x
  ```

#### `Input`
- **����**: ���[�U�[���͂̎擾�B
- **�g�p��**:
  ```csharp
  if (Input.GetKey(KeyCode.W)) { ... } // W�L�[�̓��͂��擾
  if (Input.GetKey(KeyCode.S)) { ... } // S�L�[�̓��͂��擾
  if (Input.GetKey(KeyCode.A)) { ... } // A�L�[�̓��͂��擾
  if (Input.GetKey(KeyCode.D)) { ... } // D�L�[�̓��͂��擾
  if (Input.GetKeyDown(KeyCode.Space) && !isJumping) { ... } // �X�y�[�X�L�[�̓��͂��擾
  ```

#### `Collision`
- **����**: �����Փ˃C�x���g�̏��̒񋟁B
- **�g�p��**:
  ```csharp
  void OnCollisionEnter(Collision collision)
  {
      if (collision.gameObject.CompareTag("Ground

"))
      {
          isJumping = false;
          animator.SetTrigger("JumpEnd");
      }
  }
  ```

���̃X�N���v�g�ł́A�����̃N���X���g�p���ăL�����N�^�[�̈ړ��A��]�A�A�j���[�V�����A�W�����v�𐧌䂵�A���[�U�[�̓��͂ɉ����ē����ύX���܂��B
 */
public class GuidecharaCon : MonoBehaviour
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
