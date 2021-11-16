using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����������ƶ�����ת��Ѱ·
/// </summary>
public class PlayerMotor : MonoBehaviour
{
    public float speed = 0.5f;       //�ƶ��ٶ�
    public CharacterController playerConl;
    /// <summary>
    /// ��ǰ�ƶ�
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
    /// �����ƶ�
    /// </summary>
    /// <param name="speedState"></param>
    public void MoveMentLeft(float speedState)
    {
        playerConl.SimpleMove(transform.right * -speedState);
        //transform.Translate(Vector3.left * speedState * Time.deltaTime);
    }
    /// <summary>
    /// �����ƶ�
    /// </summary>
    /// <param name="speedState"></param>
    public void MoveMentRight(float speedState)
    {
        playerConl.SimpleMove(transform.right * speedState);
        //transform.Translate(Vector3.right * speedState * Time.deltaTime);
    }
    /// <summary>
    /// ����ƶ�
    /// </summary>
    /// <param name="speedState"></param>
    public void MoveMentBack(float speedState)
    {
        playerConl.SimpleMove(transform.forward * -speedState);
        //transform.Translate(Vector3.back * speedState * Time.deltaTime);
    }
}
