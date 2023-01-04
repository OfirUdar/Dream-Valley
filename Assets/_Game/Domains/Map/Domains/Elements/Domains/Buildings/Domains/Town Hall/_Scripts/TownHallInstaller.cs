using UnityEngine;
using Zenject;

namespace Game.Map.Element.Building.TownHall
{
    public class TownHallInstaller : MonoInstaller
    {
        //*** NEED TO BE DONE
        //Info popup
        //Upgrade popup

        [SerializeField] private TownHallLevelsData _townHallLevels;
        [SerializeField] private UITownHallInfoDisplay _uiInfoDisplayPfb;

        public override void InstallBindings()
        {
            //Install Active State
            Container.BindInterfacesAndSelfTo<TownHallState>().AsSingle();

            Container.Bind<IBuildingState>()
               .WithId(StateType.Active)
               .To<TownHallState>().FromResolve();

            Container.Bind<TownHallLevelsData>().FromInstance(_townHallLevels).AsSingle();

            Container.Bind<IInfoDisplayer>().To<InfoDisplayer>()
                .AsSingle()
                .WithArguments(_uiInfoDisplayPfb);

            //Container.Bind<IUpgradeDisplayer>().To<UpgradeDisplayer>()
            //   .AsSingle()
            //   .WithArguments(_uiUpgradeDisplayPfb);


            Container.BindInterfacesTo<TownHallCapacityContainer>().AsSingle().NonLazy();
        }
    }
}
