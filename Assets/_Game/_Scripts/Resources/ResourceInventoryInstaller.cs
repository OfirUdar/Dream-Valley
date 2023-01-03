using UnityEngine;
using Zenject;

namespace Game
{
    public class ResourceInventoryInstaller : MonoInstaller
    {
        [SerializeField] private InitResourceDataListSO _initResourceDataListSO;

        public override void InstallBindings()
        {
            Container.Bind<InitResourceDataListSO>()
              .FromInstance(_initResourceDataListSO).AsSingle();

            Container.BindInterfacesTo<ResourcesInventory>().AsSingle().NonLazy();
        }
    }
}