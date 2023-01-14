using UnityEngine;
using Zenject;

namespace Game.Map.Element
{
    public class ElementInteractionInstaller : MonoInstaller
    {
        [Header("Settings")]
        [SerializeField] private bool _isDraggable = true;
        [Header("References")]
        [SerializeField] private SelectedVisual _selectedVisual;
        [SerializeField] private EditVisual _editVisual;
        [SerializeField] private PlacerBehaviour _placerBehaviour;
        [Space]
        [SerializeField] private PlaceApprover _placeApprover;
        [SerializeField] private PointerEvents _pointerEvents;

        public override void InstallBindings()
        {

            Container.Bind<ISelectVisual>().FromInstance(_selectedVisual).AsSingle();
            Container.Bind<IEditVisual>().FromInstance(_editVisual).AsSingle();
            Container.Bind<IPlaceApprover>().FromInstance(_placeApprover).AsSingle();

            Container.Bind<IPlaceable>().FromInstance(_placerBehaviour).AsSingle();
            Container.Bind<IDraggable>().To<Dragger>().AsSingle();
            Container.Bind<ISelectable>().To<Selector>().AsSingle();

            Container.Bind<PointerEvents>().FromInstance(_pointerEvents).AsSingle();

            Container.BindInterfacesAndSelfTo<InteractionInvoker>()
                .AsSingle()
                .WithArguments(_isDraggable)
                .NonLazy();
        }
    }
}