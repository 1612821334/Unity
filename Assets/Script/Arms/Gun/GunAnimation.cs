using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������
/// </summary>
public class GunAnimation : MonoBehaviour
{
    /// <summary>
    /// ������ǩ
    /// </summary>
    public enum AnimType
    {
        Rolading,LackBullet,Shoot
    };
    public GunAnimationAction action;
    private void Awake()
    {
        action = new GunAnimationAction(GetComponent<Animator>());
    }
}
