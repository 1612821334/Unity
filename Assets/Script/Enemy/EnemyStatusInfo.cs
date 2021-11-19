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
    public EnemySpawn spawn;                 //����������
    private void Awake()
    {
        enemyAi = GetComponent<EnemyAi>();
    }
    /// <summary>
    /// ����
    /// </summary>
    /// <param name="amount"></param>
    public void Damage(float amount)
    {
        hp -= amount;
        if (hp <= 0) 
        {
            state = true;
            Death();
        }
    }
    /// <summary>
    /// ����
    /// </summary>
    public void Death()
    {
        enemyAi.anim.action.Play(EnemyAnimation.AnimType.Death);
        //�ȴ����������������
        #region
        //string animString = "";
        //var arr = enemyAi.anim.action.anim.GetCurrentAnimatorClipInfo(0);
        //if (arr != null && arr.Length >= 1)
        //{
        //    animString = arr[0].clip.name;
        //    print(animString);
        //}
        #endregion
        if (enemyAi.anim.action.IsPlay(EnemyAnimation.AnimType.Death))
        {
            enemyAi.motor.poinits.IsUsable = true;
            enemyAi.audios.source.PlayAudioType(EnemyAudioCenter.AudioType.Death);
            Object.Destroy(this.gameObject);
            spawn.LateCreateEnemy();
        }
    }
}
