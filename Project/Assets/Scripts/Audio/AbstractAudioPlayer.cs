using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameAudio
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class AbstractAudioPlayer<T> : MonoBehaviour where T : Enum
    {
        [Header("Audio Files")]
        [SerializeField]
        private List<AudioClipPair<T>> audioClipPairs = new();

        protected Dictionary<T, AudioClip> audioClips = new();
        private AudioSource audioSource;

        void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            BuildDictionary();
        }

        private void BuildDictionary()
        {
            audioClips.Clear();
            foreach (var pair in audioClipPairs)
            {
#if UNITY_EDITOR
                if (audioClips.ContainsKey(pair.key))
                {
                    Debug.LogError("Overwriting existing clip in dictionary for key: " + pair.key);
                }
#endif
                audioClips[pair.key] = pair.value;
            }
        }

        protected void PlayClip(T clipKey, bool loop = false)
        {
            if (audioClips.TryGetValue(clipKey, out AudioClip clip))
            {
                audioSource.clip = clip;
                audioSource.loop = loop;
                audioSource.Play();
            }
#if UNITY_EDITOR
            else
            {
                Debug.LogWarning("Clip not found: " + clipKey);
            }
#endif
        }

        protected void StopClip()
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
