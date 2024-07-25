using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    // Skybox�̃}�e���A����z��Ƃ��ĕێ�
    public Material[] skyboxes;
    // ���݂�Skybox�̃C���f�b�N�X
    private int currentSkyboxIndex = 0;

    // �����ݒ�
    void Start()
    {
        // ������Ԃ�Skybox��ݒ�
        if (skyboxes.Length > 0)
        {
            RenderSettings.skybox = skyboxes[currentSkyboxIndex];
        }
    }

    // �g���K�[�ɐG�ꂽ�Ƃ��ɌĂ΂�郁�\�b�h
    void OnTriggerEnter(Collider other)
    {
        // ����̃^�O�i��FPlayer�j�ɐG�ꂽ�ꍇ
        if (other.CompareTag("Player"))
        {
            ChangeSkybox();
        }
    }

    // Skybox��ύX���郁�\�b�h
    void ChangeSkybox()
    {
        // Skybox�̃C���f�b�N�X���X�V
        currentSkyboxIndex = (currentSkyboxIndex + 1) % skyboxes.Length;
        // Skybox��؂�ւ���
        RenderSettings.skybox = skyboxes[currentSkyboxIndex];
    }
}

