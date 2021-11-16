using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����Ч����
/// </summary>
public class PlayerAudio : MonoBehaviour
{
    public AudioClip[] clips;
    public PlayerAudioCenter source;
    private void Awake()
    {
        source = new PlayerAudioCenter(GetComponent<AudioSource>(),clips);
    }
}
