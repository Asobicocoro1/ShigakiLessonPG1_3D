using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSkyboxChanger : MonoBehaviour
{
    public Material[] skyboxes; // スカイボックスの配列
    private int currentSkyboxIndex = 0;
    private float changeInterval = 12.0f;
    private float timer = 0.0f;

    void Start()
    {
        if (skyboxes.Length > 0)
        {
            RenderSettings.skybox = skyboxes[currentSkyboxIndex];
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= changeInterval)
        {
            timer = 0.0f;
            ChangeSkybox();
        }
    }

    void ChangeSkybox()
    {
        currentSkyboxIndex = (currentSkyboxIndex + 1) % skyboxes.Length;
        RenderSettings.skybox = skyboxes[currentSkyboxIndex];
        DynamicGI.UpdateEnvironment(); // グローバルイルミネーションの更新
    }
}
