using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ҿ�������
/// </summary>
public class PlayerCenter : MonoBehaviour
{
    public float run_speed = 1f;                //�����ٶ�
    public float walk_speed = 0.5f;             //�����ٶ�
    private PlayerAnimation anim;               //��Ҷ�����
    private PlayerMotor motor;                  //������
    private PlayerAudio audios;                  //�����Ч
    private void Start()
    {
        anim = GetComponent<PlayerAnimation>();
        motor = GetComponent<PlayerMotor>();
        audios = GetComponent<PlayerAudio>();
        motor.playerConl = GetComponent<CharacterController>();
    }
    private void Update()
    {

        PlayerControlDetail();
    }
    /// <summary>
    /// ��Ҿ�����Ϊ
    /// </summary>
    private void PlayerControlDetail()
    {
        if (Input.GetKeyDown(KeyCode.Space))                                      //��Ծ
        {
            anim.action.PlayAnimation(PlayerAnimation.AnimType.Jump);
            audios.source.PlayAudioType(PlayerAudioCenter.AudioType.Jump);
        }
        else if (Input.GetMouseButton(0))                                         //���
        {
            anim.action.PlayAnimation(PlayerAnimation.AnimType.Shoot, true);
            audios.source.PlayAudioType(PlayerAudioCenter.AudioType.Shoot);
        }
        else if (Input.GetKey(KeyCode.S))                                         //����
        {
            motor.MoveMentBack(walk_speed);
            anim.action.PlayAnimation(PlayerAnimation.AnimType.WalkBack, true);
            audios.source.PlayAudioType(PlayerAudioCenter.AudioType.Walk);
        }
        else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))      //����
        {
            motor.MoveMentForward(run_speed);
            anim.action.PlayAnimation(PlayerAnimation.AnimType.Speed, 1);
            audios.source.PlayAudioType(PlayerAudioCenter.AudioType.Run);
        }
        else if (Input.GetKey(KeyCode.W))                                         //����
        {
            motor.MoveMentForward(walk_speed);
            anim.action.PlayAnimation(PlayerAnimation.AnimType.Speed, 0.5f);
            audios.source.PlayAudioType(PlayerAudioCenter.AudioType.Walk);
        }
        else if (Input.GetMouseButtonDown(1))                                     //��ս����
        {
            anim.action.PlayAnimation(PlayerAnimation.AnimType.Fight);
            audios.source.PlayAudioType(PlayerAudioCenter.AudioType.Hit);
        }
        else if (Input.GetKey(KeyCode.A))                                         //����
        {
            motor.MoveMentLeft(walk_speed);
            anim.action.PlayAnimation(PlayerAnimation.AnimType.Dircetion, 0.5f);
            audios.source.PlayAudioType(PlayerAudioCenter.AudioType.Walk);
        }
        else if (Input.GetKey(KeyCode.D))                                         //����
        {
            motor.MoveMentRight(walk_speed);
            anim.action.PlayAnimation(PlayerAnimation.AnimType.Dircetion, 1);
            audios.source.PlayAudioType(PlayerAudioCenter.AudioType.Walk);
        }
        else
        {
            anim.action.PlayAnimation(PlayerAnimation.AnimType.Other, 0);
            anim.action.PlayAnimation(PlayerAnimation.AnimType.Other, false);
        }
    }
}
