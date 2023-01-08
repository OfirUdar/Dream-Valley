using Zenject;

namespace Game.Camera
{
    public class CameraPointerInstaller : MonoInstaller
    {

        public override void InstallBindings()
        {
            Container.Bind<ICameraPointerUtility>().To<CamPointerUtility>().AsSingle();
        }
    }
}