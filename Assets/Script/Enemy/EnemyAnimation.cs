using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人动画类，负责播放动画
/// </summary>
public class EnemyAnimation : MonoBehaviour
{
    /// <summary>
    /// 动画标签
    /// </summary>
    public enum AnimType
    {
        Walking,Run,ShootsGun,SwordAttack,Idle,Death
    };
    public AnimationAction action;
    private void Awake()
    {
        action = new AnimationAction(GetComponentInChildren<Animator>());
    }
}
