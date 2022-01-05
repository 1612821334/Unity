using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeart : MonoBehaviour
{
    private Text hpText;
    private float totalHp;
    private Image hpIMage;
    public GameObject objPlayer;
    private PlayerStatusInfo playerInfo;
    void Start()
    {
        hpText = GetComponentInChildren<Text>();
        hpIMage= GetComponentInChildren<Image>();
        playerInfo = objPlayer.GetComponentInChildren<PlayerStatusInfo>();
        hpIMage.fillAmount = 1; totalHp = playerInfo.hp;
    }
    void Update()
    {
        hpText.text = playerInfo.hp.ToString();
        hpIMage.fillAmount = playerInfo.hp / totalHp;
    }
}
