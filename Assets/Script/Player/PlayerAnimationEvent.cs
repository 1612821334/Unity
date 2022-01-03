using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 玩家动画事件类
/// </summary>
public class PlayerAnimationEvent : MonoBehaviour
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
    /// 玩家音效
    /// </summary>
    private PlayerAudio playerAudio;
    /// <summary>
    /// 动画事件
    /// </summary>
    private AnimationEvent _event;
    /// <summary>
    /// 玩家连发枪
    /// </summary>
    private AutomaticGun gun;
    public GameObject deathUI;
    private void Awake()
    {
        _event= new AnimationEvent();
        anim = GetComponent<Animator>();
        gun = GetComponentInChildren<AutomaticGun>();
        playerAudio = GetComponentInParent<PlayerAudio>();
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
        playerAudio.source.PlayAudioType(PlayerAudioCenter.AudioType.Death);
        deathUI.SetActive(true);
    }
    /// <summary>
    /// 添加动画事件
    /// </summary>
    private void AddAnimationEvent()
    {
        for(int i=0;i<clips.Length; i++)
        {
            switch (clips[i].name)
            {
                case "pistol shooting with recoil":
                        _event.functionName = "Fire";
                        _event.time = clips[i].length * 0.5f;
                        clips[i].AddEvent(_event); break;
                case "dead2":
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
        for(int i=0;i<clips.Length;i++)
        {
            clips[i].events=default(AnimationEvent[]);
        }
    }
}
