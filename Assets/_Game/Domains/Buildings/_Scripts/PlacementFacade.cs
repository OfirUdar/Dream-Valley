using Zenject;

namespace Game
{
    public class PlacementFacade
    {
        //Injected like this in order to prevent ciruclar dependcanies

        [Inject]
        public IPlaceable Placeable { get; private set; }
        [Inject]
        public ISelectable Selectable { get; private set; }
        [Inject] 
        public IDraggable Draggable { get; private set; }

        [Inject]
        public IPlaceApprover PlaceApprover{ get; private set; }

    }
}