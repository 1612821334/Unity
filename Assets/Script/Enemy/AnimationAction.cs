using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 动画行为类，负责动画具体播放
/// </summary>
public class AnimationAction
{
    public Animator anim;                   //动画控制器
    public AnimatorStateInfo animationState;//动画控制器状态信息
    public float animTime = 0.75f;         //动画执行时间
    public AnimationAction(Animator anim)
    {
        this.anim = anim;
    }
    /// <summary>
    /// 播放动画
    /// </summary>
    /// <param name="animatorType"></param>
    public void Play(EnemyAnimation.AnimType animatorType)
    {
        animationState = anim.GetCurrentAnimatorStateInfo(0);
        switch (animatorType)
        {
            case EnemyAnimation.AnimType.Walking: anim.SetTrigger("Walking");break;
            case EnemyAnimation.AnimType.Run:
                if (anim != null && (animationState.IsName("Run") == false))
                    anim.Play("Run", 0);
                else anim.SetTrigger("Run"); break;
            case EnemyAnimation.AnimType.ShootsGun: anim.SetTrigger("ShootsGun"); break;
            case EnemyAnimation.AnimType.SwordAttack: anim.SetTrigger("SwordAttack"); break;
            case EnemyAnimation.AnimType.Death:
                if (anim != null && animationState.IsName("death") == false)
                {
                    anim.Play("Death", 0);
                }
                else
                    anim.SetTrigger("Death"); break;
            default:anim.SetTrigger("Idle"); break;
        }
    }
    /// <summary>
    /// 当前动画是否播放完成
    /// </summary>
    /// <param name="animatorType"></param>
    /// <returns></returns>
    public bool IsPlay(EnemyAnimation.AnimType animatorType)
    {
        animationState = anim.GetCurrentAnimatorStateInfo(0);
        switch (animatorType)
        {
            case EnemyAnimation.AnimType.ShootsGun:
                if (animationState.IsName("ShootsGun") && animationState.normalizedTime >= animTime)
                    return true;
                    break;
            case EnemyAnimation.AnimType.SwordAttack:
                if (animationState.IsName("SwordAttack") && animationState.normalizedTime >= animTime)
                    return true; 
                break;
            case EnemyAnimation.AnimType.Death:
                if (animationState.IsName("Death") && animationState.normalizedTime >= animTime)
                    return true;
                break;
            case EnemyAnimation.AnimType.Idle:
                if (animationState.IsName("Idle") && animationState.normalizedTime >= animTime)
                    return true;
                break;
        }
        return false;
    }
}
