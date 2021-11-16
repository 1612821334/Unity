using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class WayLine
{
    public Vector3[] WayPoints { get; set; }//路线点
    public bool IsUsable { get; set; }      //路线可用状态
    public WayLine()
    {

    }
    public WayLine(int pointCount)
    {
        WayPoints = new Vector3[pointCount];
        IsUsable = true;
    }
}
