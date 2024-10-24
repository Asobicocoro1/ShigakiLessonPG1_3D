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

            InitializeDefaultMappings();  // ボタンのデフォルトマッピングを設定
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ボタン・軸マッピングを初期化
    private void InitializeDefaultMappings()
    {
        buttonMappings = new Dictionary<string, string>
        {
            { "Slide", "Fire1" },
            { "GrappleRight", "Fire2" },  // 右ワイヤーのボタンを設定
            { "GrappleLeft", "Fire3" },   // 左ワイヤーのボタンを設定
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

    // ボタンが押されたときの入力
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

    // ボタンが押されているかをチェック
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

    // **新しく追加するメソッド**: ボタンが離されたタイミングをチェック
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

    // 軸入力を取得
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

    // ボタンマッピングを設定
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

    // 軸マッピングを設定
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
