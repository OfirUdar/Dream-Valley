using Zenject;

namespace Game
{
    public class ProfileInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            //Container.Bind<Profile>().ToSelf().AsSingle(); 
        }
    }
}