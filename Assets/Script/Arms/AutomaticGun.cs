using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����ǹ
/// </summary>
public class AutomaticGun : Gun
{
    /// <summary>
    /// �´�����ʱ��
    /// </summary>
    private float nextShoot;
    /// <summary>
    /// �ӵ����ɼ��
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
