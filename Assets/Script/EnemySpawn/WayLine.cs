using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class WayLine
{
    public Vector3[] WayPoints { get; set; }//·�ߵ�
    public bool IsUsable { get; set; }      //·�߿���״̬
    public WayLine()
    {

    }
    public WayLine(int pointCount)
    {
        WayPoints = new Vector3[pointCount];
        IsUsable = true;
    }
}
