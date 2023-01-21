using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game
{
    public interface ISoundsManager
    {
        public void PlayOneShot(AudioClip clip);
        public void PlayOneShot(AudioClip clip, float volumeScale);
        public void PlayRandomOneShot(params AudioClip[] clips);
        public void PlayRandomOneShot(float volumeScale, params AudioClip[] clips);
    }
    public class SoundsManager : ISoundsManager
    {
        [Inject] private readonly AudioSource _audioSource;

        public void PlayOneShot(AudioClip clip)
        {
            _audioSource.PlayOneShot(clip);
        }
        public void PlayOneShot(AudioClip clip, float volumeScale)
        {
            _audioSource.PlayOneShot(clip, volumeScale);
        }
        public void PlayRandomOneShot(params AudioClip[] clips)
        {
            var randomIndex = Random.Range(0, clips.Length);
            PlayOneShot(clips[randomIndex]);
        }
        public void PlayRandomOneShot(float volumeScale, params AudioClip[] clips)
        {
            var randomIndex = Random.Range(0, clips.Length);
            PlayOneShot(clips[randomIndex], volumeScale);
        }
    }
}
