using UnityEngine;
using Zenject;

namespace Game.Placement
{
    public class PlacementInstaller : MonoInstaller<PlacementInstaller>
    {
        [SerializeField] private PlacementSO _placementSO;
        [SerializeField] private PlacementBehaviour _placementBehaviour;
        [SerializeField] private SelectTrigger _selectTrigger;
        [SerializeField] private DragTrigger _dragTrigger;

        public override void InstallBindings()
        {
            Container.Bind<PlacementFacade>().ToSelf().AsSingle().NonLazy();
            Container.BindInstance(_placementSO).AsSingle();


            Container.Bind<IPlaceable>().FromInstance(_placementBehaviour).AsSingle();
            Container.Bind<ISelectable>().FromInstance(_selectTrigger).AsSingle();
            Container.Bind<IDraggable>().FromInstance(_dragTrigger).AsSingle();
           
        }

    }
}