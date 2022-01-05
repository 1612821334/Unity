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
    [HideInInspector]
    public float damage;             //�ӵ��˺�
    public LayerMask mask;           //������
    private RaycastHit hit;          //���߽��
    private bool isColider;          //��ײ״̬
    private void OnEnable()
    {
        Invoke("DisplayBullet", 3);
    }
    private void Update()
    {
        BulletColiderJuadge();
    }
    /// <summary>
    /// �ӵ�ȡ������
    /// </summary>
    private void DisplayBullet()
    {
        this.gameObject.SetActive(false);
    }
    /// <summary>
    /// �ӵ��ж�����״̬
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
            hitPos = hit.point;//����λ������
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
    /// ���ɻ�����Ч
    /// </summary>
    private void GenerateContactEffect()
    {
        if (hit.collider == null) return;
        #region
        //[�����]
        //���ݱ�ǩ������Դ(��Ч)
        //��Ч���ƹ��򣺴��·��+�Ӵ������ǩ
        //GameObject prefabGo = Resources.Load<GameObject>("ContactEffects(��Ч·��)/" + hit.collider.tag);
        //if (prefabGo)
        //    Instantiate(prefabGo, hitPos + hit.normal * 0.01f, Quaternion.LookRotation(hit.normal));
        #endregion
        GameObject prefabGo = EffectPool.instance.GetPooledObject(hit.collider.tag);
        prefabGo.transform.position = hitPos + hit.normal * 0.01f;
        prefabGo.transform.rotation = Quaternion.LookRotation(hit.normal);
    }
}
