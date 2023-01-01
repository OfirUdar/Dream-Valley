using UnityEngine;
using Zenject;

namespace Game.Map.Element.Building.Resources
{
    public class ResourceGeneratorInstaller : MonoInstaller
    {
        [SerializeField] private ResourceGeneratorLevelsData _levels;
        [SerializeField] private UIResourceInfoDisplay _uiInfoDisplayPfb;
        [SerializeField] private UIResourceUpgradeDisplay _uiUpgradeDisplayPfb;

        public override void InstallBindings()
        {
            //Install Active State
            Container.BindInterfacesAndSelfTo<ResourceGenerator>().AsSingle();

            Container.Bind<IBuildingState>()
               .WithId(StateType.Active)
               .To<ResourceGenerator>().FromResolve();


            Container.Bind<ResourceGeneratorLevelsData>().FromInstance(_levels).AsSingle();

            Container.Bind<IInfoDisplayer>().To<InfoDisplayer>()
                .AsSingle()
                .WithArguments(_uiInfoDisplayPfb);
            Container.Bind<IUpgradeDisplayer>().To<UpgradeDisplayer>()
               .AsSingle()
               .WithArguments(_uiUpgradeDisplayPfb);

         

        }
    }
}