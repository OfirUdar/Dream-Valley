namespace Game.Map
{
    public class DragManager : IDragManager
    {
        private readonly ExistDragState _existElementDragger;
        private readonly NewDragState _newElementDragger;


        private DraggerStateBase _currentState;

        public DragManager(ExistDragState existElementDragger,
            NewDragState newElementDragger)
        {
            _existElementDragger = existElementDragger;
            _newElementDragger = newElementDragger;

            ChangeToExistElementDragger();
        }

        public void RequestStartDrag(IMapElement mapElement)
        {
            _currentState.RequestStartDrag(mapElement);
        }
        public void RequestEndDrag()
        {
            _currentState.RequestEndDrag();
        }
        public void RequestDrag()
        {
            _currentState.RequestDrag();
        }


        public void ChangeToExistElementDragger()
        {
            _currentState = _existElementDragger;
        }
        public void ChangeToNewElementDragger()
        {
            _currentState = _newElementDragger;
        }
    }

}