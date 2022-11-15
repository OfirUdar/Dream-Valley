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
        [Space]
        [SerializeField] private SelectedVisual _selectedVisual;
        [SerializeField] private EditVisual _editVisual;
        [Space]
        [SerializeField] private PlacerBehaviour _placerBehaviour;
        [SerializeField] private PointerEvents _pointerEvents;



        public override void InstallBindings()
        {
            Container.BindFactory
               <Object, MapElementBehaviour, MapElementBehaviour.Factory>()
               .FromFactory<PrefabFactory<MapElementBehaviour>>();

            Container.Bind<MapElementData>().FromInstance(_elementSO.Data);

            Container.Bind<AreaSizeFitter>().ToSelf().AsSingle()
                .WithArguments(_idleArea, _editAreaRenderer, _collider).NonLazy();

            Container.Bind<IMapElement>().To<MapElement>().AsSingle().WithArguments(_elementGameObject);

            InstallInteraction();
        }

        private void InstallInteraction()
        {
            Container.Bind<ISelectVisual>().FromInstance(_selectedVisual).AsSingle();
            Container.Bind<IEditVisual>().FromInstance(_editVisual).AsSingle();

            Container.Bind<IPlaceable>().FromInstance(_placerBehaviour).AsSingle();
            Container.Bind<IDraggable>().To<Dragger>().AsSingle();
            Container.Bind<ISelectable>().To<Selector>().AsSingle();

            Container.Bind<PointerEvents>().FromInstance(_pointerEvents).AsSingle();

            Container.BindInterfacesAndSelfTo<InteractionCaller>().AsSingle().NonLazy();
        }
    }
}