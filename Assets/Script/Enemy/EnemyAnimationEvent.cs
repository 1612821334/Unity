using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���˶����¼���
/// </summary>
public class EnemyAnimationEvent : MonoBehaviour
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
    /// ������Ч
    /// </summary>
    private EnemyAudio enemyAudio;
    /// <summary>
    /// �����¼�
    /// </summary>
    private AnimationEvent _event;
    /// <summary>
    /// ����ǹе
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
    /// ��Ӷ����¼�
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
    /// ��������¼�
    /// </summary>
    private void ClearAllEvent()
    {
        for (int i = 0; i < clips.Length; i++)
        {
            clips[i].events = default(AnimationEvent[]);
        }
    }
}
