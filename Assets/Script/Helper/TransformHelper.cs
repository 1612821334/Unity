using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Transfrom助手类
/// </summary>
public class TransfromHleper
{
    /// <summary>
    /// 层级未知查找子物体
    /// </summary>
    /// <param name="transfromBase">父物体变换组件引用</param>
    /// <param name="childName">子物体名称</param>
    /// <returns></returns>
    public static Transform GetChildrenOfTranform(Transform transfromBase, string childName)
    {
        //父物体中查找
        Transform transformOfChildern = transfromBase.Find(childName);
        if (transformOfChildern != null)
        {
            return transformOfChildern;
        }
        //问题交给子物体查找
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