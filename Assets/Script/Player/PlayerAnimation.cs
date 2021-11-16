using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家动画类，负责播放动画
/// </summary>
public class PlayerAnimation : MonoBehaviour
{
    /// <summary>
    /// 动画标签
    /// </summary>
    public enum AnimType
    {
        Walking, WalkBack, Run, Shoot, Jump, Idle, Death, Fight, Dircetion, Speed, Other
    };
    public PlayerAnimationAction action;
    private void Awake()
    {
        action = new PlayerAnimationAction(GetComponent<Animator>());
    }
}
