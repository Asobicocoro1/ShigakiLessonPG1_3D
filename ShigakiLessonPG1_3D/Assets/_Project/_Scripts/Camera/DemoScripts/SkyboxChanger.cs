using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    // Skyboxのマテリアルを配列として保持
    public Material[] skyboxes;
    // 現在のSkyboxのインデックス
    private int currentSkyboxIndex = 0;

    // 初期設定
    void Start()
    {
        // 初期状態のSkyboxを設定
        if (skyboxes.Length > 0)
        {
            RenderSettings.skybox = skyboxes[currentSkyboxIndex];
        }
    }

    // トリガーに触れたときに呼ばれるメソッド
    void OnTriggerEnter(Collider other)
    {
        // 特定のタグ（例：Player）に触れた場合
        if (other.CompareTag("Player"))
        {
            ChangeSkybox();
        }
    }

    // Skyboxを変更するメソッド
    void ChangeSkybox()
    {
        // Skyboxのインデックスを更新
        currentSkyboxIndex = (currentSkyboxIndex + 1) % skyboxes.Length;
        // Skyboxを切り替える
        RenderSettings.skybox = skyboxes[currentSkyboxIndex];
    }
}

