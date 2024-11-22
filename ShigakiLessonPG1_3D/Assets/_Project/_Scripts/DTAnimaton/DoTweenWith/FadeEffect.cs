using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/*
 重要なメッセージや演出	1秒～3秒
通常のUIの表示・非表示	0.5秒～1秒
瞬間的な操作や通知	0.1秒～0.5秒
 */

namespace WithDOTween
{
    public class FadeEffect : MonoBehaviour
    {
        public CanvasGroup canvasGroup;
        public float fadeDuration = 0.5f;

        void Start()
        {
            canvasGroup.alpha = 0;
            canvasGroup.DOFade(1f, fadeDuration); // フェードイン
        }
    }
}
