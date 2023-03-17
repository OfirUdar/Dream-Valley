using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Settings.UI
{
    public class SettingsPanel : MonoBehaviour
    {
        [SerializeField] private Slider _sfxVolumeSlider;
        [SerializeField] private Slider _musicVolumeSlider;

        private AudioSetting _sfxSetting;
        private AudioSetting _musicSetting;

        [Inject]
        public void Init([Inject(Id = "SFX")] AudioSetting sfxSetting, [Inject(Id = "Music")] AudioSetting musicSetting)
        {
            _sfxSetting = sfxSetting;
            _musicSetting = musicSetting;

            _sfxVolumeSlider.SetValueWithoutNotify(_sfxSetting.GetValue());
            _musicVolumeSlider.SetValueWithoutNotify(_musicSetting.GetValue());
        }


        private void OnEnable()
        {
            _sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeSliderChanged);
            _musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeSliderChanged);
        }
        private void OnDisable()
        {
            _sfxVolumeSlider.onValueChanged.RemoveListener(OnSFXVolumeSliderChanged);
            _musicVolumeSlider.onValueChanged.RemoveListener(OnMusicVolumeSliderChanged);
        }


        private void OnSFXVolumeSliderChanged(float volume)
        {
            _sfxSetting.SetValue(volume);
        }
        private void OnMusicVolumeSliderChanged(float volume)
        {
            _musicSetting.SetValue(volume);
        }
    }



}

