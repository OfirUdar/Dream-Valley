namespace Game.Map
{
    public interface IDragManager
    {
        public void RequestStartDrag(IMapElement mapElement);
        public void RequestEndDrag();
        public void RequestDrag();

        public void ChangeToExistElementDragger();
        public void ChangeToNewElementDragger();

    }

}