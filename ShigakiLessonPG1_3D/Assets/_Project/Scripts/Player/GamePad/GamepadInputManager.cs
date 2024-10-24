using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class GamepadInputManager : MonoBehaviour
{
    public static GamepadInputManager Instance { get; private set; }

    private Dictionary<string, string> buttonMappings;
    private Dictionary<string, string> axisMappings;

    private void Awake()
    {
        // �V���O���g���p�^�[���̎���
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �I�u�W�F�N�g�����̃V�[���ł��j�����Ȃ��悤�ɂ���

            // �����A���̃I�u�W�F�N�g�����[�g�I�u�W�F�N�g�łȂ��ꍇ�A���[�g�Ɉړ�
            if (transform.parent != null)
            {
                transform.SetParent(null); // �e�I�u�W�F�N�g����؂藣�����ƂŃ��[�g�I�u�W�F�N�g�ɂ���
            }

            InitializeDefaultMappings();    // �f�t�H���g�̃}�b�s���O��ݒ�
        }
        else
        {
            Destroy(gameObject);  // �V���O���g�����ێ����A�d���C���X�^���X��j��
        }
    }

    // �f�t�H���g�̃{�^���E���}�b�s���O��ݒ�
    private void InitializeDefaultMappings()
    {
        buttonMappings = new Dictionary<string, string>
        {
            { "Slide", "Fire1" },      // B�{�^�� (��)
            { "Grapple", "Fire2" },    // R2�g���K�[ (��)
            { "Run", "Fire3" }         // L2�g���K�[ (��)
        };

        axisMappings = new Dictionary<string, string>
        {
            { "MoveHorizontal", "Horizontal" },     // ���X�e�B�b�NX��
            { "MoveVertical", "Vertical" },         // ���X�e�B�b�NY��
            { "LookHorizontal", "RightStickHorizontal" },   // �E�X�e�B�b�NX��
            { "LookVertical", "RightStickVertical" }        // �E�X�e�B�b�NY��
        };
    }

    public bool GetButtonDown(string action)
    {
        if (buttonMappings.ContainsKey(action))
        {
            return Input.GetButtonDown(buttonMappings[action]);
        }
        else
        {
            Debug.LogWarning($"Action '{action}' is not mapped.");
            return false;
        }
    }

    public bool GetButton(string action)
    {
        if (buttonMappings.ContainsKey(action))
        {
            return Input.GetButton(buttonMappings[action]);
        }
        else
        {
            Debug.LogWarning($"Action '{action}' is not mapped.");
            return false;
        }
    }

    public float GetAxis(string action)
    {
        if (axisMappings.ContainsKey(action))
        {
            return Input.GetAxis(axisMappings[action]);
        }
        else
        {
            Debug.LogWarning($"Axis '{action}' is not mapped.");
            return 0f;
        }
    }

    public void SetButtonMapping(string action, string button)
    {
        if (buttonMappings.ContainsKey(action))
        {
            buttonMappings[action] = button;
        }
        else
        {
            buttonMappings.Add(action, button);
        }
    }

    public void SetAxisMapping(string action, string axis)
    {
        if (axisMappings.ContainsKey(action))
        {
            axisMappings[action] = axis;
        }
        else
        {
            axisMappings.Add(action, axis);
        }
    }
}
