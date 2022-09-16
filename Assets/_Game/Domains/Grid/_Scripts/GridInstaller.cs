using UnityEngine;
using Zenject;

namespace Game
{
    public class GridInstaller : MonoInstaller
    {
        [SerializeField] private BuildingSO buildingSO;
        [SerializeField] private MeshRenderer _gridPlane;
        [Space]
        [SerializeField] private int _rows;
        [SerializeField] private int _columns;
        [SerializeField] private float _cellSize;

        public override void InstallBindings()
        {
            var scale = new Vector3(_rows, 0, _columns) * _cellSize;
            scale.y = _gridPlane.transform.localScale.y;
            _gridPlane.transform.localScale = scale;
            _gridPlane.material.mainTextureScale = new Vector2(_rows, _columns) /2f;

            Container.Bind<IGrid<Transform>>().To<Grid<Transform>>().AsSingle()
                .WithArguments(_rows, _columns, _cellSize).NonLazy();

            Container.Bind<GridDebug<Transform>>().ToSelf().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<PlaceSnapping>().AsSingle().NonLazy();
            var ob = Instantiate(buildingSO.Pfb);
            var snapping = Container.Resolve<PlaceSnapping>();
            snapping.SetTransform(ob.transform, buildingSO.BuildData);

            //Container.BindInterfacesAndSelfTo<PlaceHandler>().AsSingle()
            //    .WithArguments(buildingSO).NonLazy();

        }
    }

}
