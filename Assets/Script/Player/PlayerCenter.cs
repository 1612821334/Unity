using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ҿ�������
/// </summary>
[RequireComponent(typeof(PlayerStatusInfo))]
[RequireComponent(typeof(PlayerAnimation))]
[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(PlayerAudio))]
public class PlayerCenter : MonoBehaviour
{
    public float run_speed = 1f;                //�����ٶ�
    public float walk_speed = 0.5f;             //�����ٶ�
    private PlayerAnimation anim;               //��Ҷ�����
    private PlayerMotor motor;                  //������
    private PlayerAudio audios;                 //�����Ч
    private PlayerStatusInfo info;              //�����Ϣ
    private void Start()
    {
        anim = GetComponent<PlayerAnimation>();
        motor = GetComponent<PlayerMotor>();
        audios = GetComponent<PlayerAudio>();
        info = GetComponent<PlayerStatusInfo>();
        info.anim = anim;info.audios = audios;
        motor.playerConl = GetComponent<CharacterController>();
    }
    private void Update()
    {
        info.Damage();
        if (info.state == false) 
        {
            PlayerControlDetail();
        }
    }
    /// <summary>
    /// ��Ҿ�����Ϊ
    /// </summary>
    private void PlayerControlDetail()
    {
        if (Input.GetButtonDown("Jump"))                                     
        {
            MoveAnimAudio(PlayerAnimation.AnimType.Jump, PlayerAudioCenter.AudioType.Jump); //��Ծ
        }
        else if (Input.GetAxis("Fire1") > 0)                                         
        {
            MoveAnimAudio(PlayerAnimation.AnimType.Shoot, PlayerAudioCenter.AudioType.Shoot, true); //���
        }
        else if (Input.GetAxis("Vertical")<0)                                        
        {
            MoveAnimAudio(walk_speed, PlayerAnimation.AnimType.WalkBack, true, PlayerAudioCenter.AudioType.Walk); //����
        }
        else if (Input.GetAxis("Fire3") >0 && Input.GetAxis("Vertical") > 0)      
        {
            MoveAnimAudio(run_speed, PlayerAnimation.AnimType.Speed, 1, PlayerAudioCenter.AudioType.Run);//����
        }
        else if (Input.GetAxis("Vertical")>0)                                         
        {
            MoveAnimAudio(walk_speed, PlayerAnimation.AnimType.Speed, 0.5f, PlayerAudioCenter.AudioType.Walk);//����
        }
        else if (Input.GetAxis("Fire2")>0)                                     
        {
            MoveAnimAudio(PlayerAnimation.AnimType.Fight, PlayerAudioCenter.AudioType.Hit);//��ս����
        }
        else if (Input.GetAxis("Horizontal")<0)                                        
        {
            MoveAnimAudio(walk_speed, PlayerAnimation.AnimType.Dircetion, 0.5f, PlayerAudioCenter.AudioType.Walk); //����
        }
        else if (Input.GetAxis("Horizontal")>0)                                         
        {
            MoveAnimAudio(walk_speed, PlayerAnimation.AnimType.Dircetion, 1, PlayerAudioCenter.AudioType.Walk);//����
        }
        else
        {
            anim.action.PlayAnimation(PlayerAnimation.AnimType.Other, 0);
            anim.action.PlayAnimation(PlayerAnimation.AnimType.Other, false);
        }
    }
    /// <summary>
    /// ��Ծ����ս������
    /// </summary>
    /// <param name="animatorType"></param>
    /// <param name="aduioType"></param>
    private void MoveAnimAudio(PlayerAnimation.AnimType animatorType, PlayerAudioCenter.AudioType aduioType)
    {
        anim.action.PlayAnimation(animatorType);
        audios.source.PlayAudioType(aduioType);
    }
    /// <summary>
    /// ���
    /// </summary>
    /// <param name="animatorType"></param>
    /// <param name="aduioType"></param>
    /// <param name="state"></param>
    private void MoveAnimAudio(PlayerAnimation.AnimType animatorType, PlayerAudioCenter.AudioType aduioType, bool state)
    {
        anim.action.PlayAnimation(animatorType, state);
        audios.source.PlayAudioType(aduioType);
    }
    /// <summary>
    /// ����
    /// </summary>
    /// <param name="speed"></param>
    /// <param name="animatorType"></param>
    /// <param name="state"></param>
    /// <param name="aduioType"></param>
    private void MoveAnimAudio(float speed, PlayerAnimation.AnimType animatorType, bool state, PlayerAudioCenter.AudioType aduioType)
    {
        motor.MoveMentBack(speed);
        anim.action.PlayAnimation(animatorType, state);
        audios.source.PlayAudioType(aduioType);
    }
    /// <summary>
    /// ǰ���������ƶ������ܵ�
    /// </summary>
    /// <param name="speed"></param>
    /// <param name="animatorType"></param>
    /// <param name="speedType"></param>
    /// <param name="aduioType"></param>
    private void MoveAnimAudio(float speed, PlayerAnimation.AnimType animatorType, float speedType, PlayerAudioCenter.AudioType aduioType)
    {
        switch (animatorType)
        {
            case PlayerAnimation.AnimType.Speed: motor.MoveMentForward(speed);break;
            case PlayerAnimation.AnimType.Dircetion: if (speedType == 0.5f)
                {
                    motor.MoveMentLeft(speed);
                }
                else motor.MoveMentRight(speed); break;
        }
        anim.action.PlayAnimation(animatorType, speedType);
        audios.source.PlayAudioType(aduioType);
    }
}
