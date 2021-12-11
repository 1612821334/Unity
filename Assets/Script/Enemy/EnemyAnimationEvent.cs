using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人动画事件类
/// </summary>
public class EnemyAnimationEvent : MonoBehaviour
{
    /// <summary>
    /// 挂载动画控制器
    /// </summary>
    private Animator anim;
    /// <summary>
    /// 动画控制器中动画片段
    /// </summary>
    private AnimationClip[] clips;
    /// <summary>
    /// 敌人音效
    /// </summary>
    private EnemyAudio enemyAudio;
    /// <summary>
    /// 动画事件
    /// </summary>
    private AnimationEvent _event;
    /// <summary>
    /// 敌人枪械
    /// </summary>
    private EnemyGun gun;
    private void Awake()
    {
        _event = new AnimationEvent();
        anim = GetComponent<Animator>();
        gun = GetComponentInChildren<EnemyGun>();
        enemyAudio = GetComponentInParent<EnemyAudio>();
        clips = anim.runtimeAnimatorController.animationClips;
        AddAnimationEvent();
    }
    private void OnDestroy()
    {
        ClearAllEvent();
    }
    private void Fire()
    {
        gun.Fire();
    }
    private void Death()
    {
        enemyAudio.source.PlayAudioType(EnemyAudioCenter.AudioType.Death);
    }
    /// <summary>
    /// 添加动画事件
    /// </summary>
    private void AddAnimationEvent()
    {
        for (int i = 0; i < clips.Length; i++)
        {
            switch (clips[i].name)
            {
                case "shoots gun_2":
                    _event.functionName = "Fire";
                    _event.time = clips[i].length * 0.5f;
                    clips[i].AddEvent(_event); break;
                case "Death":
                    _event.functionName = "Death";
                    _event.time = clips[i].length * 0.1f;
                    clips[i].AddEvent(_event); break;
            }
        }
        anim.Rebind();
    }
    /// <summary>
    /// 清除所有事件
    /// </summary>
    private void ClearAllEvent()
    {
        for (int i = 0; i < clips.Length; i++)
        {
            clips[i].events = default(AnimationEvent[]);
        }
    }
}
