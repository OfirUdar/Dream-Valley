using Zenject;

namespace Game.Map
{
    public class MapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<MapManager>().AsSingle().OnInstantiated(OnMapManagerInstainiated);
            Container.Bind<IMapSaver>().To<MapSaver>().AsSingle();
        }

        private void OnMapManagerInstainiated(InjectContext context, object sender)
        {
            context.Container.Resolve<IMapManager>().LoadAll();
        }
    }
}