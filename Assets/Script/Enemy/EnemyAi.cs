using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����AI��ͨ���ж�ִ��Ѱ·������
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
    public float distanceOfPlayer = 2;               //������ƶ��ľ�������
    private float atckTimer;                       //������ʱ��
    private float atkInterVal = 2;                   //�������ʱ��
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
    /// AI����
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
    /// ��������
    /// </summary>
    /// <returns></returns>
    IEnumerator Damage()
    {
        info.Damage();
        yield return 0;
    }
    /// <summary>
    /// ������ʱ��
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    IEnumerator Wait(float time)
    {
        for (atckTimer = time; atckTimer > 0; atckTimer -= Time.deltaTime)
            yield return 0;
    }
    /// <summary>
    /// ���˿�������
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
    /// ������ƶ�
    /// </summary>
    IEnumerator PathToPlayer()
    {
        anim.action.Play(EnemyAnimation.AnimType.Run);
        audios.source.PlayAudioType(EnemyAudioCenter.AudioType.Run);
        yield return 0;
    }

    /// <summary>
    /// ����Զ�̹���
    /// </summary>
    IEnumerator Shootinig()
    {
        //if (anim.action.IsPlay(EnemyAnimation.AnimType.ShootsGun)) anim.action.Play(EnemyAnimation.AnimType.Idle);
        anim.action.Play(EnemyAnimation.AnimType.ShootsGun);
        yield return StartCoroutine(Wait(atkInterVal));
    }

    /// <summary>
    /// ����·��Ѱ·
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
    /// ���˽�ս����
    /// </summary>
    IEnumerator EnemyAttack()
    {
        anim.action.Play(EnemyAnimation.AnimType.SwordAttack);
        audios.source.PlayAudioType(EnemyAudioCenter.AudioType.Hit);
        yield return StartCoroutine(Wait(atkInterVal));
    }
    /// <summary>
    /// �ƶ���������
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
