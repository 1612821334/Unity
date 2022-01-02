using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���г�
/// </summary>
public class SimplePool : MonoBehaviour
{
    public GameObject[] GameObjectPerfab;
    /// <summary>
    /// ��ʼ��������
    /// </summary>
    public int pooledAmount = 5;
    /// <summary>
    /// ���������
    /// </summary>
    public bool lockPoolSize;
    protected int currentIndex;
    [HideInInspector]
    /// <summary>
    /// ��������
    /// </summary>
    public static List<GameObject> GameObjectsPool = new List<GameObject>();
    /// <summary>
    /// ��ʼ�������
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
    /// ��ȡ��Ч����
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
