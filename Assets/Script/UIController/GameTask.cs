using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 游戏任务
/// </summary>
public class GameTask : MonoBehaviour
{
    public Text taskText;
    public GameObject winObj;
    public static GameTask instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        taskText = GetComponent<Text>();
        taskText.text = "前进！消灭僵尸：0/12";
    }
    private void Update()
    {
        if (taskText.text == "前进！消灭僵尸：12/12")
        {
            winObj.SetActive(!winObj.activeSelf);
            Time.timeScale = 0;
        }
    }
}
