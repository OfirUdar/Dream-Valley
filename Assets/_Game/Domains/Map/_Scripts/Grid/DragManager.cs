using Udar;
using UnityEngine;

namespace Game.Map
{
    public class DragManager
    {
        private readonly IMapGrid _grid;
        private readonly ICameraController _cameraController;
        private readonly CamPointerUtility _camPointerUtility;

        private Vector3 _startPosition;
        private IMapElement _currentElement;
        private Vector3 _offsetPosition;

        public DragManager(IMapGrid grid, ICameraController cameraController, CamPointerUtility camPointerUtility)
        {
            _grid = grid;
            _cameraController = cameraController;
            _camPointerUtility = camPointerUtility;
        }

        public void RequestStartDrag(IMapElement mapElement)
        {
            if (mapElement.IsSelected)
            {
                _startPosition = mapElement.Position;
                _offsetPosition = CalculateOffsetPosition(mapElement.Position);

                _currentElement = mapElement;
                _currentElement.StartDrag();
                _grid.Remove(_currentElement);
                _cameraController.SetActive(false);
            }
        }
        public void RequestEndDrag()
        {
            if (_currentElement == null)
                return;

            var snappedPosition = GetSnappedPosition();

            _currentElement.Position = snappedPosition;

            var canPlace = _grid.CanPlace(_currentElement);
            _currentElement.EndDrag(canPlace);

            if (canPlace)
            {
                _grid.Place(_currentElement);
                _currentElement = null;
            }

            _cameraController.SetActive(true);

        }
        public void RequestDrag()
        {
            if (_currentElement == null)
                return;

            var snappedPosition = GetSnappedPosition();

            var canPlace = _grid.CanPlace(snappedPosition, _currentElement.Width, _currentElement.Height);

            _currentElement.Position = Vector3.Lerp(_currentElement.Position, snappedPosition, 15f * Time.deltaTime);
            _currentElement.OnDrag(canPlace);
        }


        private Vector3 GetSnappedPosition()
        {
            _camPointerUtility.RaycastPointer(out Vector3 worldPosition);

            worldPosition -= _offsetPosition; //touch offset
            var indexes = _grid.GetIndexes(worldPosition);

            indexes.x = Mathf.Clamp(indexes.x, 0, _grid.GetRowsAmount() - _currentElement.Width);
            indexes.y = Mathf.Clamp(indexes.y, 0, _grid.GetColumnsAmount() - _currentElement.Height);

            var snapPosition = _grid.GetWorldPosition(indexes.x, indexes.y);

            return snapPosition;
        }

        private Vector3 CalculateOffsetPosition(Vector3 elementPosition)
        {
            _camPointerUtility.RaycastPointer(out Vector3 worldPosition);

            return worldPosition - elementPosition;
        }

    }
}