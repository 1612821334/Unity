using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ӵ�
/// </summary>
public class Bullet : MonoBehaviour
{
    private Vector3 start;           //��ʼλ��
    private Vector3 direction;       //���߷���
    private Vector3 hitPos;          //����λ��
    private float length;            //���߳���
    public float speed = 20;         //�ӵ��ٶ�
    public float damage = 100;       //�ӵ��˺�
    public LayerMask mask;           //������
    private RaycastHit hit;          //���߽��
    private bool isColider;          //��ײ״̬
    private void Start()
    {
        Destroy(this.gameObject, 3);
    }
    private void Update()
    {
        BulletColider();
    }
    /// <summary>
    /// �ӵ��ж�����״̬
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
            hitPos = hit.point;//����λ������
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
