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
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (transform.parent != null)
            {
                transform.SetParent(null);
            }

            InitializeDefaultMappings();  // �{�^���̃f�t�H���g�}�b�s���O��ݒ�
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // �{�^���E���}�b�s���O��������
    private void InitializeDefaultMappings()
    {
        buttonMappings = new Dictionary<string, string>
        {
            { "Slide", "Fire1" },
            { "GrappleRight", "Fire2" },  // �E���C���[�̃{�^����ݒ�
            { "GrappleLeft", "Fire3" },   // �����C���[�̃{�^����ݒ�
            { "Run", "Fire4" }
        };

        axisMappings = new Dictionary<string, string>
        {
            { "MoveHorizontal", "Horizontal" },
            { "MoveVertical", "Vertical" },
            { "LookHorizontal", "RightStickHorizontal" },
            { "LookVertical", "RightStickVertical" }
        };
    }

    // �{�^���������ꂽ�Ƃ��̓���
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

    // �{�^����������Ă��邩���`�F�b�N
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

    // **�V�����ǉ����郁�\�b�h**: �{�^���������ꂽ�^�C�~���O���`�F�b�N
    public bool GetButtonUp(string action)
    {
        if (buttonMappings.ContainsKey(action))
        {
            return Input.GetButtonUp(buttonMappings[action]);
        }
        else
        {
            Debug.LogWarning($"Action '{action}' is not mapped.");
            return false;
        }
    }

    // �����͂��擾
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

    // �{�^���}�b�s���O��ݒ�
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

    // ���}�b�s���O��ݒ�
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
