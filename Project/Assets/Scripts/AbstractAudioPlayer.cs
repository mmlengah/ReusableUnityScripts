using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))] 
public abstract class AbstractAudioPlayer<T> : MonoBehaviour where T : Enum
{
    protected Dictionary<T, AudioClip> audioClips;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        InitializeAudioClips();
    }

    protected abstract void InitializeAudioClips();

    public void PlayClip(T clipKey, bool loop = false)
    {
        if (audioClips.TryGetValue(clipKey, out AudioClip clip))
        {
            audioSource.clip = clip;
            audioSource.loop = loop;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Clip not found: " + clipKey);
        }
    }

    public void StopClip()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
