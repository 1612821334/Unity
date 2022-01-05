using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonEventCenter : MonoBehaviour
{
    public GameObject menu;
    public GameObject myBag;
    private GameObject settingSence;
    private GameObject settingmenu;
    private AudioSource[] sources;
    private AudioSource buttonSource;
    public delegate void Voice();
    public Voice VoiceInitialization;
    private void Start()
    {
        VoiceInitialization += IsVoice;
        VoiceInitialization += SetVoice;
        buttonSource = GetComponent<AudioSource>();
        settingSence = GameObject.FindObjectOfType<DontDestory>().gameObject;
        settingmenu = settingSence.transform.GetChild(0).gameObject;
    }
    private void FixedUpdate()
    {
        if (menu != null && !menu.activeInHierarchy)
        {
            settingmenu.SetActive(false);
        }
        sources = GameObject.FindObjectsOfType<AudioSource>();
    }
    /// <summary>
    /// ʵʱ��������
    /// </summary>
    private void Update()
    {
        VoiceInitialization();
    }
    /// <summary>
    /// ����״̬����
    /// </summary>
    private void IsVoice()
    {
        if (sources == null) return;
        for (int index = 0; index < sources.Length; index++)
        {
            if (sources[index] == null) continue;
            sources[index].mute = DontDestory.isVoice;
        }
    }
    /// <summary>
    /// ������С����
    /// </summary>
    private void SetVoice()
    {
        if (sources == null) return;
        for (int index = 0; index < sources.Length; index++)
        {
            if (sources[index] == null) continue;
            sources[index].volume = DontDestory.voice;
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
        buttonSource.Play();
        Time.timeScale = 0;
        settingmenu.SetActive(!settingmenu.activeSelf);
    }
    /// <summary>
    /// ������Ϸ
    /// </summary>
    public void OnContinueGame()
    {
        buttonSource.Play();
        Time.timeScale = 1;
        menu.SetActive(!menu.activeSelf);
    }
    /// <summary>
    /// ���¿�ʼ
    /// </summary>
    public void OnReStartGame()
    {
        buttonSource.Play();
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
        buttonSource.Play();
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
