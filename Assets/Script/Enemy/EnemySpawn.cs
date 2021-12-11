using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����������������������Ѱ·
/// </summary>
public class EnemySpawn : MonoBehaviour
{
    public GameObject[] enemyType;                          //��������
    public int maxCount = 5;                                //������������
    public int startCount;                                  //���ϵ�����
    private float createInterval = 2;                       //�������ɼ��ʱ��
    public static int spawnedCount;                         //�����ɵ�����
    private SearchFixDistancePlayer playerSearch;           //������ɾ��������
    private PlayerStatusInfo player;                        //������ɾ����������Ϣ
    public WayLine[] linePoints;                            //����·��
    private Transform wayChild;                             //��·�߸��ڵ�
    public delegate void SpawnDelegate();
    public static SpawnDelegate SpawnDelegates;
    private void Awake()
    {
        SpawnDelegates += LateCreateEnemy;
        playerSearch = GetComponent<SearchFixDistancePlayer>();
        CalculateWayLinePoints();
    }
    /// <summary>
    /// ·�߻�ȡ
    /// </summary>
    private void CalculateWayLinePoints()
    {
        linePoints = new WayLine[this.transform.childCount];
        for (int i = 0; i < linePoints.Length; i++)
        {
            //ÿһ��·�߸��ڵ�
            wayChild = this.transform.GetChild(i);
            //����·�߶���
            linePoints[i] = new WayLine(wayChild.childCount);
            //��ȡ���ڵ��������ӽڵ�·��
            for (int pointIndex = 0; pointIndex < wayChild.childCount; pointIndex++)
            {
                linePoints[i].WayPoints[pointIndex] = wayChild.GetChild(pointIndex).position;
            }
        }
    }
    /// <summary>
    /// ��ȡ���п���·��
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
    /// ���ʱ���ӳ����ɵ���
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
    /// ��������
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
    }
    /// <summary>
    /// ���������ʼ���ɵ�����
    /// </summary>
    private void Update()
    {
        if (spawnedCount < startCount)
        {
            LateCreateEnemy();
        }   
    }
}
