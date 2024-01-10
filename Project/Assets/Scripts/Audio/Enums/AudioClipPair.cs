using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameAudio
{
    [System.Serializable]
    public class AudioClipPair<T> where T : Enum
    {
        public T key;
        public AudioClip value;
    }
}
