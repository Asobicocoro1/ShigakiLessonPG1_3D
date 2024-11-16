using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // DOTween���g�p���邽�߂̖��O���

public class CubeAnimator : MonoBehaviour
{
    void Start()
    {
        // �ʒu��(3, 3, 3)��2�b�Ԃňړ�
        transform.DOMove(new Vector3(3, 3, 3), 2f);

        // �X�P�[����(2, 2, 2)��1�b�Ŋg�債�A1�b��Ɍ��ɖ߂�
        transform.DOScale(new Vector3(2, 2, 2), 1f).SetLoops(2, LoopType.Yoyo);

        // ��]��45�x����2�b�ŕω�����������
        transform.DORotate(new Vector3(0, 45, 0), 2f, RotateMode.LocalAxisAdd).SetLoops(-1, LoopType.Incremental);
    }
}

