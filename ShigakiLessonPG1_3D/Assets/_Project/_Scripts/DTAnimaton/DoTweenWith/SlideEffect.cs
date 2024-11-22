using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace WithDOTween
{
    public class SlideEffect : MonoBehaviour
    {
        public RectTransform rectTransform;
        public Vector2 startPos = new Vector2(-500, 0);
        public Vector2 endPos = Vector2.zero;
        public float slideDuration = 0.5f;

        void Start()
        {
            rectTransform.anchoredPosition = startPos;
            rectTransform.DOAnchorPos(endPos, slideDuration); // スライドイン
        }
    }
}
/*
 1. スライドアウトの追加
スライドイン後、ボタン押下などのトリガーでスライドアウトするアニメーションを追加することも可能です。

csharp
コードをコピーする
public void SlideOut()
{
    rectTransform.DOAnchorPos(startPos, slideDuration); // 開始位置へ戻る
}
2. Easeの設定
DOTweenを使う場合、Easeを設定することでアニメーションの動きにメリハリを加えられます。

csharp
コードをコピーする
rectTransform.DOAnchorPos(endPos, slideDuration).SetEase(Ease.OutBounce);
 */
