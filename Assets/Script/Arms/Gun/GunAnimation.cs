using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Œ‰∆˜∂Øª≠
/// </summary>
public class GunAnimation : MonoBehaviour
{
    /// <summary>
    /// ∂Øª≠±Í«©
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
