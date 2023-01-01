using Zenject;

namespace Game.Map
{
    public class OptionsAggragetorInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IOptionsAggragetor>().To<OptionsAggragetor>().AsSingle();
        }

    }
}