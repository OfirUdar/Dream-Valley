using UnityEngine;
using Zenject;

namespace Game.Map.Grid
{
    public class GridInstaller : MonoInstaller
    {
        [SerializeField] private GridSettingsSO _settings;
        [Space]
        [SerializeField] private GroundGridVisual _groudGridVisual;
        public override void InstallBindings()
        {
            Container.Bind<GridSettingsSO>()
                .FromInstance(_settings).AsSingle();

            Container.Bind<IMapGrid>().To<MapGrid>()
                .AsSingle().NonLazy();

            Container.Bind<IGroundGridVisual>().FromInstance(_groudGridVisual);
        }


    }
}
