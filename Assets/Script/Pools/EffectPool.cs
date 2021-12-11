using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 特效对象池
/// </summary>
public class EffectPool : MonoBehaviour
{
    public static EffectPool instance;
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
    /// 特效
    /// </summary>
    private List<GameObject> peopleEffectPool = new List<GameObject>();
    private List<GameObject> environmentEffectPool = new List<GameObject>();
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject peopleEffect = Resources.Load<GameObject>("ContactEffects/People");
            GameObject environmentEffect = Resources.Load<GameObject>("ContactEffects/Environment");
            peopleEffect.SetActive(false);
            environmentEffect.SetActive(false);
            peopleEffectPool.Add(peopleEffect);
            environmentEffectPool.Add(environmentEffect);
        }
    }
    public GameObject GetPooledObject(string type)
    {
        switch (type)
        {
            case "People": return (GetPooledEffect(peopleEffectPool, type)); 
            case "Environment": return (GetPooledEffect(environmentEffectPool, type)); 
        }
        return null;
    }
    private GameObject GetPooledEffect(List<GameObject> objects, string type)
    {
        for (int i = 0; i < objects.Count; i++)
        {
            int item = (currentIndex + i) % objects.Count;
            if (!objects[item].activeInHierarchy)
            {
                currentIndex = (item + 1) % objects.Count;
                objects[item].SetActive(true);
                return objects[item];
            }
        }
        if (!lockPoolSize)
        {
            GameObject Effect = Resources.Load<GameObject>("ContactEffects/" + type);
            objects.Add(Effect);
            return Effect;
        }
        return null;
    }
}
