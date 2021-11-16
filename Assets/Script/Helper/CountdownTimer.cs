using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class CountdownTimer : MonoBehaviour
{
    private double totalTime;
    public int secound=120;
    private Text txtTimer;
    private float nextTime=1;
    private void Start()
    {
        //寻找组件的语句最好放到start或者awake初始化阶段中
        txtTimer = GetComponent<Text>();
        ///重复调用("方法名"，第一次执行时间，每次执行间隔)，该语句只在加载脚本是执行一次
        InvokeRepeating("Timer3", 1, 1);
    }
    private void Update()
    {
        //Timer1();
        //Timer2();
    }
    /// <summary>
    /// 累加每帧间隔时间计时器，Time.deltaTime
    /// 适用于类似走走停停先等待间隔，再执行
    /// </summary>
    private void Timer1()
    {
        totalTime += Time.deltaTime;
        if (totalTime >= 1)
        {
            totalTime = 0;
            secound--;
            txtTimer.text = string.Format("{0:d2}:{1:d2}", secound / 60, secound % 60);
            if (secound >= 1 && secound <= 10)
            {
                txtTimer.color = Color.red;
            }
            else
            {
                Time.timeScale = 0;
            }
        }
    }
    /// <summary>
    /// 下一次修改时间计时器,Time.time
    /// 适用于类似发射子弹，先执行在按照间隔执行
    /// </summary>
    private void Timer2()
    {
        if (Time.time >= nextTime)
        {
            secound--;
            txtTimer.text = string.Format("{0:d2}:{1:d2}", secound / 60, secound % 60);
            nextTime = Time.time + 1;
            if (secound >= 1 && secound <= 10)
            {
                txtTimer.color = Color.red;
            }
            else
            {
                Time.timeScale = 0;
            }
        }
    }
    /// <summary>
    /// InvokeRepeating，适用于固定间隔，重复执行
    /// </summary>
    private void Timer3()
    {
        secound--;
        txtTimer.text = string.Format("{0:d2}:{1:d2}", secound / 60, secound % 60);
        if (secound < 1)
        {
            CancelInvoke("Timer3");
        }
    }
}