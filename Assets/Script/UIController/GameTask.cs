using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��Ϸ����
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
        taskText.text = "ǰ��������ʬ��0/12";
    }
    private void Update()
    {
        if (taskText.text == "ǰ��������ʬ��12/12")
        {
            winObj.SetActive(!winObj.activeSelf);
            Time.timeScale = 0;
        }
    }
}
