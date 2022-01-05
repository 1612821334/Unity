using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������Ч
/// </summary>
public class MuzzleFlash : MonoBehaviour
{
    public GameObject flashGo;          //��Ч
    private float hideTimer;            //����ʱ��
    private float goTime;                //��Ϸ����ʱ��
    public float displayTime = 0.3f;    //��Ч������
    /// <summary>
    /// ��Ч����
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
