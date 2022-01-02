using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���״̬��Ϣ
/// </summary>
public class PlayerStatusInfo : MonoBehaviour
{
    [SerializeField]
    [Range(1, 1000)]
    public float hp = 100;                    //Ѫ��
    public float damage;                       //�˺�
    public bool state;                         //���״̬
    public PlayerAnimation anim;               //��Ҷ���
    public PlayerAudio audios;                 //�����Ч
    /// <summary>
    /// �ṩ�ⲿʹ��
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
