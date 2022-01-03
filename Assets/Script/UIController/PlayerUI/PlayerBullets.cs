using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBullets : MonoBehaviour
{
    private Text showText;
    private GameObject objPlayer;
    private AutomaticGun playerGun;
    void Start()
    {
        showText = GetComponentInChildren<Text>();
        objPlayer = GameObject.FindWithTag("Player");
        playerGun = objPlayer.GetComponentInChildren<AutomaticGun>();
    }
    void Update()
    {
        showText.text = playerGun.remainBullets.ToString();
    }
}
