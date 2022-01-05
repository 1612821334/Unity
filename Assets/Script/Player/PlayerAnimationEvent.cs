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
    public float attackDistance = 0.5f;
    private EnemyStatusInfo enemyInfo;
    private void Awake()
    {
        _event= new AnimationEvent();
        anim = GetComponent<Animator>();
        gun = GetComponentInChildren<AutomaticGun>();
        playerAudio = GetComponentInParent<PlayerAudio>();
        enemyInfo = GameObject.FindObjectOfType<EnemyStatusInfo>();
        clips = anim.runtimeAnimatorController.animationClips;
        AddAnimationEvent();
    }
    private void OnDestroy()
    {
        ClearAllEvent();
    }
    /// <summary>
    /// ���
    /// </summary>
    private void Fire()
    {
        gun.Fire();
    }
    /// <summary>
    /// ��ս����
    /// </summary>
    private void Attack()
    {
        if (Vector3.Distance(transform.position, enemyInfo.transform.position) <= attackDistance)
        {
            enemyInfo.damage = 10;
        }
    }
    /// <summary>
    /// ����
    /// </summary>
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
                case "puch left and right":
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
        for(int i=0;i<clips.Length;i++)
        {
            clips[i].events=default(AnimationEvent[]);
        }
    }
}
