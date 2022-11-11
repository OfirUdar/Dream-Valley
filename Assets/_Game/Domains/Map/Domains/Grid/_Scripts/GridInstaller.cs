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

            Container.Bind<IMapGrid>().To<Grid>()
                .AsSingle().NonLazy();


            Container.BindInterfacesAndSelfTo<SelectionManager>()
                .AsSingle().NonLazy();
            Container.Bind<DragManager>().ToSelf()
              .AsSingle().NonLazy();
        }


    }
}
