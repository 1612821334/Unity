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
    private bool isFull = true;
    private bool isVoice = true;
    private void Start()
    {
        sources = GameObject.FindObjectsOfType<AudioSource>();
        sliderControl = GetComponentInChildren<Slider>();
        dropDownCrl = GetComponentInChildren<Dropdown>();
        sliderControl.value = sources[0].volume;
    }
    /// <summary>
    /// 声音开关
    /// </summary>
    public void IsVoice()
    {
        for (int index = 0; index < sources.Length; index++)
        {
            sources[index].mute = isVoice;
            DontDestory.isVoice = isVoice;
        }
        isVoice = !isVoice;
    }
    /// <summary>
    /// 声音设置
    /// </summary>
    public void VoiceSetting()
    {
        for(int index=0;index<sources.Length;index++)
        {
            sources[index].volume = sliderControl.value;
            DontDestory.voice = sliderControl.value;
        }
    }
    /// <summary>
    /// 全屏设置
    /// </summary>
    public void IsFullScreen()
    {
        Screen.fullScreen = isFull;
        isFull = !isFull;
    }
    /// <summary>
    /// 分辨率设置
    /// </summary>
    public void SetResolution()
    {
        switch (dropDownCrl.value)
        {
            case 0: Screen.SetResolution(2560, 1440, isFull); break;
            case 1: Screen.SetResolution(1920, 1080, isFull); break;
            case 2: Screen.SetResolution(800, 600, isFull); break;
        }
    }
    /// <summary>
    ///退出设置
    /// </summary>
    public void OnSetGameButtonClick()
    {
        Time.timeScale = 0;
        this.gameObject.SetActive(!this.gameObject.activeSelf);
    }
}
