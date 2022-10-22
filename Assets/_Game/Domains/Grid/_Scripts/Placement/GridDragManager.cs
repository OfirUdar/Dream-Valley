using Game.Camera;
using UnityEngine;
using Zenject;

namespace Game
{
    public class GridDragManager : ITickable
    {
        private readonly IUserInput _input;
        private readonly IGrid _grid;
        private readonly CamPointerUtility _camPointer;
        private readonly CameraController _camController;

        private IDraggable _dragPlace;
        private Vector3 _dragOffsetPosition;
        private bool _isDragging;

        public GridDragManager(IUserInput input,
            IGrid grid,
            CamPointerUtility camPointer,
            CameraController cameraController)
        {
            _input = input;
            _grid = grid;
            _camPointer = camPointer;
            _camController = cameraController;
        }

        public void SetPlacement(IDraggable draggable)
        {
            _dragPlace = draggable;
        }

        public void Tick()
        {
            if(_input.IsPointerDown())
            {
                _dragPlace.StartDrag();
            }
            if (_input.IsPointerPressing())
            {
                if (_camPointer.RaycastPointer(out Vector3 pointerPoint))
                {
                    pointerPoint -= _dragOffsetPosition;

                    //_grid.GetIndexes(pointerPoint, out int row, out int column);

                    //var place = _dragPlace.Place;
                    ////Keep the object on the grid
                    //row = Mathf.Clamp(row, 0, _grid.GetRowsAmount() - place.Width);
                    //column = Mathf.Clamp(column, 0, _grid.GetColumnsAmount() - place.Height);

                    //var snappedPosition = _grid.GetWorldPosition(row, column);
                    //place.Position = snappedPosition;

                    //bool canPlace = _grid.CanPlace(place);

                    //_dragPlace.OnDrag(canPlace);
                }
            }
            if (_input.IsPointerUp())
            {
                _dragPlace.EndDrag();
            }
        }


    }
    public interface IDraggable
    {
        public void StartDrag();
        public void OnDrag(bool canPlace);
        public void EndDrag();
    }
}