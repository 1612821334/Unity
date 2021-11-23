using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class PlayerStatusInfo : MonoBehaviour
{
    [SerializeField]
    [Range(1, 100)]
    private float hp = 100;                    //血量
    public float damage;                       //伤害
    public PlayerAnimation anim;               //玩家动画
    public PlayerAudio audios;                 //玩家音效
    public void Damage()
    {
        hp -= damage;
        if(hp<=0)
        {
            Death();
        }
    }
    public void Death()
    {
        anim.action.PlayAnimation(PlayerAnimation.AnimType.Death);
        //等待死亡动画播放完成
        if (anim.action.IsPlay(PlayerAnimation.AnimType.Death))
        {
            audios.source.PlayAudioType(PlayerAudioCenter.AudioType.Death);
            Object.Destroy(this.gameObject);
        }
        return;
    }
}
