using System.Collections.Generic;
using UnityEngine;

public class GamepadInputManager : MonoBehaviour
{
    // �V���O���g���C���X�^���X
    public static GamepadInputManager Instance { get; private set; }

    private Dictionary<string, string> buttonMappings; // �{�^���}�b�s���O�p�̎���
    private Dictionary<string, string> axisMappings;   // ���}�b�s���O�p�̎���

    private void Awake()
    {
        // �C���X�^���X�����ݒ�Ȃ玩�g���C���X�^���X�Ƃ��ĕێ�
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �V�[�����ς���Ă��I�u�W�F�N�g��j�����Ȃ�

            // �e������ꍇ�͐e�I�u�W�F�N�g���番��
            if (transform.parent != null)
            {
                transform.SetParent(null);
            }

            InitializeDefaultMappings();  // �{�^���Ǝ��̃f�t�H���g�ݒ��������
        }
        else
        {
            Destroy(gameObject); // �C���X�^���X�����łɑ��݂���ꍇ�A�d����h�����߂ɔj��
        }
    }

    // �{�^���Ǝ��̃f�t�H���g�}�b�s���O��ݒ�
    private void InitializeDefaultMappings()
    {
        buttonMappings = new Dictionary<string, string>
        {
            { "GrappleRight", "Fire5" }, // �E���C���[�̃{�^��
            { "GrappleLeft", "Fire6" }   // �����C���[�̃{�^��
        };

        axisMappings = new Dictionary<string, string>
        {
            { "MoveHorizontal", "Horizontal" },            // �ړ��̉���
            { "MoveVertical", "Vertical" },                // �ړ��̏c��
            { "LookHorizontal", "RightStickHorizontal" },  // ���_�ړ��̉���
            { "LookVertical", "RightStickVertical" }       // ���_�ړ��̏c��
        };
    }

    // �{�^���������ꂽ�u�Ԃ̎擾
    public bool GetButtonDown(string action)
    {
        // �}�b�s���O���ꂽ�{�^�������݂��邩�m�F���ĉ����ꂽ�����擾
        return buttonMappings.ContainsKey(action) && Input.GetButtonDown(buttonMappings[action]);
    }

    // �{�^���������ꂽ�u�Ԃ̎擾
    public bool GetButtonUp(string action)
    {
        // �}�b�s���O���ꂽ�{�^�������݂��邩�m�F���ė����ꂽ�����擾
        return buttonMappings.ContainsKey(action) && Input.GetButtonUp(buttonMappings[action]);
    }

    // ���̓��͂��擾
    public float GetAxis(string action)
    {
        // �}�b�s���O���ꂽ�������݂���ꍇ�A���̓��͒l��Ԃ�
        return axisMappings.ContainsKey(action) ? Input.GetAxis(axisMappings[action]) : 0f;
    }
}
