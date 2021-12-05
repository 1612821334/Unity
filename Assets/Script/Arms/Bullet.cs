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
    public float damage = 100;       //子弹伤害
    public LayerMask mask;           //检测层物
    private RaycastHit hit;          //射线结果
    private bool isColider;          //碰撞状态
    private void Start()
    {
        Destroy(this.gameObject, 3);
    }
    private void Update()
    {
        BulletColider();
    }
    /// <summary>
    /// 子弹判定击中状态
    /// </summary>
    private void BulletColider()
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
            if (hit.collider.tag == "Enemy")
            {
                hit.collider.GetComponent<EnemyStatusInfo>().damage = damage;
            }
            if(hit.collider.tag == "Player")
            {
                hit.collider.GetComponent<PlayerStatusInfo>().damage = damage;
            }
            Destroy(this.gameObject);
        }
    }
}
