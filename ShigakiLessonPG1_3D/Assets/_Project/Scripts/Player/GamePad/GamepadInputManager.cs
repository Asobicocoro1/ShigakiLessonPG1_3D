using System.Collections.Generic;
using UnityEngine;

public class GamepadInputManager : MonoBehaviour
{
    // シングルトンインスタンス
    public static GamepadInputManager Instance { get; private set; }

    private Dictionary<string, string> buttonMappings; // ボタンマッピング用の辞書
    private Dictionary<string, string> axisMappings;   // 軸マッピング用の辞書

    private void Awake()
    {
        // インスタンスが未設定なら自身をインスタンスとして保持
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーンが変わってもオブジェクトを破棄しない

            // 親がある場合は親オブジェクトから分離
            if (transform.parent != null)
            {
                transform.SetParent(null);
            }

            InitializeDefaultMappings();  // ボタンと軸のデフォルト設定を初期化
        }
        else
        {
            Destroy(gameObject); // インスタンスがすでに存在する場合、重複を防ぐために破棄
        }
    }

    // ボタンと軸のデフォルトマッピングを設定
    private void InitializeDefaultMappings()
    {
        buttonMappings = new Dictionary<string, string>
        {
            { "GrappleRight", "Fire5" }, // 右ワイヤーのボタン
            { "GrappleLeft", "Fire6" }   // 左ワイヤーのボタン
        };

        axisMappings = new Dictionary<string, string>
        {
            { "MoveHorizontal", "Horizontal" },            // 移動の横軸
            { "MoveVertical", "Vertical" },                // 移動の縦軸
            { "LookHorizontal", "RightStickHorizontal" },  // 視点移動の横軸
            { "LookVertical", "RightStickVertical" }       // 視点移動の縦軸
        };
    }

    // ボタンが押された瞬間の取得
    public bool GetButtonDown(string action)
    {
        // マッピングされたボタンが存在するか確認して押されたかを取得
        return buttonMappings.ContainsKey(action) && Input.GetButtonDown(buttonMappings[action]);
    }

    // ボタンが離された瞬間の取得
    public bool GetButtonUp(string action)
    {
        // マッピングされたボタンが存在するか確認して離されたかを取得
        return buttonMappings.ContainsKey(action) && Input.GetButtonUp(buttonMappings[action]);
    }

    // 軸の入力を取得
    public float GetAxis(string action)
    {
        // マッピングされた軸が存在する場合、その入力値を返す
        return axisMappings.ContainsKey(action) ? Input.GetAxis(axisMappings[action]) : 0f;
    }
}
