using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 主相机事件
/// </summary>
public class MainCameraEvent : MonoBehaviour
{
    private Animation anim;
    private Camera mainCamera;
    public float playtime = 60;
    private AnimationEvent _event = new AnimationEvent();
    private void Start()
    {
        mainCamera = GetComponent<Camera>();
        anim = GetComponent<Animation>();
        ShowGameMap();
        anim.clip.AddEvent(_event);
    }
    /// <summary>
    /// 展示地图
    /// </summary>
    private void ShowGameMap()
    {
        mainCamera.depth = 2;
        anim.PlayQueued(anim.clip.name);
        _event.functionName = "ExitShowMap";
        _event.time = playtime;
    }
    /// <summary>
    /// 退出展示
    /// </summary>
    private void ExitShowMap()
    {
        anim.Stop();
        mainCamera.depth = -1;
        Time.timeScale = 1;
    }
}
