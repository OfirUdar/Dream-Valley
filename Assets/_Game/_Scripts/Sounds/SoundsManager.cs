﻿using UnityEngine;

namespace Game
{
    public class SoundsManager : ISoundsManager
    {
        private readonly AudioSource _audioSource;

        public SoundsManager(AudioSource audioSource)
        {
            _audioSource = audioSource;
        }


        public void PlayOneShot(AudioClipInfo audioInfo)
        {
            _audioSource.pitch = audioInfo.Pitch;
            _audioSource.PlayOneShot(audioInfo.Clip,audioInfo.ScaleVolume);
        }
        public void PlayRandomOneShot(params AudioClipInfo[] audioInfos)
        {
            var randomIndex = Random.Range(0, audioInfos.Length);
            PlayOneShot(audioInfos[randomIndex]);
        }
    }
}