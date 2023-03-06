using UnityEngine;
using UnityEngine.Audio;
using Zenject;
namespace Game.Settings
{
    public class SoundsSettingsInstaller : MonoInstaller
    {
        private const string _SFX_VOLUME_KEY = "SFX_VOLUME";
        private const string _MUSIC_VOLUME_KEY = "MUSIC_VOLUME";

        [SerializeField] private AudioMixer _sfxAudioMixer;
        [SerializeField] private AudioMixer _musicAudioMixer;

        public override void InstallBindings()
        {
            Container.Bind<AudioSetting>().WithId("SFX").ToSelf()
                .AsTransient()
                .WithArguments(_sfxAudioMixer,_SFX_VOLUME_KEY);


            Container.Bind<AudioSetting>().WithId("Music").ToSelf()
                .AsTransient()
                .WithArguments(_musicAudioMixer, _MUSIC_VOLUME_KEY);
        }

    }
}
