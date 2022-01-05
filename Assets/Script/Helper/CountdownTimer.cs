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
        //Ѱ������������÷ŵ�start����awake��ʼ���׶���
        txtTimer = GetComponent<Text>();
        ///�ظ�����("������"����һ��ִ��ʱ�䣬ÿ��ִ�м��)�������ֻ�ڼ��ؽű���ִ��һ��
        InvokeRepeating("Timer3", 1, 1);
    }
    private void Update()
    {
        //Timer1();
        //Timer2();
    }
    /// <summary>
    /// �ۼ�ÿ֡���ʱ���ʱ����Time.deltaTime
    /// ��������������ͣͣ�ȵȴ��������ִ��
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
    /// ��һ���޸�ʱ���ʱ��,Time.time
    /// ���������Ʒ����ӵ�����ִ���ڰ��ռ��ִ��
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
    /// InvokeRepeating�������ڹ̶�������ظ�ִ��
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