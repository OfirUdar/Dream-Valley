namespace Game.Map
{
    public interface IDragManager
    {
        public void Lock(bool isLock);
        public void RequestStartDrag(IMapElement mapElement);
        public void RequestEndDrag();
        public void RequestDrag();

        public void ChangeToExistElementDragger();
        public void ChangeToNewElementDragger(IMapElement mapElement);

    }

}