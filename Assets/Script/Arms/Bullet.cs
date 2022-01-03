using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 子弹
/// </summary>
public class Bullet : MonoBehaviour
{
    private Vector3 start;           //起始位置
    private Vector3 direction;       //射线方向
    private Vector3 hitPos;          //击中位置
    private float length;            //射线长度
    public float speed = 20;         //子弹速度
    [HideInInspector]
    public float damage;             //子弹伤害
    public LayerMask mask;           //检测层物
    private RaycastHit hit;          //射线结果
    private bool isColider;          //碰撞状态
    private void OnEnable()
    {
        Invoke("DisplayBullet", 3);
    }
    private void Update()
    {
        BulletColiderJuadge();
    }
    /// <summary>
    /// 子弹取消激活
    /// </summary>
    private void DisplayBullet()
    {
        this.gameObject.SetActive(false);
    }
    /// <summary>
    /// 子弹判定击中状态
    /// </summary>
    private void BulletColiderJuadge()
    {
        
        start = transform.position;
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        //transform.Translate(transform.forward * speed * Time.deltaTime, Space.Self);
        direction = transform.position - start;
        length = direction.magnitude;
        isColider = Physics.Raycast(start, direction, out hit, length);
        if (isColider)
        {
            hitPos = hit.point;//击中位置坐标
            //GenerateContactEffect();
            if (hit.collider.tag == "Enemy")
            {
                hit.collider.GetComponent<EnemyStatusInfo>().damage = damage;
            }
            if(hit.collider.tag == "Player")
            {
                hit.collider.GetComponent<PlayerStatusInfo>().damage = damage;
            }
            DisplayBullet();
        }
    }
    /// <summary>
    /// 生成击中特效
    /// </summary>
    private void GenerateContactEffect()
    {
        if (hit.collider == null) return;
        #region
        //[对象池]
        //根据标签加载资源(特效)
        //特效名称规则：存放路径+接触物体标签
        //GameObject prefabGo = Resources.Load<GameObject>("ContactEffects(特效路径)/" + hit.collider.tag);
        //if (prefabGo)
        //    Instantiate(prefabGo, hitPos + hit.normal * 0.01f, Quaternion.LookRotation(hit.normal));
        #endregion
        GameObject prefabGo = EffectPool.instance.GetPooledObject(hit.collider.tag);
        prefabGo.transform.position = hitPos + hit.normal * 0.01f;
        prefabGo.transform.rotation = Quaternion.LookRotation(hit.normal);
    }
}
