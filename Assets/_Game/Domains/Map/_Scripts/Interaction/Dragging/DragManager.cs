using System;

namespace Game.Map
{
    public class DragManager : IDragManager
    {
        private readonly ExistDragState _existElementDragger;
        private readonly NewDragState _newElementDragger;


        private DraggerStateBase _currentState;
        private bool _isLock;

        public event Action Dragging;
        public event Action<bool> DraggingEnded;


        public DragManager(ExistDragState existElementDragger,
            NewDragState newElementDragger)
        {
            _existElementDragger = existElementDragger;
            _newElementDragger = newElementDragger;

            ChangeToExistElementDragger();
        }

        public void RequestStartDrag(IMapElement mapElement)
        {
            if (_isLock)
                return;

            _currentState.RequestStartDrag(mapElement);
        }
        public void RequestEndDrag()
        {
            if (_isLock)
                return;

            _currentState.RequestEndDrag();
        }
        public void RequestDrag()
        {
            if (_isLock)
                return;

            _currentState.RequestDrag();
        }

        public void ChangeToExistElementDragger()
        {
            _currentState = _existElementDragger;
        }
        public void ChangeToNewElementDragger(IMapElement mapElement)
        {
            _currentState = _newElementDragger;
            _newElementDragger.SetMapElement(mapElement);
        }

        public void Lock(bool isLock)
        {
            _isLock = isLock;
        }
    }

}