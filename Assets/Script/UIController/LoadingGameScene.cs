using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingGameScene : MonoBehaviour
{
    public Text loadingText;
    public Image progressBar;
    public float progressValue = 100;
    private float curProgressValue = 0;
    private void Start()
    {
        progressBar.fillAmount = 0;
    }
    //private void FixedUpdate()
    //{
    //    if (curProgressValue < progressValue)
    //    {
    //        curProgressValue++;
    //    }
    //    loadingText.text = string.Format("游戏正在加载中，请稍后.....{0}% ", curProgressValue / progressValue * 100);
    //    progressBar.fillAmount = curProgressValue / progressValue;
    //    if (curProgressValue == 100)
    //    {
    //        SceneManager.LoadScene(2);
    //    }
    //}
    private void Update()
    {
        StartCoroutine(LoadGame());
    }

    private IEnumerator LoadGame()
    {
        Loading();
        yield return new WaitForSeconds(0.2f);
    }
    private void Loading()
    {
        if (curProgressValue < progressValue)
        {
            curProgressValue++;
        }
        loadingText.text = string.Format("游戏正在加载中，请稍后.....{0}% ", curProgressValue / progressValue * 100);
        progressBar.fillAmount = curProgressValue / progressValue;
        if (curProgressValue >= 100)
        {
            SceneManager.LoadScene(2);
        }
    }
}
