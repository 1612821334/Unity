using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonEventCenter : MonoBehaviour
{
    public GameObject menu;
    public GameObject myBag;
    public GameObject settingSence;
    //private void Start()
    //{
    //    GetComponent<Button>().onClick.AddListener(OnClick);
    //}
    private void FixedUpdate()
    {
        if (!menu.activeInHierarchy)
        {
            settingSence.SetActive(false);
        }
    }
    /// <summary>
    /// 开始游戏
    /// </summary>
    public void OnStartGameButtonClick()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    /// <summary>
    /// 游戏设置
    /// </summary>
    public void OnSetGameButtonClick()
    {
        Time.timeScale = 0;
        settingSence.SetActive(true);
    }
    /// <summary>
    /// 继续游戏
    /// </summary>
    public void OnContinueGame()
    {
        Time.timeScale = 1;
        menu.SetActive(!menu.activeSelf);
    }
    /// <summary>
    /// 重新开始
    /// </summary>
    public void OnReStartGame()
    {
        Scene Load = SceneManager.GetActiveScene();
        SceneManager.LoadScene(Load.name);
        Time.timeScale = Time.timeScale == 1 ? 0 : 1;
    }
    /// <summary>
    /// 返回主界面
    /// </summary>
    public void OnBackMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    /// <summary>
    /// 关闭背包
    /// </summary>
    public void OnCloseBag()
    {
        myBag.SetActive(!myBag.activeSelf);
        Time.timeScale = Time.timeScale == 1 ? 0 : 1;
    }
    /// <summary>
    /// 退出游戏
    /// </summary>
    public void OnExitButtonClick()
    {
        Application.Quit();
    }
}
