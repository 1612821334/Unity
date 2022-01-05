using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    /// <summary>
    /// 任务完成数
    /// </summary>
    private float taskCount;
    private PlayerStatusInfo player;
    private void Awake()
    {
        _event = new AnimationEvent();
        anim = GetComponent<Animator>();
        gun = GetComponentInChildren<EnemyGun>();
        enemyAudio = GetComponentInParent<EnemyAudio>();
        player = GameObject.FindObjectOfType<PlayerStatusInfo>();
        clips = anim.runtimeAnimatorController.animationClips;
        AddAnimationEvent();
    }
    /// <summary>
    /// 销毁事件
    /// </summary>
    private void OnDestroy()
    {
        Invoke("ClearAllEvent",1);
    }
    /// <summary>
    /// 射击
    /// </summary>
    private void Fire()
    {
        gun.Fire();
    }
    /// <summary>
    /// 近战伤害
    /// </summary>
    private void Attack()
    {
        player.damage = 10;
        enemyAudio.source.PlayAudioType(EnemyAudioCenter.AudioType.Hit);
    }
    /// <summary>
    /// 死亡
    /// </summary>
    private void Death()
    {
        GameTask.instance.taskText.text = string.Format("消灭僵尸：{0}/12", ++taskCount);
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
                case "sword attack":
                    _event.functionName = "Attack";
                    _event.time = clips[i].length * 0.5f;
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
