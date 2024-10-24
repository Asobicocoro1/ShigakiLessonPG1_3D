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
        // シングルトンパターンの実装
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // オブジェクトを次のシーンでも破棄しないようにする

            // もし、このオブジェクトがルートオブジェクトでない場合、ルートに移動
            if (transform.parent != null)
            {
                transform.SetParent(null); // 親オブジェクトから切り離すことでルートオブジェクトにする
            }

            InitializeDefaultMappings();    // デフォルトのマッピングを設定
        }
        else
        {
            Destroy(gameObject);  // シングルトンを維持し、重複インスタンスを破棄
        }
    }

    // デフォルトのボタン・軸マッピングを設定
    private void InitializeDefaultMappings()
    {
        buttonMappings = new Dictionary<string, string>
        {
            { "Slide", "Fire1" },      // Bボタン (例)
            { "Grapple", "Fire2" },    // R2トリガー (例)
            { "Run", "Fire3" }         // L2トリガー (例)
        };

        axisMappings = new Dictionary<string, string>
        {
            { "MoveHorizontal", "Horizontal" },     // 左スティックX軸
            { "MoveVertical", "Vertical" },         // 左スティックY軸
            { "LookHorizontal", "RightStickHorizontal" },   // 右スティックX軸
            { "LookVertical", "RightStickVertical" }        // 右スティックY軸
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
