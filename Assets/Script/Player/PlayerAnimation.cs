using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��Ҷ����࣬���𲥷Ŷ���
/// </summary>
public class PlayerAnimation : MonoBehaviour
{
    /// <summary>
    /// ������ǩ
    /// </summary>
    public enum AnimType
    {
        Walking, WalkBack, Run, Shoot, Jump, Idle, Death, Fight, Dircetion, Speed, Other
    };
    public PlayerAnimationAction action;
    private void Awake()
    {
        action = new PlayerAnimationAction(GetComponent<Animator>());
    }
}
