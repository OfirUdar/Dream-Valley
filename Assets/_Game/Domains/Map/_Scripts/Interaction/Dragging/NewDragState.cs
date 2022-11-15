namespace Game.Map
{

    //For new element
    public class NewDragState : DraggerStateBase
    {
        public NewDragState(IMapGrid grid, ICameraController cameraController, CamPointerUtility camPointerUtility, SelectionManager selectionManager) : base(grid, cameraController, camPointerUtility, selectionManager)
        {
        }
        public override void OnDragStarted()
        {
        }

        public override void OnDragEnded(bool hasPlaced)
        {

        }

        public override void OnCanceled()
        {
            _currentElement.Destroy();
        }
    }

}