using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������Ч
/// </summary>
public class MuzzleFlash : MonoBehaviour
{
    public GameObject flashGo;          //��Ч
    public GameObject bullet;           //�ӵ�
    private GameObject bullets;         //�����ӵ�
    private float hideTimer;            //����ʱ��
    public float goTime;                //��Ϸ����ʱ��
    public float nextTime = 0.3f;       //�ӵ����ɼ��
    public float displayTime = 0.3f;    //��Ч������
    /// <summary>
    /// ��Ч����
    /// </summary>
    public void DisplayFlash()
    {
        flashGo.SetActive(true);
        hideTimer = goTime + displayTime;
    }
    /// <summary>
    /// �ӵ�����
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
