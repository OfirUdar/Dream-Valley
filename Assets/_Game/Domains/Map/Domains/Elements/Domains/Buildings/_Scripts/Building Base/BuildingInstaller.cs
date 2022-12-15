using UnityEngine;
using Zenject;

namespace Game.Map.Element.Building
{
    public class BuildingInstaller : MonoInstaller
    {
        [SerializeField] private LevelsListSO _levels;

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

        }
    }

}