using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单列池
/// </summary>
public class SimplePool : MonoBehaviour
{
    public GameObject[] GameObjectPerfab;
    /// <summary>
    /// 初始池子容量
    /// </summary>
    public int pooledAmount = 5;
    /// <summary>
    /// 对象池锁定
    /// </summary>
    public bool lockPoolSize;
    protected int currentIndex;
    [HideInInspector]
    /// <summary>
    /// 对象序列
    /// </summary>
    public static List<GameObject> GameObjectsPool = new List<GameObject>();
    /// <summary>
    /// 初始化对象池
    /// </summary>
    protected virtual void Start()
    {
        for (int i = 0; i < pooledAmount; i++)
        {
            int randIndex = GameObjectPerfab.Length > 1 ? Random.Range(0, GameObjectPerfab.Length) : 0;
            GameObject obj = Instantiate(GameObjectPerfab[randIndex]);
            obj.SetActive(false);
            GameObjectsPool.Add(obj);
        }
    }
    /// <summary>
    /// 获取有效池物
    /// </summary>
    /// <returns></returns>
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < GameObjectsPool.Count; i++)
        {
            int item = (currentIndex + i) % GameObjectsPool.Count;
            if (!GameObjectsPool[item].activeInHierarchy)
            {
                currentIndex = (item + 1) % GameObjectsPool.Count;
                GameObjectsPool[item].SetActive(true);
                return GameObjectsPool[item];
            }
        }
        if (!lockPoolSize)
        {
            int randIndex = GameObjectPerfab.Length > 1 ? Random.Range(0, GameObjectPerfab.Length) : 0;
            GameObject obj = Instantiate(GameObjectPerfab[randIndex]);
            GameObjectsPool.Add(obj);
            return obj;
        }
        return null;
    }
}
