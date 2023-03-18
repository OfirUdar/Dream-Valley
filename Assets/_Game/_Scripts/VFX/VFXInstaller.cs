using Zenject;

namespace Game
{
    public class VFXInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(IInitializable), typeof(IVFXPool)).To<VFXPool>().AsSingle();
        }
    }
}