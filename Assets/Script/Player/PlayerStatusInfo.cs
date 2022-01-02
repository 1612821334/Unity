using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家状态信息
/// </summary>
public class PlayerStatusInfo : MonoBehaviour
{
    [SerializeField]
    [Range(1, 1000)]
    public float hp = 100;                    //血量
    public float damage;                       //伤害
    public bool state;                         //玩家状态
    public PlayerAnimation anim;               //玩家动画
    public PlayerAudio audios;                 //玩家音效
    /// <summary>
    /// 提供外部使用
    /// </summary>
    public static PlayerStatusInfo istance { get; private set; }
    private void Awake()
    {
        istance = this;
    }
    public void Damage()
    {
        hp -= damage;
        if(hp<=0)
        {
            state = true;
            Death();
        }
        damage = 0;
    }
    public void Death()
    {
        anim.action.PlayAnimation(PlayerAnimation.AnimType.Death);
    }
}
