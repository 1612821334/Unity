using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    /// <summary>
    /// ���������
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
    /// �����¼�
    /// </summary>
    private void OnDestroy()
    {
        Invoke("ClearAllEvent",1);
    }
    /// <summary>
    /// ���
    /// </summary>
    private void Fire()
    {
        gun.Fire();
    }
    /// <summary>
    /// ��ս�˺�
    /// </summary>
    private void Attack()
    {
        player.damage = 10;
        enemyAudio.source.PlayAudioType(EnemyAudioCenter.AudioType.Hit);
    }
    /// <summary>
    /// ����
    /// </summary>
    private void Death()
    {
        GameTask.instance.taskText.text = string.Format("����ʬ��{0}/12", ++taskCount);
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
                case "sword attack":
                    _event.functionName = "Attack";
                    _event.time = clips[i].length * 0.5f;
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
