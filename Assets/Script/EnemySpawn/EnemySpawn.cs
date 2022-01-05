using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����������������������Ѱ·
/// </summary>
public class EnemySpawn : MonoBehaviour
{
    public GameObject[] enemyType;                          //��������
    public static EnemySpawn instance;
    public int maxCount = 5;                                //������������
    public int startCount = 2;                              //��ʼ������
    private float createInterval = 2;                       //�������ɼ��ʱ��
    public int spawnedCount;                                //�����ɵ�����
    private SearchFixDistancePlayer playerSearch;           //������ɾ��������
    private PlayerStatusInfo player;                        //������ɾ����������Ϣ
    public WayLine[] linePoints;                            //����·��
    private Transform wayChild;                             //��·�߸��ڵ�
    public delegate void SpawnDelegate(string name);
    public SpawnDelegate spawnDelegates;
    private void Awake()
    {
        instance = this;
        CalculateWayLinePoints();
        spawnDelegates += SpawnDelegateJuadge;
        playerSearch = GetComponent<SearchFixDistancePlayer>();
    }
    private void OnEnable()
    {
        spawnDelegates("StartGenerate");
    }
    private void OnDisable()
    {
        StopCoroutine("StartGenerate");
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
    /// ��������
    /// </summary>
    private void GenerateEnemy()
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
    /// ���ʱ���ӳ����ɵ���
    /// </summary>
    IEnumerator LateCreateEnemy()
    {
        spawnedCount++;
        yield return StartCoroutine("FindPlayer");
        if (player != null)
        {
            print("����");
            if (spawnedCount > maxCount) { }
            else Invoke("GenerateEnemy", Random.Range(1, createInterval));
        }
    }
    /// <summary>
    /// ��ʼ������
    /// </summary>
    IEnumerator StartGenerate()
    {
        while (gameObject.activeInHierarchy)
        {
            if (spawnedCount < startCount)
            {
                StartCoroutine("LateCreateEnemy");
            }
            yield return StartCoroutine("ActivateNextSpawn");
        }
    }
    /// <summary>
    /// ��Ҳ���
    /// </summary>
    /// <returns></returns>
    IEnumerator FindPlayer()
    {
        do
        {
            player = playerSearch.FindPlayerByMinDistance();
            yield return 0;
        } while (player == null);
    }
    /// <summary>
    /// ����������������һ������
    /// </summary>
    IEnumerator ActivateNextSpawn()
    {
        if (spawnedCount > maxCount)
        {
            SpawnSystem.instance.ActivateNextSpawn();
        }
        yield return 0;
    }
    /// <summary>
    /// ����������Э��ί��
    /// </summary>
    /// <param name="name"></param>
    public void SpawnDelegateJuadge(string name)
    {
        StartCoroutine(name);
    }
}
