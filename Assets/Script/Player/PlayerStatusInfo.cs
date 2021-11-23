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
    private float hp = 100;                    //Ѫ��
    public float damage;                       //�˺�
    public PlayerAnimation anim;               //��Ҷ���
    public PlayerAudio audios;                 //�����Ч
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
        //�ȴ����������������
        if (anim.action.IsPlay(PlayerAnimation.AnimType.Death))
        {
            audios.source.PlayAudioType(PlayerAudioCenter.AudioType.Death);
            Object.Destroy(this.gameObject);
        }
        return;
    }
}
