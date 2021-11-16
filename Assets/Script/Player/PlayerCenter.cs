using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家控制中心
/// </summary>
public class PlayerCenter : MonoBehaviour
{
    public float run_speed = 1f;                //奔跑速度
    public float walk_speed = 0.5f;             //行走速度
    private PlayerAnimation anim;               //玩家动画类
    private PlayerMotor motor;                  //玩家马达
    private PlayerAudio audios;                  //玩家音效
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
    /// 玩家具体行为
    /// </summary>
    private void PlayerControlDetail()
    {
        if (Input.GetKeyDown(KeyCode.Space))                                      //跳跃
        {
            anim.action.PlayAnimation(PlayerAnimation.AnimType.Jump);
            audios.source.PlayAudioType(PlayerAudioCenter.AudioType.Jump);
        }
        else if (Input.GetMouseButton(0))                                         //射击
        {
            anim.action.PlayAnimation(PlayerAnimation.AnimType.Shoot, true);
            audios.source.PlayAudioType(PlayerAudioCenter.AudioType.Shoot);
        }
        else if (Input.GetKey(KeyCode.S))                                         //后退
        {
            motor.MoveMentBack(walk_speed);
            anim.action.PlayAnimation(PlayerAnimation.AnimType.WalkBack, true);
            audios.source.PlayAudioType(PlayerAudioCenter.AudioType.Walk);
        }
        else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))      //奔跑
        {
            motor.MoveMentForward(run_speed);
            anim.action.PlayAnimation(PlayerAnimation.AnimType.Speed, 1);
            audios.source.PlayAudioType(PlayerAudioCenter.AudioType.Run);
        }
        else if (Input.GetKey(KeyCode.W))                                         //行走
        {
            motor.MoveMentForward(walk_speed);
            anim.action.PlayAnimation(PlayerAnimation.AnimType.Speed, 0.5f);
            audios.source.PlayAudioType(PlayerAudioCenter.AudioType.Walk);
        }
        else if (Input.GetMouseButtonDown(1))                                     //近战攻击
        {
            anim.action.PlayAnimation(PlayerAnimation.AnimType.Fight);
            audios.source.PlayAudioType(PlayerAudioCenter.AudioType.Hit);
        }
        else if (Input.GetKey(KeyCode.A))                                         //左移
        {
            motor.MoveMentLeft(walk_speed);
            anim.action.PlayAnimation(PlayerAnimation.AnimType.Dircetion, 0.5f);
            audios.source.PlayAudioType(PlayerAudioCenter.AudioType.Walk);
        }
        else if (Input.GetKey(KeyCode.D))                                         //右移
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
