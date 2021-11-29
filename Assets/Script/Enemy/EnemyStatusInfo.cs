using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人信息类，存放敌人血量，受伤，死亡
/// </summary>
public class EnemyStatusInfo : MonoBehaviour
{
    [SerializeField]
    [Range(0,1000)]
    public float hp =200;                    //血量
    private EnemyAi enemyAi;                 //AI类
    [HideInInspector]
    public  bool state;                      //状态
    public float damage;                     //伤害数
    public EnemySpawn spawn;                 //敌人生成器
    private void Awake()
    {
        enemyAi = GetComponent<EnemyAi>();
    }
    /// <summary>
    /// 受伤
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
    /// 死亡
    /// </summary>
    public void Death()
    {
        enemyAi.anim.action.Play(EnemyAnimation.AnimType.Death);
        //等待死亡动画播放完成
        if (enemyAi.anim.action.IsPlay(EnemyAnimation.AnimType.Death))
        {
            enemyAi.motor.poinits.IsUsable = true;
            enemyAi.audios.source.PlayAudioType(EnemyAudioCenter.AudioType.Death);
            Object.Destroy(this.gameObject);
            spawn.LateCreateEnemy();
        }
    }
}
