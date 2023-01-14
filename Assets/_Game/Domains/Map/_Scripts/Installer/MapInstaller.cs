using Zenject;

namespace Game.Map
{
    public class MapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<MapManager>().AsSingle();
            Container.Bind<IMapSaver>().To<MapSaver>().AsSingle();
        }
    }
}