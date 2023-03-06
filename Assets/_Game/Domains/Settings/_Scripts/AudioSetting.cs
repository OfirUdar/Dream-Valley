using UnityEngine;
using UnityEngine.Audio;

namespace Game.Settings
{
    public class AudioSetting
    {
        private readonly AudioMixer _audioMixer;
        private readonly string _savedKey;

        public AudioSetting(AudioMixer audioMixer, string savedKey)
        {
            _audioMixer = audioMixer;
            _savedKey = savedKey;

            var loadSavedVolume = PlayerPrefs.GetFloat(_savedKey, 1);

            SetValueWithoutSave(loadSavedVolume);
        }

        public float GetValue()
        {
            return PlayerPrefs.GetFloat(_savedKey, 1);
        }
        public void SetValue(float value)
        {
            SetValueWithoutSave(value);
            PlayerPrefs.SetFloat(_savedKey, value);
        }
        public void SetValueWithoutSave(float value)
        {
            _audioMixer.SetFloat("Volume", -80f + value * 80f);
        }
    }
}
