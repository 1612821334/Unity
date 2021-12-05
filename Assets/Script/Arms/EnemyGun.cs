using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// µ–»À«π
/// </summary>
public class EnemyGun : Gun
{
    protected override void Start()
    {
        base.Start();
    }
    public override void Fire()
    {
        base.Fire();
        if(base.currentAmmoBullets<=0)
        {
            base.UpdateAmmo();
        }
    }
}
