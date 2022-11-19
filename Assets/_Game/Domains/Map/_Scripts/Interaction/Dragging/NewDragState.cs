using Zenject;

namespace Game.Map
{

    //For new element
    public class NewDragState : DraggerStateBase
    {
        [Inject]
        private readonly IDragManager _dragManager;

        public NewDragState(IMapGrid grid,
            ICameraController cameraController,
            CamPointerUtility camPointerUtility,
            SelectionManager selectionManager) : base(grid, cameraController, camPointerUtility, selectionManager)
        {


        }
        public override void OnDragStarted()
        {
           // _selectionManager.Lock(true);
            _currentElement.PlaceApprover.SubscribeForCallbacks(OnApproveRequested, OnCancelRequested);
        }

        public override void OnDragEnded(bool canPlace)
        {
            _currentElement.EndDrag(false);
        }

        public override void OnCanceled()
        {
           // _currentElement.Destroy();
            //_selectionManager.Lock(false);
            //_dragManager.ChangeToExistElementDragger();
            //MainUIEventAggregator.Show();
        }

        private void OnApproveRequested()
        {
            //_currentElement.PlaceApprover.Hide();
            //_selectionManager.Lock(false);
            //_dragManager.ChangeToExistElementDragger();
            //MainUIEventAggregator.Show();


            _grid.Place(_currentElement);
            _currentElement.EndDrag(true);
            _currentElement = null;

        }
        private void OnCancelRequested()
        {
            Cancel();
        }
    }

}