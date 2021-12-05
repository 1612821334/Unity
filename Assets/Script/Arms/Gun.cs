using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 枪，资源暂无枪动画
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class Gun : MonoBehaviour
{
    /// <summary>
    /// 弹匣容量
    /// </summary>
    public int ammoCapacity = 15;
    /// <summary>
    /// 当前弹匣子弹数
    /// </summary>
    public int currentAmmoBullets = 15;
    /// <summary>
    /// 总携带子弹数
    /// </summary>
    public int remainBullets = 90;
    /// <summary>
    /// 枪动画类
    /// </summary>
    private GunAnimation anim;
    /// <summary>
    /// 特效
    /// </summary>
    private MuzzleFlash flashGo;
    /// <summary>
    /// 子弹
    /// </summary>
    public GameObject bulletPrefab;
    /// <summary>
    /// 射击声音
    /// </summary>
    public AudioClip[] audioClips;
    private AudioSource source;
    protected virtual void Start()
    {
        anim = GetComponent<GunAnimation>();
        flashGo = GetComponentInChildren<MuzzleFlash>();
        source = GetComponent<AudioSource>();
    }
    /// <summary>
    /// 射击准备
    /// </summary>
    /// <returns></returns>
    private bool Ready()
    {
        if (currentAmmoBullets <= 0
            //|| anim.action.IsPlaying(GunAnimation.AnimType.Rolading)
            )
        {
            if (currentAmmoBullets == 0)
            {
                //anim.action.PlayAnimation(GunAnimation.AnimType.LackBullet); 
            }
            return false;
        }
        currentAmmoBullets--;
        return true;
    }
    /// <summary>
    /// 射击
    /// </summary>
    public virtual void Fire()
    {
        if (!Ready()) return;
        Instantiate(bulletPrefab, flashGo.transform.position, flashGo.transform.rotation);
        //anim.action.PlayAnimation(GunAnimation.AnimType.Shoot);
        source.PlayOneShot(audioClips[0]);
        flashGo.DisplayFlash();
    }
    /// <summary>
    /// 更换弹匣
    /// </summary>
    public virtual void UpdateAmmo()
    {
        if (remainBullets <= 0 || currentAmmoBullets == ammoCapacity) return;
        currentAmmoBullets = remainBullets >= ammoCapacity ? ammoCapacity : remainBullets;
        remainBullets -= currentAmmoBullets;
        //anim.action.PlayAnimation(GunAnimation.AnimType.Rolading);
    }
}
