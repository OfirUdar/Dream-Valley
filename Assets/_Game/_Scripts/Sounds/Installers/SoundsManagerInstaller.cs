using UnityEngine;
using Zenject;

namespace Game
{
    public class SoundsManagerInstaller : MonoInstaller
    {
        [SerializeField] private AudioSource _audioSource;
        public override void InstallBindings()
        {
            Container.Bind<ISoundsManager>().To<SoundsManager>()
                .AsSingle().WithArguments(_audioSource);
        }
    }
}
