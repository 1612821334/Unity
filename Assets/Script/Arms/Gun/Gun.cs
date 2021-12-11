using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ǹ����Դ����ǹ����
/// </summary>
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(BulletsPool))]
public class Gun : MonoBehaviour
{
    /// <summary>
    /// ��ϻ����
    /// </summary>
    public int ammoCapacity = 15;
    /// <summary>
    /// ��ǰ��ϻ�ӵ���
    /// </summary>
    public int currentAmmoBullets = 15;
    /// <summary>
    /// ��Я���ӵ���
    /// </summary>
    public int remainBullets = 90;
    /// <summary>
    /// ǹ������
    /// </summary>
    private GunAnimation anim;
    /// <summary>
    /// ��Ч
    /// </summary>
    private MuzzleFlash flashGo;
    /// <summary>
    /// �ӵ�
    /// </summary>
    public GameObject bulletPrefab;
    /// <summary>
    /// �������
    /// </summary>
    public AudioClip[] audioClips;
    /// <summary>
    /// ������
    /// </summary>
    public float atk = 100;
    private AudioSource source;
    private GameObject bullet;
    private BulletsPool bulletPool;
    protected virtual void Start()
    {
        anim = GetComponent<GunAnimation>();
        bulletPool = GetComponent<BulletsPool>();
        flashGo = GetComponentInChildren<MuzzleFlash>();
        source = GetComponent<AudioSource>();
    }
    /// <summary>
    /// ���׼��
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
    /// ���
    /// </summary>
    public virtual void Fire()
    {
        if (!Ready()) return;
        bullet = bulletPool.GetPooledObject();
        //GameObject BulletGo = Instantiate(bulletPrefab, flashGo.transform.position, flashGo.transform.rotation);
        bullet.transform.position = flashGo.transform.position;
        bullet.transform.rotation = flashGo.transform.rotation;
        bullet.GetComponent<Bullet>().damage = atk;
        //anim.action.PlayAnimation(GunAnimation.AnimType.Shoot);
        source.PlayOneShot(audioClips[0]);
        flashGo.DisplayFlash();
    }
    /// <summary>
    /// ������ϻ
    /// </summary>
    public virtual void UpdateAmmo()
    {
        if (remainBullets <= 0 || currentAmmoBullets == ammoCapacity) return;
        currentAmmoBullets = remainBullets >= ammoCapacity ? ammoCapacity : remainBullets;
        remainBullets -= currentAmmoBullets;
        //anim.action.PlayAnimation(GunAnimation.AnimType.Rolading);
    }
}
