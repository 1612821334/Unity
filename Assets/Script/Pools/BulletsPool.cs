using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 子弹对象池
/// </summary>
public class BulletsPool : MonoBehaviour
{
    public GameObject bulletPerfab;
    public static BulletsPool instance;
    /// <summary>
    /// 初始池子容量
    /// </summary>
    public int pooledAmount = 5;
    /// <summary>
    /// 对象池锁定
    /// </summary>
    public bool lockPoolSize;
    private int currentIndex;
    /// <summary>
    /// 子弹
    /// </summary>
    private List<GameObject> bulletsPool = new List<GameObject>();
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        for (int i = 0; i < pooledAmount; i++) 
        {
            GameObject bullet = Instantiate(bulletPerfab);
            bullet.SetActive(false);
            bulletsPool.Add(bullet);
        }
    }
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < bulletsPool.Count; i++)
        {
            int item = (currentIndex + i) % bulletsPool.Count;
            if(!bulletsPool[item].activeInHierarchy)
            {
                currentIndex = (item + 1) % bulletsPool.Count;
                bulletsPool[item].SetActive(true);
                return bulletsPool[item];
            }
        }
        if(!lockPoolSize)
        {
            GameObject bullet = Instantiate(bulletPerfab);
            bulletsPool.Add(bullet);
            return bullet;
        }
        return null;
    }
}
