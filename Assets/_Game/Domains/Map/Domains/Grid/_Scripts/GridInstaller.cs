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
            Container.Bind<GridSettings>()
                .FromInstance(_settings.Settings).AsSingle();

            Container.Bind<IGrid>().To<Grid>()
                .AsSingle().NonLazy();

        }


    }
}
