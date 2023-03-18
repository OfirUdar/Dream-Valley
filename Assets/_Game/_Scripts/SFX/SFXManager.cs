using System.Threading.Tasks;
using UnityEngine;

namespace Game
{
    public class SFXManager : ISFXManager
    {
        private readonly AudioSource _audioSource;

        public SFXManager(AudioSource audioSource)
        {
            _audioSource = audioSource;
        }


        public void PlayOneShot(AudioClipInfoSO audioInfo)
        {
            _audioSource.pitch = audioInfo.Pitch;
            _audioSource.PlayOneShot(audioInfo.Clip, audioInfo.ScaleVolume);
        }

        public async void PlayOneShotWithDelay(AudioClipInfoSO audioInfo, int milisecondsDelay)
        {
            await Task.Delay(milisecondsDelay);
            PlayOneShot(audioInfo);
        }

        public void PlayRandomOneShot(params AudioClipInfoSO[] audioInfos)
        {
            var randomIndex = Random.Range(0, audioInfos.Length);
            PlayOneShot(audioInfos[randomIndex]);
        }
    }
}
