using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人生成器，负责敌人生成寻路
/// </summary>
public class EnemySpawn : MonoBehaviour
{
    public GameObject[] enemyType;                          //敌人类型
    public int maxCount;                                    //最大敌人生成数
    public int startCount;                                  //场上敌人数
    private float createInterval;                           //敌人生成间隔时间
    public static int spawnedCount;                         //已生成敌人数
    private SearchFixDistancePlayer playerSearch;           //最近生成距离内玩家
    private PlayerStatusInfo player;                        //最近生成距离内玩家信息
    public WayLine[] linePoints;                            //所有路线
    private Transform wayChild;                             //子路线父节点
    private void Awake()
    {
        maxCount = 5;
        startCount = 2;
        createInterval = 2;
        playerSearch = GetComponent<SearchFixDistancePlayer>();
        CalculateWayLinePoints();
    }
    /// <summary>
    /// 路线获取
    /// </summary>
    private void CalculateWayLinePoints()
    {
        linePoints = new WayLine[this.transform.childCount];
        for (int i = 0; i < linePoints.Length; i++)
        {
            //每一条路线父节点
            wayChild = this.transform.GetChild(i);
            //创建路线对象
            linePoints[i] = new WayLine(wayChild.childCount);
            //获取父节点下所有子节点路标
            for (int pointIndex = 0; pointIndex < wayChild.childCount; pointIndex++)
            {
                linePoints[i].WayPoints[pointIndex] = wayChild.GetChild(pointIndex).position;
            }
        }
    }
    /// <summary>
    /// 获取所有可用路线
    /// </summary>
    /// <returns></returns>
    private WayLine[] SelectUsableWayLine()
    {
        List<WayLine> result = new List<WayLine>(linePoints.Length);
        foreach (var item in linePoints)
        {
            if (item.IsUsable) result.Add(item);
        }
        return result.ToArray();
    }
    /// <summary>
    /// 随机时间延迟生成敌人
    /// </summary>
    public void LateCreateEnemy()
    {
        player = playerSearch.FindPlayerByMinDistance();
        if (player != null)
        {
            spawnedCount++;
            if (spawnedCount > maxCount) return;
            Invoke("GenerateEnemy", Random.Range(1, createInterval));
        }
    }
    /// <summary>
    /// 敌人生成
    /// </summary>
    public void GenerateEnemy()
    {
        WayLine[] usableWayLines = SelectUsableWayLine();
        WayLine line = usableWayLines[Random.Range(0, usableWayLines.Length)];
        int randIndex = Random.Range(0, enemyType.Length);
        GameObject enemy =
            Instantiate(enemyType[randIndex], line.WayPoints[0], Quaternion.identity);
        EnemyMotor enemyMotor = enemy.GetComponent<EnemyMotor>();
        line.IsUsable = false;
        enemyMotor.poinits = line;
        enemyMotor.playerPoint = player.transform;
        enemy.GetComponent<EnemyStatusInfo>().spawn = this;
    }
    /// <summary>
    /// 满足距离后初始生成敌人数
    /// </summary>
    private void Update()
    {
        if (spawnedCount < startCount)
        {
            LateCreateEnemy();
        }   
    }
}
