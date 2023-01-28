using UnityEngine;
using Zenject;

namespace Game
{
    public class PlaySound : MonoBehaviour
    {
        [SerializeField] private AudioClipInfo _audioClipInfo;
        
        [Inject] private readonly ISoundsManager _soundsManager;

        public void Play()
        {
            _soundsManager.PlayOneShot(_audioClipInfo);
        }
        public void PlayOneShot()
        {
            _soundsManager.PlayOneShot(_audioClipInfo);
        }
    }
}