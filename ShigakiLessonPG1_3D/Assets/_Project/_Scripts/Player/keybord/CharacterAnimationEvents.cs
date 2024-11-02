using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationEvents : MonoBehaviour
{
    // このメソッドはアニメーションイベントを受け取ります
    public void OnFootstep()
    {
        Debug.Log("Footstep event triggered");
        // 足音の効果音を再生するなどの処理をここに追加します
    }
}