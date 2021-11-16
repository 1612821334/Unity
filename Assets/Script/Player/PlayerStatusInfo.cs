using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class PlayerStatusInfo : MonoBehaviour
{
    [SerializeField]
    [Range(1, 100)]
    private float hp = 100;
    public float damage;
    public void Damage()
    {
        hp -= damage;
        if(hp<=0)
        {
            Death();
        }
    }
    public void Death()
    {
        return;
    }
}
