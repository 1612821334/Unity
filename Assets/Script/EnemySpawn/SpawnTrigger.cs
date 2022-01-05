using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ɴ���
/// </summary>
public class SpawnTrigger : MonoBehaviour
{
    /// <summary>
    /// ������������
    /// </summary>
    public GameObject Spawn;
    private void OnEnable()
    {
        Spawn = GameObject.FindWithTag("Spawn");
    }
    /// <summary>
    /// �Ӵ�����
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            //�������������ɽڵ㼤��
            Spawn.transform.GetChild(0).gameObject.SetActive(true);
            transform.gameObject.SetActive(false);
        }
    }
}
