using Zenject;

namespace Game
{
    public class CamPointerInstaller : MonoInstaller
    {

        public override void InstallBindings()
        {
            Container.Bind<CamPointerUtility>().ToSelf().AsSingle();
        }
    }
}