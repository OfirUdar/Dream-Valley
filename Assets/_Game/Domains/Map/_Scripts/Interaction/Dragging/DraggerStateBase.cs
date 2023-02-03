using UnityEngine;
using Zenject;

namespace Game.Map
{
    public abstract class DraggerStateBase : IInitializable, ILateDisposable
    {
        protected readonly IMapGrid _grid;
        private readonly ICameraController _cameraController;
        private readonly ICameraPointerUtility _camPointerUtility;
        protected readonly ISelectionManager _selectionManager;

        private Vector3 _offsetPosition;
        protected IMapElement _currentElement;

        [Inject(Id = "Dragging")] private readonly IEventCommand _draggingEventCommand;
        [Inject(Id = "Placed")] protected readonly IEventCommand _dragEndPlacedEventCommand;
        [Inject(Id = "Placed_Error")] protected readonly IEventCommand _dragEndPlacedErrorEventCommand;


        protected abstract bool CanStartDrag(IMapElement mapElement);
        public abstract void OnDragStarted();
        public abstract void OnDragEnded(bool hasPlaced);
        public abstract void OnCanceled();

        public DraggerStateBase(IMapGrid grid, ICameraController cameraController,
            ICameraPointerUtility camPointerUtility,
            ISelectionManager selectionManager)
        {
            _grid = grid;
            _cameraController = cameraController;
            _camPointerUtility = camPointerUtility;
            _selectionManager = selectionManager;
        }


        public virtual void RequestStartDrag(IMapElement mapElement)
        {
            if (CanStartDrag(mapElement))
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
            OnDragEnded(canPlace);

            _cameraController.SetActive(true);
        }
        public void RequestDrag()
        {
            if (_currentElement == null)
                return;

            var snappedPosition = GetSnappedPosition();

            if ((snappedPosition - _currentElement.Position).sqrMagnitude > 0.1f)
                _draggingEventCommand.Execute();


            var canPlace = _grid.CanPlace(snappedPosition, _currentElement.Width, _currentElement.Height, _currentElement);

            _currentElement.Position = Vector3.Lerp(_currentElement.Position, snappedPosition, 20f * Time.deltaTime);
            _currentElement.OnDrag(canPlace);
        }




        private Vector3 GetSnappedPosition()
        {
            _camPointerUtility.InputRaycast(out Vector3 worldPosition);

            worldPosition -= _offsetPosition; //touch offset
            var indexes = _grid.GetIndexes(worldPosition);

            indexes.x = Mathf.Clamp(indexes.x, 0, _grid.GetRowsAmount() - _currentElement.Width);
            indexes.y = Mathf.Clamp(indexes.y, 0, _grid.GetColumnsAmount() - _currentElement.Height);

            var snapPosition = _grid.GetWorldPosition(indexes.x, indexes.y);

            return snapPosition;
        }

        private Vector3 CalculateOffsetPosition(Vector3 elementPosition)
        {
            _camPointerUtility.InputRaycast(out Vector3 worldPosition);

            return worldPosition - elementPosition;
        }

        protected void Cancel()
        {
            OnCanceled();
            _currentElement = null;
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
                {
                    Cancel();
                }
            }
        }
    }

}