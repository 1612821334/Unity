using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������Ϊ�࣬���𶯻����岥��
/// </summary>
public class AnimationAction
{
    public Animator anim;                   //����������
    public AnimatorStateInfo animationState;//����������״̬��Ϣ
    public float animTime = 0.75f;         //����ִ��ʱ��
    public AnimationAction(Animator anim)
    {
        this.anim = anim;
    }
    /// <summary>
    /// ���Ŷ���
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
    /// ��ǰ�����Ƿ񲥷����
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
