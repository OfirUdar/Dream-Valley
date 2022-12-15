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
            Container.BindInterfacesAndSelfTo<ResourceGenerator>().AsSingle();

            Container.Bind<IBuildingState>()
               .WithId(StateType.Active)
               .To<ResourceGenerator>().FromResolve();


            Container.Bind<ResourceGeneratorLevelsData>().FromInstance(_levels).AsSingle();
        }
    }
}