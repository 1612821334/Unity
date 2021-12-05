using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 枪动画类,资源暂时没有枪动画
/// </summary>
public class GunAnimationAction : MonoBehaviour
{
    public Animator anim;
    public AnimatorStateInfo animationState;
    public GunAnimationAction(Animator anim)
    {
        this.anim = anim;
    }
    /// <summary>
    /// 播放动画
    /// </summary>
    /// <param name="animatorType"></param>
    public void PlayAnimation(GunAnimation.AnimType animatorType)
    {
        switch (animatorType)
        {
            case GunAnimation.AnimType.Rolading: anim.SetTrigger(""); break;
            case GunAnimation.AnimType.LackBullet: anim.SetTrigger(""); break;
            default: anim.SetTrigger(""); break;
        }
    }
    /// <summary>
    /// 正在播放动画
    /// </summary>
    /// <param name="animatorType"></param>
    /// <returns></returns>
    public bool IsPlaying(GunAnimation.AnimType animatorType)
    {
        animationState = anim.GetCurrentAnimatorStateInfo(0);
        switch (animatorType)
        {
            case GunAnimation.AnimType.Rolading:
                if (animationState.IsName("Shoot"))return true; break;
        }
        return false;
    }
}
