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
    /// ��ʼ��Ϸ
    /// </summary>
    public void OnStartGameButtonClick()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    /// <summary>
    /// ��Ϸ����
    /// </summary>
    public void OnSetGameButtonClick()
    {
        Time.timeScale = 0;
        settingSence.SetActive(true);
    }
    /// <summary>
    /// ������Ϸ
    /// </summary>
    public void OnContinueGame()
    {
        Time.timeScale = 1;
        menu.SetActive(!menu.activeSelf);
    }
    /// <summary>
    /// ���¿�ʼ
    /// </summary>
    public void OnReStartGame()
    {
        Scene Load = SceneManager.GetActiveScene();
        SceneManager.LoadScene(Load.name);
        Time.timeScale = Time.timeScale == 1 ? 0 : 1;
    }
    /// <summary>
    /// ����������
    /// </summary>
    public void OnBackMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    /// <summary>
    /// �رձ���
    /// </summary>
    public void OnCloseBag()
    {
        myBag.SetActive(!myBag.activeSelf);
        Time.timeScale = Time.timeScale == 1 ? 0 : 1;
    }
    /// <summary>
    /// �˳���Ϸ
    /// </summary>
    public void OnExitButtonClick()
    {
        Application.Quit();
    }
}
