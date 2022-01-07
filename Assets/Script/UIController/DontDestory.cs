using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���س���ʱ��ɾ��������
/// </summary>
public class DontDestory : MonoBehaviour
{
    /// <summary>
    /// ���س���ʱ�����ٵ�����
    /// </summary>
    public GameObject[] DontDestroyObjects;
    /// <summary>
    /// ��Ϸ����
    /// </summary>
    public static float voice = 0.5f;
    /// <summary>
    /// ��Ч״̬
    /// </summary>
    public static bool isVoice = false;
    /// <summary>
    /// �Ƿ��Ѿ�����DontDestroy������
    /// </summary>
    private static bool isExist;

    void Awake()
    {
        DontDestoryOnLoadSence();
    }
    private void DontDestoryOnLoadSence()
    {
        if (!isExist)
        {
            for (int i = 0; i < DontDestroyObjects.Length; i++)
            {
                //�����һ�μ��أ�����Щ������ΪDontDestroy
                DontDestroyOnLoad(DontDestroyObjects[i]);
            }
            Resolution[] resoulutions = Screen.resolutions;
            Screen.SetResolution(resoulutions[resoulutions.Length - 1].width, resoulutions[resoulutions.Length - 1].height, true, 60);
            isExist = true;
        }
        else
        {
            for (int i = 0; i < DontDestroyObjects.Length; i++)
            {
                //����Ѿ����ڣ���ɾ���ظ�������
                Destroy(DontDestroyObjects[i]);
            }
        }
    }
}
