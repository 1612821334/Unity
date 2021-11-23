using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武器特效
/// </summary>
public class MuzzleFlash : MonoBehaviour
{
    public GameObject flashGo;          //特效
    public GameObject bullet;           //子弹
    private GameObject bullets;         //生成子弹
    private float hideTimer;            //隐藏时间
    public float goTime;                //游戏运行时间
    public float nextTime = 0.3f;       //子弹生成间隔
    public float displayTime = 0.3f;    //特效激活间隔
    /// <summary>
    /// 特效激活
    /// </summary>
    public void DisplayFlash()
    {
        flashGo.SetActive(true);
        hideTimer = goTime + displayTime;
    }
    /// <summary>
    /// 子弹生成
    /// </summary>
    public void CreateBullet()
    {
        if (goTime >= nextTime)
        {
            bullets=Instantiate(bullet, flashGo.transform.position, Quaternion.identity);
            bullets.transform.localRotation = flashGo.transform.rotation;
            nextTime = goTime + displayTime;
        }
    }
    private void Update()
    {
        goTime = Time.time;
        if (flashGo.activeInHierarchy && goTime >= hideTimer) 
        {
            flashGo.SetActive(false);
        }
        if(Input.GetButton("Fire1"))
        {
            DisplayFlash();
            CreateBullet();
        }
    }
}
