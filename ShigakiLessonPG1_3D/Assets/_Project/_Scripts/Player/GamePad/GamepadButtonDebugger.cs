using UnityEngine;

public class GamepadButtonDebugger : MonoBehaviour
{
    private void Update()
    {
        // 各ボタンの押下状態を確認
        CheckButtons();
        
    }

    private void CheckButtons()
    {
        // ボタンの検出（joystick button 0 〜 19まで）
        for (int i = 0; i < 20; i++)
        {
            if (Input.GetKeyDown("joystick button " + i))
            {
                Debug.Log($"Button {i} pressed. (Mapped in Input Manager as 'joystick button {i}')");
            }
        }

        // "Grapple" ボタンの入力チェック
        if (GamepadInputManager.Instance.GetButtonDown("Grapple"))
        {
            Debug.Log("Grapple button pressed via GamepadInputManager.");
        }

        // "Slide" ボタンの入力チェック
        if (GamepadInputManager.Instance.GetButtonDown("Slide"))
        {
            Debug.Log("Slide button pressed via GamepadInputManager.");
        }
    }

    
}
