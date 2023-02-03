using UnityEngine;
using Zenject;

namespace Game.Resources.UI
{
    public class UIResourcesInstaller : MonoInstaller
    {
        [Header("Prefabs: ")]
        [SerializeField] private ResourceUI _resourcesUIPfb;
        [SerializeField] private ResourceUITween _resourcesUITweenPfb;

        [Header("Containers: ")]
        [SerializeField] private Transform _container;
        [SerializeField] private Transform _resourcesInventoryTransform;

        public override void InstallBindings()
        {
            Container.BindMemoryPool<ResourceUITween, ResourceUITween.Pool>()
                .ExpandByOneAtATime()
                .FromComponentInNewPrefab(_resourcesUITweenPfb)
                .UnderTransform(_container);

            Container.BindFactory<ResourceUI, ResourceUI.Factory>()
               .FromComponentInNewPrefab(_resourcesUIPfb)
               .UnderTransform(_resourcesInventoryTransform);

        }
    }
}
