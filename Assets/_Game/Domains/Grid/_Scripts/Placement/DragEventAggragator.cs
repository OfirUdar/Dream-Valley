using System;

namespace Game
{
    public class DragEventAggragator
    {
        public event Action<PlacementFacade> StartDragRequested;
        public event Action DragRequested;
        public event Action EndDragRequested;

        public void RequestStartDrag(PlacementFacade placementFacade)
        {
            StartDragRequested?.Invoke(placementFacade);
        }
        public void RequestDrag()
        {
            DragRequested?.Invoke();
        }
        public void RequestEndDrag()
        {
            EndDragRequested?.Invoke();
        }
    }
}

