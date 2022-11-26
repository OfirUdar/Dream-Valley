namespace Game.Map
{

    //For new element
    public class NewDragState : DraggerStateBase
    {

        public NewDragState(IMapGrid grid,
            ICameraController cameraController,
            CamPointerUtility camPointerUtility,
            ISelectionManager selectionManager) : base(grid, cameraController, camPointerUtility, selectionManager)
        {


        }
        public override void OnDragStarted()
        {
            _currentElement.PlaceApprover.SubscribeForCallbacks(OnApproveRequested, OnCancelRequested);
        }

        public override void OnDragEnded(bool canPlace)
        {
            _currentElement.EndDrag(false);
        }

        public override void OnCanceled()
        {
        }

        private void OnApproveRequested()
        {
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