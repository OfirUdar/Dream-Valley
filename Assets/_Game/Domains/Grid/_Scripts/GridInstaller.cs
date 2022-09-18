using UnityEngine;
using Zenject;

namespace Game
{
    public class GridInstaller : MonoInstaller
    {
        [SerializeField] private BuildingSO buildingSO;
        [SerializeField] private MeshRenderer _gridPlane;
        [Space]
        [SerializeField] private GridSettingsSO _settings;

        public override void InstallBindings()
        {
            PlaneVisual();

            Container.Bind<GridSettings>()
                .FromInstance(_settings.Settings).AsSingle();


            Container.Bind<IGrid<Transform>>().To<Grid<Transform>>()
                .AsSingle().NonLazy();

            //Container.Bind<GridDebug<Transform>>().ToSelf().AsSingle().NonLazy();

            Container.Bind<PlaceHandler<Transform>>().ToSelf().AsSingle()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<PlaceSnapping>().AsSingle().NonLazy();

            Container
                .BindFactory<Object, PlacementBehaviour, PlacementBehaviour.Factory>()
                .FromFactory<PrefabFactory<PlacementBehaviour>>();


            var snapping = Container.Resolve<PlaceSnapping>();
            snapping.SetTransform(buildingSO);

            UnityEngine.SceneManagement.SceneManager.LoadScene("MainUI", UnityEngine.SceneManagement.LoadSceneMode.Additive);

        }

        private void PlaneVisual()
        {
            var _rows = _settings.Settings.Rows;
            var _columns = _settings.Settings.Columns;
            var _cellSize = _settings.Settings.CellSize;

            var scale = new Vector3(_rows, 0, _columns) * _cellSize;
            scale.y = _gridPlane.transform.localScale.y;
            _gridPlane.transform.localScale = scale;
            _gridPlane.material.mainTextureScale = new Vector2(_rows, _columns) / 2f;
        }
    }

}
