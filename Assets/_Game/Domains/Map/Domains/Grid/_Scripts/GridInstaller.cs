using UnityEngine;
using Zenject;

namespace Game.Map.Grid
{
    public class GridInstaller : MonoInstaller
    {
        [Space]
        [SerializeField] private GridSettingsSO _settings;

        public override void InstallBindings()
        {
            Container.Bind<GridSettingsSO>()
                .FromInstance(_settings).AsSingle();

            Container.Bind<IMapGrid>().To<MapGrid>()
                .AsSingle().NonLazy();

        }


    }
}
