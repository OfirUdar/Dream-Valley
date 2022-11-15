using UnityEngine;
using Zenject;

namespace Game.Map
{
    public abstract class DraggerStateBase : IInitializable, ILateDisposable
    {
        protected readonly IMapGrid _grid;
        private readonly ICameraController _cameraController;
        private readonly CamPointerUtility _camPointerUtility;
        private readonly SelectionManager _selectionManager;

        private Vector3 _offsetPosition;

        protected IMapElement _currentElement;


        public abstract void OnDragStarted();
        public abstract void OnDragEnded(bool hasPlaced);
        public abstract void OnCanceled();

        public DraggerStateBase(IMapGrid grid, ICameraController cameraController,
            CamPointerUtility camPointerUtility,
            SelectionManager selectionManager)
        {
            _grid = grid;
            _cameraController = cameraController;
            _camPointerUtility = camPointerUtility;
            _selectionManager = selectionManager;
        }


        public void RequestStartDrag(IMapElement mapElement)
        {
            if (mapElement.IsSelected)
            {
                _offsetPosition = CalculateOffsetPosition(mapElement.Position);

                if (_currentElement == null)//prevent start drag when already stored
                {
                    _currentElement = mapElement;
                    _currentElement.StartDrag();
                    OnDragStarted();
                }
               
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
                OnDragEnded(canPlace);
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

        public void Initialize()
        {
            _selectionManager.SelectionChanged += OnSelectionChanged;
        }

        public void LateDispose()
        {
            _selectionManager.SelectionChanged -= OnSelectionChanged;
        }

        private void OnSelectionChanged(ISelectable selection)
        {
            if (selection == null || selection != _currentElement)
            {
                if (_currentElement != null)
                    OnCanceled();
            }
        }
    }

}