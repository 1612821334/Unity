using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家马达：负责移动，旋转，寻路
/// </summary>
public class PlayerMotor : MonoBehaviour
{
    public float speed = 0.5f;       //移动速度
    public CharacterController playerConl;
    /// <summary>
    /// 向前移动
    /// </summary>
    public void MoveMentForward()
    {
        playerConl.SimpleMove(transform.forward * speed);
        //transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    public void MoveMentForward(float speedState)
    {
        playerConl.SimpleMove(transform.forward * speedState);
        //transform.Translate(Vector3.forward * speedState * Time.deltaTime);
    }
    /// <summary>
    /// 向左移动
    /// </summary>
    /// <param name="speedState"></param>
    public void MoveMentLeft(float speedState)
    {
        playerConl.SimpleMove(transform.right * -speedState);
        //transform.Translate(Vector3.left * speedState * Time.deltaTime);
    }
    /// <summary>
    /// 向右移动
    /// </summary>
    /// <param name="speedState"></param>
    public void MoveMentRight(float speedState)
    {
        playerConl.SimpleMove(transform.right * speedState);
        //transform.Translate(Vector3.right * speedState * Time.deltaTime);
    }
    /// <summary>
    /// 向后移动
    /// </summary>
    /// <param name="speedState"></param>
    public void MoveMentBack(float speedState)
    {
        playerConl.SimpleMove(transform.forward * -speedState);
        //transform.Translate(Vector3.back * speedState * Time.deltaTime);
    }
}
