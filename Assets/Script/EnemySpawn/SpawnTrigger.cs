using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 生成触发
/// </summary>
public class SpawnTrigger : MonoBehaviour
{
    /// <summary>
    /// 父物体生成器
    /// </summary>
    public GameObject Spawn;
    private void OnEnable()
    {
        Spawn = GameObject.FindWithTag("Spawn");
    }
    /// <summary>
    /// 接触触发
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            //生成器具体生成节点激活
            Spawn.transform.GetChild(0).gameObject.SetActive(true);
            transform.gameObject.SetActive(false);
        }
    }
}
