using Zenject;

namespace Game.Map.Element.LevelSystem
{
    public class LevelSystemInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<LevelManager>().AsSingle().NonLazy();
        }

    }
}