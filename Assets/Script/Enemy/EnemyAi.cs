using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人AI，通过判断执行寻路攻击等
/// </summary>
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterController))]
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
    public float distanceOfPlayer = 2;               //向玩家移动的距离条件
    private float atckTimer;                       //攻击计时器
    private float atkInterVal = 2;                   //攻击间隔时间
    private void Awake()
    {
        anim = GetComponent<EnemyAnimation>();
        motor = GetComponent<EnemyMotor>();
        motor.mesh = GetComponentInChildren<MeshRenderer>();
        motor.enemyConl = GetComponent<CharacterController>();
        info = GetComponent<EnemyStatusInfo>();
        audios = GetComponent<EnemyAudio>();
    }
    private void OnEnable()
    {
        StartCoroutine("Ai");
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
    /// <summary>
    /// AI控制
    /// </summary>
    /// <returns></returns>
    IEnumerator Ai()
    {
        while (gameObject.activeInHierarchy)
        {
            yield return StartCoroutine("Damage");
            if (!info.state) { StartCoroutine("ConsoleCenter"); }
        }
    }
    /// <summary>
    /// 敌人受伤
    /// </summary>
    /// <returns></returns>
    IEnumerator Damage()
    {
        info.Damage();
        yield return 0;
    }
    /// <summary>
    /// 攻击计时器
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    IEnumerator Wait(float time)
    {
        for (atckTimer = time; atckTimer > 0; atckTimer -= Time.deltaTime)
            yield return 0;
    }
    /// <summary>
    /// 敌人控制中心
    /// </summary>
    IEnumerator ConsoleCenter()
    {
        yield return StartCoroutine("EnemyMove");
        switch (currentState)
        {
            case State.PathFinding:
                StartCoroutine("EnemyPathFinding");
                break;
            case State.FightAttack:
                StartCoroutine("EnemyAttack");
                break;
            case State.PathToPlayer:
                StartCoroutine("PathToPlayer"); break;
            case State.Shooting:
                StartCoroutine("Shootinig"); break;
        }
    }
    /// <summary>
    /// 向玩家移动
    /// </summary>
    IEnumerator PathToPlayer()
    {
        anim.action.Play(EnemyAnimation.AnimType.Run);
        audios.source.PlayAudioType(EnemyAudioCenter.AudioType.Run);
        yield return 0;
    }

    /// <summary>
    /// 敌人远程攻击
    /// </summary>
    IEnumerator Shootinig()
    {
        //if (anim.action.IsPlay(EnemyAnimation.AnimType.ShootsGun)) anim.action.Play(EnemyAnimation.AnimType.Idle);
        anim.action.Play(EnemyAnimation.AnimType.ShootsGun);
        yield return StartCoroutine(Wait(atkInterVal));
    }

    /// <summary>
    /// 敌人路点寻路
    /// </summary>
    IEnumerator EnemyPathFinding()
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
        yield return 0;
    }
    /// <summary>
    /// 敌人近战攻击
    /// </summary>
    IEnumerator EnemyAttack()
    {
        anim.action.Play(EnemyAnimation.AnimType.SwordAttack);
        audios.source.PlayAudioType(EnemyAudioCenter.AudioType.Hit);
        yield return StartCoroutine(Wait(atkInterVal));
    }
    /// <summary>
    /// 移动控制中心
    /// </summary>
    IEnumerator EnemyMove()
    {
        if (PlayerStatusInfo.istance.state)
        {
            currentState = State.PathFinding;
        }
        else if (Vector3.Distance(transform.position, motor.playerPoint.position) < distanceOfPlayer)
        {
            if (Vector3.Dot(transform.forward, (motor.playerPoint.position - transform.position).normalized) >= 0.5f)
            {
                switch (motor.MoveToPlyer())
                {
                    case State.PathToPlayer: currentState = State.PathToPlayer; break;
                    case State.FightAttack: currentState = State.FightAttack; break;
                    case State.Shooting: currentState = State.Shooting; break;
                }
            }
        }
        else
        {
            currentState = State.PathFinding;
        }
        yield return 0;
    }
}
