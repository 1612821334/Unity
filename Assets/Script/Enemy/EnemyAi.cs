using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人AI，通过判断执行寻路攻击等
/// </summary>
[RequireComponent(typeof(EnemyStatusInfo))]
[RequireComponent(typeof(EnemyAnimation))]
[RequireComponent(typeof(EnemyAudio))]
[RequireComponent(typeof(EnemyMotor))]
public class EnemyAi : MonoBehaviour
{
    public enum State                              //敌人状态
    {
        PathFinding,                               //自动寻路
        FightAttack,                                    //攻击
        PathToPlayer,                              //朝向玩家移动
        Shooting
    }
    private State currentState = State.PathFinding;//敌人初始状态
    [HideInInspector]
    public EnemyAnimation anim;                    //敌人动画类
    [HideInInspector]
    public EnemyMotor motor;                       //敌人马达
    [HideInInspector]
    public EnemyStatusInfo info;                   //敌人状态类
    [HideInInspector]
    public EnemyAudio audios;                      //敌人音效
    public float damage;                           //伤害数
    public float distanceOfPlayer=2;               //向玩家移动的距离条件
    public float angle = 60;                       //敌人追击视野
    private float atckTimer;                       //攻击计时器
    private float atkInterVal=2;                   //攻击间隔时间
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
    /// 敌人控制中心
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
    /// 向玩家移动
    /// </summary>
    private void PathToPlayer()
    {
        anim.action.Play(EnemyAnimation.AnimType.Run);
        audios.source.PlayAudioType(EnemyAudioCenter.AudioType.Run);
    }

    /// <summary>
    /// 敌人远程攻击
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
    /// 敌人路点寻路
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
    /// 敌人近战攻击
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
    /// 敌人死亡判定
    /// </summary>
    /// <returns></returns>
    private bool IsDeath()
    {
        info.Damage(damage);
        damage = 0;
        return info.state;
    }
    /// <summary>
    /// 移动控制中心
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
