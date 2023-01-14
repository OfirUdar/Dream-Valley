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
                Application.targetFrameRate = 60;
                //Screen.SetResolution(Screen.width * (6 / 10), Screen.height * (6 / 10), true);
                Container.Bind<IUserInput>().To<MobileInput>().AsSingle();
            }
            else
            {
                Container.Bind<IUserInput>().To<DefaultInput>().AsSingle();
            }
        }
    }
}

