using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������Ϊ�࣬���𶯻����岥��
/// </summary>
public class PlayerAnimationAction
{
    public Animator anim;
    public AnimatorStateInfo animationState;//����������״̬��Ϣ
    private float animTime = 0.75f;         //����ִ��ʱ��
    public PlayerAnimationAction(Animator anim)
    {
        this.anim = anim;
    }
    /// <summary>
    /// ���Ŷ���
    /// </summary>
    /// <param name="animatorType"></param>
    #region
    public void PlayAnimation(PlayerAnimation.AnimType animatorType)
    {
        animationState = anim.GetCurrentAnimatorStateInfo(0);
        switch (animatorType)
        {
            case PlayerAnimation.AnimType.Walking: anim.SetTrigger("Walking"); break;
            case PlayerAnimation.AnimType.Run: anim.SetTrigger("Run"); break;
            case PlayerAnimation.AnimType.Fight: anim.SetTrigger("Fight"); break;
            case PlayerAnimation.AnimType.Jump:
                if (anim != null)
                { anim.Play("jump", 0, 0.25f); }
                else
                    anim.SetTrigger("Jump"); break;
            case PlayerAnimation.AnimType.Shoot: anim.SetTrigger("Shoot"); break;
            case PlayerAnimation.AnimType.Death:
                if (anim != null && animationState.IsName("death") == false) 
                {
                    anim.Play("death",0);
                }
                else
                    anim.SetTrigger("Death"); break;
            default: anim.SetTrigger("Idle"); break;
        }
    }
    public void PlayAnimation(PlayerAnimation.AnimType animatorType, float speed)
    {
        animationState = anim.GetCurrentAnimatorStateInfo(0);
        if (animationState.IsName("shootGun"))
        {
            anim.SetBool("Shoot", false);
        }
        switch (animatorType)
        {
            case PlayerAnimation.AnimType.Speed: anim.SetFloat("Speed", speed); break;
            case PlayerAnimation.AnimType.Dircetion: anim.SetFloat("Dircetion", speed); break;
            default: anim.SetFloat("Speed", 0); anim.SetFloat("Dircetion", 0); break;
        }
    }
    public void PlayAnimation(PlayerAnimation.AnimType animatorType, bool state)
    {
        switch (animatorType)
        {
            case PlayerAnimation.AnimType.Shoot: anim.SetBool("Shoot", state); break;
            case PlayerAnimation.AnimType.WalkBack: anim.SetBool("WalkBack", state); break;
            default:anim.SetBool("Shoot", false);anim.SetBool("WalkBack", false);break;
        }
    }
    /// <summary>
    /// ���ڲ��Ŷ���
    /// </summary>
    /// <param name="animatorType"></param>
    /// <returns></returns>
    public bool IsPlaying(PlayerAnimation.AnimType animatorType)
    {
        animationState = anim.GetCurrentAnimatorStateInfo(0);
        switch (animatorType)
        {
            case PlayerAnimation.AnimType.Shoot:
                if (animationState.IsName("shootGun")|| animationState.IsName("shoot")) return true; break;
        }
        return false;
    }
    /// <summary>
    /// �ж϶����Ƿ񲥷����
    /// </summary>
    /// <param name="animatorType"></param>
    /// <returns></returns>
    public bool IsPlay(PlayerAnimation.AnimType animatorType)
    {
        animationState = anim.GetCurrentAnimatorStateInfo(0);
        switch (animatorType)
        {
            case PlayerAnimation.AnimType.Shoot:
                if (animationState.IsName("shoot") && animationState.normalizedTime >= animTime)
                    return true;break;
            case PlayerAnimation.AnimType.Death:
                if (animationState.IsName("death") && animationState.normalizedTime >= animTime)
                    return true; break;
        }
        return false;
    }
    #endregion
}
