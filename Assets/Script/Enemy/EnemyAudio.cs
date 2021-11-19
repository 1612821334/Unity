using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// µ–»À“Ù–ß
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class EnemyAudio : MonoBehaviour
{
    public AudioClip[] clips;
    public EnemyAudioCenter source;
    private void Awake()
    {
        source = new EnemyAudioCenter(GetComponent<AudioSource>(), clips);
    }
}
