using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 查找符合生成敌人距离玩家
/// </summary>
public class SearchFixDistancePlayer : MonoBehaviour
{
    public static PlayerStatusInfo[] allPlayers;
    public PlayerStatusInfo minDistancePlayer;
    public float distance = 8;
    public float min;
    private void OnEnable()
    {
        allPlayers = Object.FindObjectsOfType<PlayerStatusInfo>();
    }
    public PlayerStatusInfo FindPlayerByMinDistance()
    {
        float minDistince = Vector3.Distance(this.transform.position, allPlayers[0].transform.position);
        minDistancePlayer = allPlayers[0];
        for (int i = 1; i < allPlayers.Length; i++)
        {
            float newDistince = Vector3.Distance(this.transform.position, allPlayers[i].transform.position);
            if (minDistince > newDistince)
            {
                minDistince = newDistince;
                minDistancePlayer = allPlayers[i];
            }
        }
        if (minDistince < distance)
        {
            min = minDistince;
            return minDistancePlayer;
        }
        return null;
    }
}