using UnityEngine;

namespace Game.Map
{
    public class ExistDragState : DraggerStateBase
    {
        private Vector3 _startPosition;

        public ExistDragState(IMapGrid grid, ICameraController cameraController, CamPointerUtility camPointerUtility, ISelectionManager selectionManager) : base(grid, cameraController, camPointerUtility, selectionManager)
        {
        }

        public override void OnDragStarted()
        {
            _startPosition = _currentElement.Position;
            //_grid.Remove(_currentElement);
        }

        public override void OnDragEnded(bool canPlace)
        {
            _currentElement.EndDrag(canPlace);

            if (canPlace)
            {
                _grid.Remove(_startPosition, _currentElement.Width, _currentElement.Height); //removing from the old
                _grid.Place(_currentElement);
                _currentElement = null;
            }

        }
        public override void OnCanceled()
        {
            _currentElement.Position = _startPosition;
            //_grid.Place(_currentElement);
            _currentElement.EndDrag(true);
        }

        protected override bool CanStartDrag(IMapElement mapElement)
        {
            return mapElement.IsSelected;
        }
    }

}