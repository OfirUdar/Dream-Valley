using UnityEngine;
using Zenject;

namespace Game
{
    public class SoundsManagerInstaller : MonoInstaller
    {
        [SerializeField] private AudioSource _audioSource;
        public override void InstallBindings()
        {
            Container.Bind<ISFXManager>().To<SFXManager>()
                .AsSingle().WithArguments(_audioSource);
        }
    }
}
