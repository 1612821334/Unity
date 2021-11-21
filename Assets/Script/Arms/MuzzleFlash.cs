using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������Ч
/// </summary>
public class MuzzleFlash : MonoBehaviour
{
    public GameObject flashGo;
    private float hideTimer;
    public float displayTime = 0.3f;
    public void DisplayFlash()
    {
        flashGo.SetActive(true);
        hideTimer = Time.time + displayTime;
    }
    private void Update()
    {
        if (flashGo.activeInHierarchy && Time.time >= hideTimer) 
        {
            flashGo.SetActive(false);
        }
        if(Input.GetButton("Fire1"))
        {
            DisplayFlash();
        }
    }
}
