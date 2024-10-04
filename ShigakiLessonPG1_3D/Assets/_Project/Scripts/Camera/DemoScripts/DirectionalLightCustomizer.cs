using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DirectionalLightCustomizer : MonoBehaviour
{
    // ライトの強度
    public float lightIntensity = 1.5f;
    // ライトの色
    public Color lightColor = new Color(1.0f, 0.95f, 0.8f); // 少し黄色がかった色
    // シャドウの強度
    public float shadowStrength = 0.6f;
    // シャドウの解像度
    public LightShadowResolution shadowResolution = LightShadowResolution.High;
    // リアルタイムグローバルイルミネーションの有効化
    public bool enableRealtimeGI = true;
    // ライトクッキー
    public Texture lightCookie;

    void Start()
    {
        // Directional Lightコンポーネントの取得
        Light dirLight = GetComponent<Light>();

        // 強度と色の設定
        dirLight.intensity = lightIntensity;
        dirLight.color = lightColor;

        // シャドウの設定
        dirLight.shadows = LightShadows.Soft;
        dirLight.shadowStrength = shadowStrength;
        dirLight.shadowResolution = shadowResolution;

        // リアルタイムグローバルイルミネーションの設定
        dirLight.lightmapBakeType = enableRealtimeGI ? LightmapBakeType.Mixed : LightmapBakeType.Realtime;

        // ライトクッキーの設定
        if (lightCookie != null)
        {
            dirLight.cookie = lightCookie;
        }
    }
}

