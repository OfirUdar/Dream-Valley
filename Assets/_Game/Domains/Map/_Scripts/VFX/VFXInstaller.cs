using Zenject;

namespace Game.Map
{
    public class VFXInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IVFXFactory>().To<VFXFactory>().AsSingle();
        }
    }
}