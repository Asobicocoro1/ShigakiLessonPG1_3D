using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRateSetter : MonoBehaviour
{
    void Start()
    {
        // �t���[�����[�g��30fps�ɌŒ�
        Application.targetFrameRate = 30;
    }

    void Update()
    {
        // �f�o�b�O�p�Ɍ��݂̃t���[�����[�g��\��
        //Debug.Log("Current FPS: " + (1.0f / Time.deltaTime));
    }
}

