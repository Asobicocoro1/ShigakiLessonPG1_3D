using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRateSetter : MonoBehaviour
{
    void Start()
    {
        // フレームレートを30fpsに固定
        Application.targetFrameRate = 30;
    }

    void Update()
    {
        // デバッグ用に現在のフレームレートを表示
        //Debug.Log("Current FPS: " + (1.0f / Time.deltaTime));
    }
}

