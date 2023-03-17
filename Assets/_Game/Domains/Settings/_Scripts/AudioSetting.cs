using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace Game.Settings
{
    public class AudioSetting:IInitializable
    {
        private readonly AudioMixer _audioMixer;
        private readonly string _savedKey;
        private readonly float _defaultValue;

        public AudioSetting(AudioMixer audioMixer, string savedKey,float defaultValue)
        {
            _audioMixer = audioMixer;
            _savedKey = savedKey;
            _defaultValue = defaultValue;
        }
        public void Initialize()
        {
            var loadSavedVolume = GetValue();

            SetValueWithoutSave(loadSavedVolume);
        }
        public float GetValue()
        {
            return PlayerPrefs.GetFloat(_savedKey, _defaultValue);
        }

        public void SetValue(float value)
        {
            SetValueWithoutSave(value);
            PlayerPrefs.SetFloat(_savedKey, value);
        }
        public void SetValueWithoutSave(float value)
        {
            _audioMixer.SetFloat("MasterVolume", Mathf.Log10(value)*20);
        }

    }
}
