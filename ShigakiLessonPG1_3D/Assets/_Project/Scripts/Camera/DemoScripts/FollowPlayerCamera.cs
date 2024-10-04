using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour
{
    public Transform player;  // �Ǐ]����v���C���[��Transform
    public Vector3 offset;    // �J�����̃I�t�Z�b�g
    public float smoothSpeed = 0.125f;  // �Ǐ]�̃X���[�Y��

    void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(player);
    }
}

