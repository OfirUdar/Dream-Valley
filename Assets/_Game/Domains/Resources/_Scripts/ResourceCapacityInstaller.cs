using Zenject;

namespace Game.Resources
{
    public class ResourceCapacityInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ResourcesCapacityManager>().AsSingle();
        }
    }
}