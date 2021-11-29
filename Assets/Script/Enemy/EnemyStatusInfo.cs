using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������Ϣ�࣬��ŵ���Ѫ�������ˣ�����
/// </summary>
public class EnemyStatusInfo : MonoBehaviour
{
    [SerializeField]
    [Range(0,1000)]
    public float hp =200;                    //Ѫ��
    private EnemyAi enemyAi;                 //AI��
    [HideInInspector]
    public  bool state;                      //״̬
    public float damage;                     //�˺���
    public EnemySpawn spawn;                 //����������
    private void Awake()
    {
        enemyAi = GetComponent<EnemyAi>();
    }
    /// <summary>
    /// ����
    /// </summary>
    /// <param name="amount"></param>
    public void Damage()
    {
        hp -= damage;
        if (hp <= 0) 
        {
            state = true;
            Death();
        }
        damage = 0;
    }
    /// <summary>
    /// ����
    /// </summary>
    public void Death()
    {
        enemyAi.anim.action.Play(EnemyAnimation.AnimType.Death);
        //�ȴ����������������
        if (enemyAi.anim.action.IsPlay(EnemyAnimation.AnimType.Death))
        {
            enemyAi.motor.poinits.IsUsable = true;
            enemyAi.audios.source.PlayAudioType(EnemyAudioCenter.AudioType.Death);
            Object.Destroy(this.gameObject);
            spawn.LateCreateEnemy();
        }
    }
}
