using UnityEngine;
using Zenject;

namespace Game
{
    public class InputInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            if (Application.isMobilePlatform)
            {
                Container.Bind<IUserInput>().To<MobileInput>().AsSingle();
            }
            else
            {
                Container.Bind<IUserInput>().To<DefaultInput>().AsSingle();
            }
        }
    }
}

