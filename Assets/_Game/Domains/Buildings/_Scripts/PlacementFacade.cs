using Zenject;

namespace Game
{
    public class PlacementFacade
    {
        public IPlaceable Placeable { get; private set; }
        public ISelectable Selectable { get; private set; }
        [Inject] //injected like this in order to prevent ciruclar dependcanies
        public IDraggable Draggable; 

        public PlacementFacade(IPlaceable placeable, ISelectable selectable)
        {
            Placeable = placeable;
            Selectable = selectable;
        }
    }
}