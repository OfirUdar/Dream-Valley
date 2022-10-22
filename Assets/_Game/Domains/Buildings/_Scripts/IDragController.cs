namespace Game
{
    public interface IDragController
    {
        public void RequestStartDrag(PlacementFacade placeable);
        public void RequestDrag();
        public void RequestEndDrag();
    }
}