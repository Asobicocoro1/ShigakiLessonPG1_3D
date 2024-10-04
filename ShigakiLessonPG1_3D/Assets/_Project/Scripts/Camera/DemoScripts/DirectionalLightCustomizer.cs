using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DirectionalLightCustomizer : MonoBehaviour
{
    // ���C�g�̋��x
    public float lightIntensity = 1.5f;
    // ���C�g�̐F
    public Color lightColor = new Color(1.0f, 0.95f, 0.8f); // �������F���������F
    // �V���h�E�̋��x
    public float shadowStrength = 0.6f;
    // �V���h�E�̉𑜓x
    public LightShadowResolution shadowResolution = LightShadowResolution.High;
    // ���A���^�C���O���[�o���C���~�l�[�V�����̗L����
    public bool enableRealtimeGI = true;
    // ���C�g�N�b�L�[
    public Texture lightCookie;

    void Start()
    {
        // Directional Light�R���|�[�l���g�̎擾
        Light dirLight = GetComponent<Light>();

        // ���x�ƐF�̐ݒ�
        dirLight.intensity = lightIntensity;
        dirLight.color = lightColor;

        // �V���h�E�̐ݒ�
        dirLight.shadows = LightShadows.Soft;
        dirLight.shadowStrength = shadowStrength;
        dirLight.shadowResolution = shadowResolution;

        // ���A���^�C���O���[�o���C���~�l�[�V�����̐ݒ�
        dirLight.lightmapBakeType = enableRealtimeGI ? LightmapBakeType.Mixed : LightmapBakeType.Realtime;

        // ���C�g�N�b�L�[�̐ݒ�
        if (lightCookie != null)
        {
            dirLight.cookie = lightCookie;
        }
    }
}

