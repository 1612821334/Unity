using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����AI��ͨ���ж�ִ��Ѱ·������
/// </summary>
[RequireComponent(typeof(EnemyStatusInfo))]
[RequireComponent(typeof(EnemyAnimation))]
[RequireComponent(typeof(EnemyAudio))]
[RequireComponent(typeof(EnemyMotor))]
public class EnemyAi : MonoBehaviour
{
    public enum State                              //����״̬
    {
        PathFinding,                               //�Զ�Ѱ·
        FightAttack,                                    //����
        PathToPlayer,                              //��������ƶ�
        Shooting
    }
    private State currentState = State.PathFinding;//���˳�ʼ״̬
    [HideInInspector]
    public EnemyAnimation anim;                    //���˶�����
    [HideInInspector]
    public EnemyMotor motor;                       //�������
    [HideInInspector]
    public EnemyStatusInfo info;                   //����״̬��
    [HideInInspector]
    public EnemyAudio audios;                      //������Ч
    public float damage;                           //�˺���
    public float distanceOfPlayer=2;               //������ƶ��ľ�������
    public float angle = 60;                       //����׷����Ұ
    private float atckTimer;                       //������ʱ��
    private float atkInterVal=2;                   //�������ʱ��
    private void Start()
    {
        anim = GetComponent<EnemyAnimation>();
        motor = GetComponent<EnemyMotor>();
        motor.mesh = GetComponentInChildren<MeshRenderer>();
        motor.enemyConl = GetComponent<CharacterController>();
        info = GetComponent<EnemyStatusInfo>();
        audios = GetComponent<EnemyAudio>();
    }
    private void Update()
    {
        if (IsDeath() != true)
        {
            EnemyMove();
        }
        else return;
    }
    /// <summary>
    /// ���˿�������
    /// </summary>
    private void ConsoleCenter()
    {
        switch (currentState)
        {
            case State.PathFinding:
                EnemyPathFinding();
                break;
            case State.FightAttack:
                EnemyAttack();
                break;
            case State.PathToPlayer:
                PathToPlayer(); break;
            case State.Shooting:
                Shootinig();break;
        }
    }
    /// <summary>
    /// ������ƶ�
    /// </summary>
    private void PathToPlayer()
    {
        anim.action.Play(EnemyAnimation.AnimType.Run);
        audios.source.PlayAudioType(EnemyAudioCenter.AudioType.Run);
    }

    /// <summary>
    /// ����Զ�̹���
    /// </summary>
    private void Shootinig()
    {
        if (anim.action.IsPlay(EnemyAnimation.AnimType.ShootsGun)) anim.action.Play(EnemyAnimation.AnimType.Idle);
        if (Time.time >= atckTimer)
        {
            anim.action.Play(EnemyAnimation.AnimType.ShootsGun);
            audios.source.PlayAudioType(EnemyAudioCenter.AudioType.Shoot);
            atckTimer = Time.time + atkInterVal;
        }
    }

    /// <summary>
    /// ����·��Ѱ·
    /// </summary>
    private void EnemyPathFinding()
    {
        if (motor.Pathfinding())
        {
            anim.action.Play(EnemyAnimation.AnimType.Run);
            audios.source.PlayAudioType(EnemyAudioCenter.AudioType.Run);
        }
        else
        {
            anim.action.Play(EnemyAnimation.AnimType.Idle);
        }
    }
    /// <summary>
    /// ���˽�ս����
    /// </summary>
    private void EnemyAttack()
    {
        if (Time.time >= atckTimer)
        {
            anim.action.Play(EnemyAnimation.AnimType.SwordAttack);
            audios.source.PlayAudioType(EnemyAudioCenter.AudioType.Hit);
            atckTimer = Time.time + atkInterVal;
        }
    }
    /// <summary>
    /// ���������ж�
    /// </summary>
    /// <returns></returns>
    private bool IsDeath()
    {
        info.Damage(damage);
        damage = 0;
        return info.state;
    }
    /// <summary>
    /// �ƶ���������
    /// </summary>
    private void EnemyMove()
    {
        Debug.DrawLine(transform.position, motor.playerPoint.position,Color.red);
        if (Vector3.Distance(motor.playerPoint.position,transform.position) < distanceOfPlayer
            && Vector3.Angle(transform.forward, (motor.playerPoint.position - transform.position).normalized) < angle)
        {
            switch (motor.MoveToPlyer())
            {
                case State.PathToPlayer: currentState = State.PathToPlayer; ConsoleCenter(); break;
                case State.FightAttack: currentState = State.FightAttack; ConsoleCenter(); break;
                case State.Shooting: currentState = State.Shooting; ConsoleCenter(); break;
            }
        }
        else
        {
            currentState = State.PathFinding;
            ConsoleCenter();
        }
    }
}
