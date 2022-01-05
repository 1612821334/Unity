using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家控制中心
/// </summary>
[RequireComponent(typeof(PlayerStatusInfo))]
[RequireComponent(typeof(PlayerAnimation))]
[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(PlayerAudio))]
public class PlayerCenter : MonoBehaviour
{
    public float run_speed = 1f;                //奔跑速度
    public float walk_speed = 0.5f;             //行走速度
    private PlayerAnimation anim;               //玩家动画类
    private PlayerMotor motor;                  //玩家马达
    private PlayerAudio audios;                 //玩家音效
    private PlayerStatusInfo playerInfo;        //玩家信息
    private AutomaticGun gun;
    public delegate void playergate();
    public playergate playergates;
    public GameObject myBag;
    public GameObject myMenu;
    private bool isOPen;
    private void Start()
    {
        playergates += OpenBag;
        playergates += OpenMenu;
        playergates += JumpOrBack;
        playergates += Fight;
        playergates += RunOrMove;
        playergates += LeftRightMove;
        playergates += Idle;
        anim = GetComponent<PlayerAnimation>();
        motor = GetComponent<PlayerMotor>();
        audios = GetComponent<PlayerAudio>();
        playerInfo = GetComponent<PlayerStatusInfo>();
        playerInfo.anim = anim; playerInfo.audios = audios;
        motor.playerConl = GetComponent<CharacterController>();
        gun = GetComponentInChildren<AutomaticGun>();
    }
    private void Update()
    {
        if (playerInfo.state)
        {
            return;
        }
        playerInfo.Damage();
        PlayerControlDetail(playergates);
    }
    /// <summary>
    /// 玩家具体行为
    /// </summary>
    private void PlayerControlDetail(playergate playergateCenter)
    {
        if (playergateCenter != null)
        {
            playergateCenter();
        }
        else
            return;
    }
    private void OpenBag()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            Time.timeScale = Time.timeScale == 1 ? 0 : 1;
            myBag.SetActive(!myBag.activeSelf);
        }
    }
    private void OpenMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = Time.timeScale == 1 ? 0 : 1;
            myMenu.SetActive(!myMenu.activeSelf);
        }
    }
    /// <summary>
    /// 跳跃后退
    /// </summary>
    private void JumpOrBack()
    {
        //跳跃
        if (Input.GetButtonDown("Jump"))
        {
            MoveAnimAudio(PlayerAnimation.AnimType.Jump, PlayerAudioCenter.AudioType.Jump);
        }
        //后退
        else if (Input.GetAxis("Vertical") < 0)
        {
            MoveAnimAudio(walk_speed, PlayerAnimation.AnimType.WalkBack, true, PlayerAudioCenter.AudioType.Walk);
        }
    }
    /// <summary>
    /// 奔跑移动
    /// </summary>
    private void RunOrMove()
    {
        //奔跑
        if (Input.GetAxis("Fire3") > 0 && Input.GetAxis("Vertical") > 0)
        {
            MoveAnimAudio(run_speed, PlayerAnimation.AnimType.Speed, 1, PlayerAudioCenter.AudioType.Run);
        }
        //行走
        else if (Input.GetAxis("Vertical") > 0)
        {
            MoveAnimAudio(walk_speed, PlayerAnimation.AnimType.Speed, 0.5f, PlayerAudioCenter.AudioType.Walk);
        }
    }
    /// <summary>
    /// 攻击
    /// </summary>
    private void Fight()
    {
        //更换弹匣
        if (Input.GetKeyDown(KeyCode.R))
        {
            gun.UpdateAmmo();
        }
        //射击
        else if (Input.GetButton("Fire1") && Time.timeScale == 1) 
        {
            MoveAnimAudio(PlayerAnimation.AnimType.Shoot, PlayerAudioCenter.AudioType.Shoot, true); 
        }
        //近战攻击
        else if (Input.GetAxis("Fire2") > 0 && Time.timeScale == 1)
        {
            MoveAnimAudio(PlayerAnimation.AnimType.Fight, PlayerAudioCenter.AudioType.Hit);
        }
    }
    /// <summary>
    /// 左右移动
    /// </summary>
    private void LeftRightMove()
    {
        //左移
        if (Input.GetAxis("Horizontal") < 0)
        {
            MoveAnimAudio(walk_speed, PlayerAnimation.AnimType.Dircetion, 0.5f, PlayerAudioCenter.AudioType.Walk); 
        }
        //右移
        else if (Input.GetAxis("Horizontal") > 0)
        {
            MoveAnimAudio(walk_speed, PlayerAnimation.AnimType.Dircetion, 1, PlayerAudioCenter.AudioType.Walk);
        }
    }
    /// <summary>
    /// 待机
    /// </summary>
    private void Idle()
    {
        //待机
        if (Input.anyKey == false)
        {
            anim.action.PlayAnimation(PlayerAnimation.AnimType.Other, 0);
            anim.action.PlayAnimation(PlayerAnimation.AnimType.Other, false);
        }
    }
    /// <summary>
    /// 跳跃、近战攻击等
    /// </summary>
    /// <param name="animatorType"></param>
    /// <param name="aduioType"></param>
    private void MoveAnimAudio(PlayerAnimation.AnimType animatorType, PlayerAudioCenter.AudioType aduioType)
    {
        anim.action.PlayAnimation(animatorType);
        audios.source.PlayAudioType(aduioType);
    }
    /// <summary>
    /// 射击
    /// </summary>
    /// <param name="animatorType"></param>
    /// <param name="aduioType"></param>
    /// <param name="state"></param>
    private void MoveAnimAudio(PlayerAnimation.AnimType animatorType, PlayerAudioCenter.AudioType aduioType, bool state)
    {
        anim.action.PlayAnimation(animatorType, state);
    }
    /// <summary>
    /// 后退
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
    /// 前进，左右移动、奔跑等
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
