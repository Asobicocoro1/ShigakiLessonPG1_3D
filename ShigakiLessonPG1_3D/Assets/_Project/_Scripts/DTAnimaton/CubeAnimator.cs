using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // DOTweenを使用するための名前空間

public class CubeAnimator : MonoBehaviour
{
    void Start()
    {
        // 位置を(3, 3, 3)に2秒間で移動
        transform.DOMove(new Vector3(3, 3, 3), 2f);

        // スケールを(2, 2, 2)に1秒で拡大し、1秒後に元に戻す
        transform.DOScale(new Vector3(2, 2, 2), 1f).SetLoops(2, LoopType.Yoyo);

        // 回転を45度ずつ2秒で変化させ続ける
        transform.DORotate(new Vector3(0, 45, 0), 2f, RotateMode.LocalAxisAdd).SetLoops(-1, LoopType.Incremental);
    }
}

