using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ????ǹ
/// </summary>
public class EnemyGun : Gun
{
    protected override void Start()
    {
        base.Start();
    }
    protected override void OnDestroy()
    {

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
