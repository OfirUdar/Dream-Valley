using System;

namespace Game.Map
{
    public interface IDragManager
    {
        public event Action Dragging;
        public event Action<bool> DraggingEnded; //bool - is if it can place or not

        public void Lock(bool isLock);
        public void RequestStartDrag(IMapElement mapElement);
        public void RequestEndDrag();
        public void RequestDrag();

        public void ChangeToExistElementDragger();
        public void ChangeToNewElementDragger(IMapElement mapElement);

    }
}