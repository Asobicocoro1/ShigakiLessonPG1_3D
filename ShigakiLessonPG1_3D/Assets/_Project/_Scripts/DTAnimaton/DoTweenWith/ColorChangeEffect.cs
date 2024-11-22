using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace WithDOTween
{
    public class ColorChangeEffect : MonoBehaviour
    {
        public Image buttonImage;
        public float colorDuration = 0.3f;

        void Start()
        {
            buttonImage.DOColor(Color.green, colorDuration);
        }
    }
}
/*ボタンで色変更をトリガーする:
Buttonを使用している場合、クリック時に色を変えることもできます。
ButtonのOn Click()イベントにColorChangeEffectのメソッドを設定。

 インタラクティブな色変更
ボタンのホバー時に色を変える: ボタンのEvent Triggerを使用して、ホバーやクリック時に異なる色を適用できます。
2. Easeのカスタマイズ
DOTweenを使用している場合、色変更にEaseを適用して変化を滑らかにできます。

参考スクリプト
image.DOColor(endColor, colorDuration).SetEase(Ease.InOutSine);*/
