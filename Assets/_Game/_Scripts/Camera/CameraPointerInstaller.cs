using Zenject;

namespace Game
{
    public class CameraPointerInstaller : MonoInstaller
    {

        public override void InstallBindings()
        {
            Container.Bind<ICameraPointerUtility>().To<CamPointerUtility>().AsSingle();
        }
    }
}