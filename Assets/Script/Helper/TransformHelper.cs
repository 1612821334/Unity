using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Transfrom������
/// </summary>
public class TransfromHleper
{
    /// <summary>
    /// �㼶δ֪����������
    /// </summary>
    /// <param name="transfromBase">������任�������</param>
    /// <param name="childName">����������</param>
    /// <returns></returns>
    public static Transform GetChildrenOfTranform(Transform transfromBase, string childName)
    {
        //�������в���
        Transform transformOfChildern = transfromBase.Find(childName);
        if (transformOfChildern != null)
        {
            return transformOfChildern;
        }
        //���⽻�����������
        int count = transfromBase.childCount;
        for (int i = 0; i < count; i++)
        {
            {
                transformOfChildern = GetChildrenOfTranform(transfromBase.GetChild(i), childName);
                if (transformOfChildern != null)
                {
                    return transformOfChildern;
                }
            }
        }
        return null;
    }
}