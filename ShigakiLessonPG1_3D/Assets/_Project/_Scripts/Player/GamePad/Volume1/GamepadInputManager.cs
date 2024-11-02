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
            InitializeDefaultMappings();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeDefaultMappings()
    {
        buttonMappings = new Dictionary<string, string>
        {
            { "Grapple", "Fire5" },
            { "Slide", "Fire6" }
        };

        axisMappings = new Dictionary<string, string>
        {
            { "MoveHorizontal", "Horizontal" },
            { "MoveVertical", "Vertical" },
            { "LookHorizontal", "RightStickHorizontal" },
            { "LookVertical", "RightStickVertical" }
        };
    }

    public bool GetButtonDown(string action)
    {
        return buttonMappings.ContainsKey(action) && Input.GetButtonDown(buttonMappings[action]);
    }

    public bool GetButtonUp(string action)
    {
        return buttonMappings.ContainsKey(action) && Input.GetButtonUp(buttonMappings[action]);
    }

    public float GetAxis(string action)
    {
        return axisMappings.ContainsKey(action) ? Input.GetAxis(axisMappings[action]) : 0f;
    }
}
