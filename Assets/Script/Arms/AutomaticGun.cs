using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 连发枪
/// </summary>
public class AutomaticGun : Gun
{
    /// <summary>
    /// 下次生成时间
    /// </summary>
    private float nextShoot;
    /// <summary>
    /// 子弹生成间隔
    /// </summary>
    public float nextTime = 0.3f;
    protected override void Start()
    {
        base.Start();
    }
    public override void Fire()
    {
        if (Time.time >= nextShoot)
        {
            base.Fire();
            nextShoot = Time.time + nextTime;
        }
    }
    public override void UpdateAmmo()
    {
        base.UpdateAmmo();
    }
    #region
    //private void Update()
    //{
    //    if(Input.GetButton("Fire1"))
    //    {
    //        if(Time.time>=nextShoot)
    //        {
    //            base.Fire();
    //            nextShoot = Time.time + nextTime;
    //        }
    //    }
    //    else if(Input.GetKeyDown(KeyCode.R))
    //    {
    //        base.UpdateAmmo();
    //    }
    //}
    #endregion
}
