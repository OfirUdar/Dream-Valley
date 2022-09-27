using UnityEngine;
using Zenject;

namespace Game
{
    public class GridInstaller : MonoInstaller
    {
        [SerializeField] private BuildingSO buildingSO;
        [Space]
        [SerializeField] private GridSettingsSO _settings;

        public override void InstallBindings()
        {
            Container.Bind<GridSettings>()
                .FromInstance(_settings.Settings).AsSingle();


            Container.Bind<IGrid>().To<Grid>()
                .AsSingle().NonLazy();

            Container.Bind<ISelectionManager>()
                .To<GridSelectionManager>()
                .AsSingle().NonLazy();

            //Container.Bind<GridPlacer<Transform>>().ToSelf().AsSingle()
            //    .NonLazy();

            //Container.BindInterfacesAndSelfTo<GridEditor>().AsSingle().NonLazy();

            //Container
            //    .BindFactory<Object, PlacementBehaviour, PlacementBehaviour.Factory>()
            //    .FromFactory<PrefabFactory<PlacementBehaviour>>();


            //var snapping = Container.Resolve<GridEditor>();
            //snapping.SetPlacement(buildingSO);

           //UnityEngine.SceneManagement.SceneManager.LoadScene("MainUI", UnityEngine.SceneManagement.LoadSceneMode.Additive);
        }


    }
}
