using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// µ¥·¢Ç¹
/// </summary>
public class SingleGun : Gun
{
    protected override void Start()
    {
        base.Start();
    }
    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            base.Fire();
        }
        else if(Input.GetKeyDown(KeyCode.R))
        {
            base.UpdateAmmo();
        }
    }
}
