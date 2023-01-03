using Zenject;

namespace Game.Resources
{
    public class ResourceCapacityInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IResourcesCapacityManager>()
                .To<ResourcesCapacityManager>().AsSingle();
        }
    }
}