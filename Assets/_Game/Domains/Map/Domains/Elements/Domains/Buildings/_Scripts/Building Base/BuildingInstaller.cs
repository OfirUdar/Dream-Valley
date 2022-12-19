using UnityEngine;
using Zenject;

namespace Game.Map.Element.Building
{
    public class BuildingInstaller : MonoInstaller
    {
        [SerializeField] private LevelsListSO _levels;
        [Space]
        [SerializeField] private LevelsElementVisualHandler _elementVisualHandler;

        public override void InstallBindings()
        {
            Container.Bind(typeof(ITickable), typeof(IDateTimer)).To<Timer>().AsSingle();

            Container.BindInterfacesTo<BuildingStateMachine>().AsSingle();

            //Install Upgrade
            Container.BindInterfacesAndSelfTo<UpgradeState>().AsSingle();

            Container.Bind<IBuildingState>()
               .WithId(StateType.Upgrade)
               .To<UpgradeState>().FromResolve();

            Container.Bind<LevelsListSO>().FromInstance(_levels);
            Container.Bind<ILevelsElementVisualHandler>().FromInstance(_elementVisualHandler);

        }
    }

}