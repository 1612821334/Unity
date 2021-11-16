using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人马达：负责移动，旋转，寻路
/// </summary>
public class EnemyMotor : MonoBehaviour
{
    public float speed = 1;          //移动速度
    public WayLine poinits;          //移动路线
    public float backDistance;       //回归距离
    public float trackDistance;      //追踪距离
    public int currentPointIndex;    //当前点位置
    public Transform playerPoint;    //玩家位置
    public MeshRenderer mesh;
    public CharacterController enemyConl;
    public EnemyMotor(WayLine value)
    {
        poinits = value;
    }
    /// <summary>
    /// 向前移动
    /// </summary>
    public void MoveMentForward()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    /// <summary>
    /// 注视目标点旋转
    /// </summary>
    /// <param name="targetPoinit"></param>
    public  void LookRotation(Vector3 targetPoinit)
    {
        transform.LookAt(targetPoinit);
    }
    /// <summary>
    /// 路点寻路
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
    /// 向玩家移动
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
