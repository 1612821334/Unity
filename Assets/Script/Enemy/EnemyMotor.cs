using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����������ƶ�����ת��Ѱ·
/// </summary>
public class EnemyMotor : MonoBehaviour
{
    public float speed = 1;          //�ƶ��ٶ�
    public WayLine poinits;          //�ƶ�·��
    public float backDistance;       //�ع����
    public float trackDistance;      //׷�پ���
    public int currentPointIndex;    //��ǰ��λ��
    public Transform playerPoint;    //���λ��
    public MeshRenderer mesh;
    public CharacterController enemyConl;
    public EnemyMotor(WayLine value)
    {
        poinits = value;
    }
    /// <summary>
    /// ��ǰ�ƶ�
    /// </summary>
    public void MoveMentForward()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    /// <summary>
    /// ע��Ŀ�����ת
    /// </summary>
    /// <param name="targetPoinit"></param>
    public  void LookRotation(Vector3 targetPoinit)
    {
        transform.LookAt(targetPoinit);
    }
    /// <summary>
    /// ·��Ѱ·
    /// </summary>
    /// <returns></returns>
    public bool Pathfinding()
    {
        if (poinits.WayPoints == null || currentPointIndex >= poinits.WayPoints.Length)
        {
            return false;
        }
        LookRotation(poinits.WayPoints[currentPointIndex]);
        MoveMentForward();
        if (Vector3.Distance(transform.position, poinits.WayPoints[currentPointIndex]) < 0.1f) 
        {
            currentPointIndex++;
        }
        return true; 
    }
    /// <summary>
    /// ������ƶ�
    /// </summary>
    /// <returns></returns>
    public EnemyAi.State MoveToPlyer()
    {
        if (currentPointIndex >= poinits.WayPoints.Length) currentPointIndex--;
        if (Vector3.Distance(transform.position, poinits.WayPoints[currentPointIndex]) >= backDistance) 
        {
            LookRotation(playerPoint.position);
            mesh.material.color = Color.white;
            return EnemyAi.State.Shooting;
        }
        else if (Vector3.Distance(transform.position, playerPoint.position) >= trackDistance)
        {
            LookRotation(playerPoint.position);
            MoveMentForward();
            mesh.material.color = Color.red;
            return EnemyAi.State.PathToPlayer;
        }
        else
        {
            return EnemyAi.State.FightAttack;
        }
    }
}
