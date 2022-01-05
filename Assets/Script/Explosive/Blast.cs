using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 爆炸物切点判定
/// </summary>
public class Blast : MonoBehaviour
{
    private Vector3 playerToExplosion;
    private Vector3 playerToExplosionDirection;
    private Vector3 leftTangentVec, rightTangentVec;
    private Transform playerTF;
    private float radius = 0.5f;
    public string playerTag = "Player";
    public float angle;
    private void Start()
    {
        GameObject player = GameObject.FindWithTag(playerTag);
        playerTF = player.transform;
        radius = player.GetComponent<CharacterController>().radius;
    }
    private void Update()
    {
        if (!PlayerStatusInfo.istance.state)
        {
            Detection();
            Debug.DrawLine(transform.position, playerTF.position, Color.red);
        }
    }
    /// <summary>
    /// 切点判定
    /// </summary>
    private void CalculateTangent()
    {
        playerToExplosion = transform.position - playerTF.position;
        playerToExplosionDirection = playerToExplosion.normalized * radius;
        angle = Mathf.Acos(radius / playerToExplosion.magnitude) * Mathf.Rad2Deg;
        leftTangentVec = playerTF.position + Quaternion.Euler(0, -angle, 0) * playerToExplosionDirection;
        rightTangentVec= playerTF.position + Quaternion.Euler(0, angle, 0) * playerToExplosionDirection;
    }
    public void Detection()
    {
        CalculateTangent();
        Debug.DrawLine(transform.position, leftTangentVec, Color.red);
        Debug.DrawLine(transform.position, rightTangentVec, Color.red);
    }
}
