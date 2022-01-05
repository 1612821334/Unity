using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人生成器系统
/// </summary>
public class SpawnSystem : MonoBehaviour
{
    public static SpawnSystem instance;
    /// <summary>
    /// 生成器
    /// </summary>
    private List<Transform> spawns = new List<Transform>();
    private void Awake()
    {
        instance = this;
    }
    /// <summary>
    /// 初始化生成器
    /// </summary>
    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            spawns.Add(transform.GetChild(i));
            spawns[i].gameObject.SetActive(false);
        }
        spawns[0].gameObject.SetActive(true);
    }
    /// <summary>
    /// 激活下一个生成器
    /// </summary>
    public void ActivateNextSpawn()
    {
        for(int i=0;i< spawns.Count;i++)
        {
            if(spawns[i].gameObject.activeInHierarchy)
            {
                spawns[i].gameObject.SetActive(false);
                int index = i < transform.childCount-1 ? i+1 : 0;
                spawns[index].gameObject.SetActive(true);
                return;
            }
        }
    }
}
