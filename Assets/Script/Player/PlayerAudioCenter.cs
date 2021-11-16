using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家音效控制中心
/// </summary>
public class PlayerAudioCenter
{
    public enum AudioType
    {
        Walk,Run,Shoot,Jump,Death,Hit
    }
    private AudioSource sourcce;
    private AudioClip[] clips;
    public PlayerAudioCenter(AudioSource audio, AudioClip[] clip)
    {
        this.sourcce = audio;
        this.clips = clip;
    }
    /// <summary>
    /// 玩家音效判断
    /// </summary>
    /// <param name="type"></param>
    public void PlayAudioType(AudioType type)
    {
        switch(type)
        {
            case AudioType.Walk: PlayAudio(clips[0]); break;
            case AudioType.Run: PlayAudio(clips[1]); break;
            case AudioType.Shoot: PlayAudio(clips[2]); break;
            case AudioType.Jump: PlayAudio(clips[3]); break;
            case AudioType.Death: PlayAudio(clips[4]); break;
            case AudioType.Hit: PlayAudio(clips[5]); break;
        }
    }
    /// <summary>
    /// 玩家音效播放
    /// </summary>
    /// <param name="clip"></param>
    public void PlayAudio(AudioClip clip)
    {
        if (sourcce.isPlaying && clip == clips[3]) 
        {
            Play(clip);
        }
        if (sourcce.isPlaying)return;
        Play(clip);
    }
    /// <summary>
    /// 播放
    /// </summary>
    /// <param name="clip"></param>
    private void Play(AudioClip clip)
    {
        sourcce.clip = clip;
        sourcce.PlayOneShot(clip);
    }
}
