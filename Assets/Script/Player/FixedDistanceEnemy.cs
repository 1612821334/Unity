using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 查找固定距离内敌人
/// </summary>
public class FixedDistanceEnemy : MonoBehaviour
{
    private EnemyStatusInfo[] allEnemy;
    private List<int> fixedDistanceEnemyIndex=new List<int>();
    private List<MeshRenderer> allMeshrender=new List<MeshRenderer>();
    [SerializeField]
    [Range(0,100)]
    private int distance;
    private void Start()
    {
        distance = 10;
        allEnemy = Object.FindObjectsOfType<EnemyStatusInfo>();
        for (int i = 0; i < allEnemy.Length; i++)
        {
            allMeshrender.Add(allEnemy[i].GetComponentInChildren<MeshRenderer>());
        }
    }
    private void Update()
    {
        fixedDistanceEnemyIndex= FindEneryByMinDistanceBeRed();
        for (int i = 0; i < fixedDistanceEnemyIndex.Count; i++)
        {
            allMeshrender[fixedDistanceEnemyIndex[i]].material.color = Color.red;
        }
        fixedDistanceEnemyIndex.Clear();
        fixedDistanceEnemyIndex = FindEneryByMinDistanceBeWhite();
        for (int i = 0; i < fixedDistanceEnemyIndex.Count; i++)
        {
            allMeshrender[fixedDistanceEnemyIndex[i]].material.color = Color.white;
        }
    }
    public List<int> FindEneryByMinDistanceBeRed()
    {
        for (int i = 0; i < allEnemy.Length; i++)
        {
            float newDistince = Vector3.Distance(this.transform.position, allEnemy[i].transform.position);
            if (newDistince < distance)
            {
                fixedDistanceEnemyIndex.Add(i);
            }
        }
        return fixedDistanceEnemyIndex;
    }
    public List<int> FindEneryByMinDistanceBeWhite()
    {
        for (int i = 0; i < allEnemy.Length; i++)
        {
            float newDistince = Vector3.Distance(this.transform.position, allEnemy[i].transform.position);
            if (newDistince > distance)
            {
                fixedDistanceEnemyIndex.Add(i);
            }
        }
        return fixedDistanceEnemyIndex;
    }
}

