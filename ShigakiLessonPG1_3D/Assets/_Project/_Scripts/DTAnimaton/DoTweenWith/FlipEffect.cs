using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace WithDOTween
{
    public class FlipEffect : MonoBehaviour
    {
        public RectTransform card;
        public float flipDuration = 0.5f;

        public void Flip()
        {
            card.DORotate(new Vector3(0, 90, 0), flipDuration / 2, RotateMode.LocalAxisAdd)
                .OnComplete(() =>
                {
                    // 表示内容を切り替え（ここでスプライトなどを変更）
                    card.DORotate(new Vector3(0, 180, 0), flipDuration / 2, RotateMode.LocalAxisAdd);
                });
        }
    }
}
