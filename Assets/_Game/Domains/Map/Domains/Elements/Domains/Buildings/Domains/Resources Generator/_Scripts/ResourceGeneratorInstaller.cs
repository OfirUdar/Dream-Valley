using UnityEngine;
using Zenject;

namespace Game.Map.Element.Building.Resources
{
    public class ResourceGeneratorInstaller : MonoInstaller
    {
        [SerializeField] private ResourceGeneratorLevelsData _levels;

        public override void InstallBindings()
        {
            //Install Active State
            //Container.Bind<IBuildingState>()
            //    .WithId(StateType.Active)
            //    .To<ResourceGenerator>().AsSingle();

            //Container.Bind<IResourceGenerator>()
            //    .To<ResourceGenerator>().FromResolve();

            Container.BindInterfacesAndSelfTo<ResourceGenerator>().AsSingle();

            Container.Bind<IBuildingState>()
               .WithId(StateType.Active)
               .To<ResourceGenerator>().FromResolve();

            //Install Upgrade
            Container.Bind<IBuildingState>()
                .WithId(StateType.Upgrade)
                .To<UpgradeBuilder>().AsSingle();

            Container.Bind<ResourceGeneratorLevelsData>().FromInstance(_levels).AsSingle();
        }
    }
}