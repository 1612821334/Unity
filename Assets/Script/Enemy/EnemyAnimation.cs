using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���˶����࣬���𲥷Ŷ���
/// </summary>
public class EnemyAnimation : MonoBehaviour
{
    /// <summary>
    /// ������ǩ
    /// </summary>
    public enum AnimType
    {
        Walking,Run,ShootsGun,SwordAttack,Idle,Death
    };
    public AnimationAction action;
    private void Awake()
    {
        action = new AnimationAction(GetComponentInChildren<Animator>());
    }
}
