using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武器特效
/// </summary>
public class MuzzleFlash : MonoBehaviour
{
    public GameObject flashGo;          //特效
    private float hideTimer;            //隐藏时间
    private float goTime;                //游戏运行时间
    public float displayTime = 0.3f;    //特效激活间隔
    /// <summary>
    /// 特效激活
    /// </summary>
    public void DisplayFlash()
    {
        flashGo.SetActive(true);
        hideTimer = goTime + displayTime;
    }
    private void Update()
    {
        goTime = Time.time;
        if (flashGo.activeInHierarchy && goTime >= hideTimer) 
        {
            flashGo.SetActive(false);
        }
    }
}
