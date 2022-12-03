using Zenject;

namespace Game.Map.Element.Building
{
    public class BuildingInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(ITickable),typeof(IDateTimer)).To<Timer>().AsSingle();

            Container.Bind(typeof(ITickable),typeof(IBuildingStateMachine))
                .To<BuildingStateMachine>()
                .AsSingle();



        }
    }
}