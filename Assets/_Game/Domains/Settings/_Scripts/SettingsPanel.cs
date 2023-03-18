using System;
using System.IO;
using Udar;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Game.Settings.UI
{
    public class SettingsPanel : MonoBehaviour
    {
        [SerializeField] private Slider _sfxVolumeSlider;
        [SerializeField] private Slider _musicVolumeSlider;
        [SerializeField] private Button _deleteAllDataButton;

        private AudioSetting _sfxSetting;
        private AudioSetting _musicSetting;

        [Inject] private readonly IDialog _dialog;

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
            _deleteAllDataButton.onClick.AddListener(OnDeleteAllDataClicked);
        }
        private void OnDisable()
        {
            _sfxVolumeSlider.onValueChanged.RemoveListener(OnSFXVolumeSliderChanged);
            _musicVolumeSlider.onValueChanged.RemoveListener(OnMusicVolumeSliderChanged);
            _deleteAllDataButton.onClick.RemoveListener(OnDeleteAllDataClicked);
        }


        private void OnSFXVolumeSliderChanged(float volume)
        {
            _sfxSetting.SetValue(volume);
        }
        private void OnMusicVolumeSliderChanged(float volume)
        {
            _musicSetting.SetValue(volume);
        }

        private void OnDeleteAllDataClicked()
        {
            _dialog.ShowComplex("Delete all Data", "Are you sure deleting all data?", "Delete", "Cancel", DeleteAllDataAsync);
        }

        private async void DeleteAllDataAsync()
        {
           
            Directory.Delete(Application.persistentDataPath, true);

            await SceneChanger.LoadSingleAsync("ApplicationInitalizer");

        }
    }



}

