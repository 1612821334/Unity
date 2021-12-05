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
    private EnemyGun gun;                          //����ǹе
    public float distanceOfPlayer=2;               //������ƶ��ľ�������
    private float atckTimer;                       //������ʱ��
    private float goTime;                          //��Ϸ����ʱ��
    private float atkInterVal=2;                   //�������ʱ��
    private void Start()
    {
        anim = GetComponent<EnemyAnimation>();
        motor = GetComponent<EnemyMotor>();
        motor.mesh = GetComponentInChildren<MeshRenderer>();
        motor.enemyConl = GetComponent<CharacterController>();
        info = GetComponent<EnemyStatusInfo>();
        audios = GetComponent<EnemyAudio>();
        gun = GetComponentInChildren<EnemyGun>();
    }
    private void Update()
    {
        goTime = Time.time;
        info.Damage();
        if (info.state == false) 
        {
            EnemyMove();
        }
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
        //if (anim.action.IsPlay(EnemyAnimation.AnimType.ShootsGun)) anim.action.Play(EnemyAnimation.AnimType.Idle);
        if (goTime >= atckTimer)
        {
            gun.Fire();
            anim.action.Play(EnemyAnimation.AnimType.ShootsGun);
            //audios.source.PlayAudioType(EnemyAudioCenter.AudioType.Shoot);
            atckTimer = goTime + atkInterVal;
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
        if (goTime >= atckTimer)
        {
            anim.action.Play(EnemyAnimation.AnimType.SwordAttack);
            audios.source.PlayAudioType(EnemyAudioCenter.AudioType.Hit);
            atckTimer = goTime + atkInterVal;
        }
    }
    /// <summary>
    /// �ƶ���������
    /// </summary>
    private void EnemyMove()
    {
        if (motor.playerPoint == null)
        {
            currentState = State.PathFinding;
            ConsoleCenter();
        }
        //Debug.DrawLine(transform.position, motor.playerPoint.position,Color.red);
        else if (Vector3.Distance(transform.position, motor.playerPoint.position) < distanceOfPlayer)
        {
            //print(Vector3.Dot(transform.forward, (motor.playerPoint.position - transform.position).normalized));
            if (Vector3.Dot(transform.forward, (motor.playerPoint.position - transform.position).normalized) >= 0.5f)
            {
                switch (motor.MoveToPlyer())
                {
                    case State.PathToPlayer: currentState = State.PathToPlayer; ConsoleCenter(); break;
                    case State.FightAttack: currentState = State.FightAttack; ConsoleCenter(); break;
                    case State.Shooting: currentState = State.Shooting; ConsoleCenter(); break;
                }
            }
        }
        else
        {
            currentState = State.PathFinding;
            ConsoleCenter();
        }
    }
}
