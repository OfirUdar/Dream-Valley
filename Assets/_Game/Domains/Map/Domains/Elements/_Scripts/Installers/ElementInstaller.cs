using UnityEngine;
using Zenject;

namespace Game.Map.Element
{
    public class ElementInstaller : MonoInstaller
    {
        [SerializeField] private MapElementSO _elementSO;
        [Space]
        [SerializeField] private GameObject _elementGameObject;
        [SerializeField] private Transform _idleArea;
        [SerializeField] private MeshRenderer _editAreaRenderer;
        [SerializeField] private BoxCollider _collider;
        [SerializeField] private Transform[] _arrowsArray;

        public override void InstallBindings()
        {
            Container.Bind<MapElementSO>().FromInstance(_elementSO);

            Container.Bind<AreaSizeFitter>().ToSelf().AsSingle()
                .WithArguments(_idleArea, _editAreaRenderer, _collider, _arrowsArray).NonLazy();

            Container.Bind<IMapElement>().To<MapElement>().AsSingle().WithArguments(_elementGameObject);

            Container.Bind<IEventor>().To<Eventor>().AsSingle();
        }
     
    }
}