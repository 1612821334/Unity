using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBullets : MonoBehaviour
{
    private Text showText;
    public GameObject objPlayer;
    private AutomaticGun playerGun;
    void Start()
    {
        showText = GetComponentInChildren<Text>();
        playerGun = objPlayer.GetComponentInChildren<AutomaticGun>();
    }
    void Update()
    {
        showText.text = playerGun.remainBullets.ToString();
    }
}
