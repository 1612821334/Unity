using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��Ҷ����¼���
/// </summary>
public class PlayerAnimationEvent : MonoBehaviour
{
    /// <summary>
    /// ���ض���������
    /// </summary>
    private Animator anim;
    /// <summary>
    /// �����������ж���Ƭ��
    /// </summary>
    private AnimationClip[] clips;
    /// <summary>
    /// �����Ч
    /// </summary>
    private PlayerAudio playerAudio;
    /// <summary>
    /// �����¼�
    /// </summary>
    private AnimationEvent _event;
    /// <summary>
    /// �������ǹ
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
    /// ��Ӷ����¼�
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
    /// ��������¼�
    /// </summary>
    private void ClearAllEvent()
    {
        for(int i=0;i<clips.Length;i++)
        {
            clips[i].events=default(AnimationEvent[]);
        }
    }
}
