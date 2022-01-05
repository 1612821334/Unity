using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����������ϵͳ
/// </summary>
public class SpawnSystem : MonoBehaviour
{
    public static SpawnSystem instance;
    /// <summary>
    /// ������
    /// </summary>
    private List<Transform> spawns = new List<Transform>();
    private void Awake()
    {
        instance = this;
    }
    /// <summary>
    /// ��ʼ��������
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
    /// ������һ��������
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
