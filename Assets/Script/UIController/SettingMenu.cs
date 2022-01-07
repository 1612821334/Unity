using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingMenu : MonoBehaviour
{
    private AudioSource[] sources;
    private Slider sliderControl;
    private Dropdown dropDownCrl;
    /// <summary>
    /// �Ƿ�ȫ��
    /// </summary>
    private bool isFull;
    /// <summary>
    /// �Ƿ���
    /// </summary>
    private bool isVoice = true;
    private void Start()
    {
        sources = GameObject.FindObjectsOfType<AudioSource>();
        sliderControl = GetComponentInChildren<Slider>();
        dropDownCrl = GetComponentInChildren<Dropdown>();
        sliderControl.value = sources[0].volume;
    }
    /// <summary>
    /// ��������
    /// </summary>
    public void IsVoice()
    {
        DontDestory.isVoice = isVoice;
        isVoice = !isVoice;
    }
    /// <summary>
    /// ��������
    /// </summary>
    public void VoiceSetting()
    {
        DontDestory.voice = sliderControl.value;
    }
    /// <summary>
    /// ȫ������
    /// </summary>
    public void IsFullScreen()
    {
        Screen.fullScreen = isFull;
        isFull = !isFull;
    }
    /// <summary>
    /// �ֱ�������
    /// </summary>
    public void SetResolution()
    {
        switch (dropDownCrl.value)
        {
            case 0: Screen.SetResolution(2560, 1600, !isFull); break;
            case 1: Screen.SetResolution(1920, 1080, !isFull); break;
            case 2: Screen.SetResolution(1280, 720, !isFull); break;
        }
    }
    /// <summary>
    ///�˳�����
    /// </summary>
    public void OnSetGameButtonClick()
    {
        Time.timeScale = 0;
        this.gameObject.SetActive(!this.gameObject.activeSelf);
    }
}
