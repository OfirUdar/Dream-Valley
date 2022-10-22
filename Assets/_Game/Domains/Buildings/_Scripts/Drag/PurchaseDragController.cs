using Game.Camera;
using UnityEngine;
using Zenject;

namespace Game
{
    public class PurchaseDragController : IInitializable, ILateDisposable
    {
        private readonly IGrid _grid;
        private readonly CameraController _camController;
        private readonly CamPointerUtility _camPointer;
        private readonly DragEventAggragator _dragEventAggragator;

        private Vector3 _offsetPosition;
        private Vector3 _placementStartPosition;
        private PlacementFacade _dragPlacement;


        public PurchaseDragController(IGrid grid,
            CameraController camController,
            CamPointerUtility camPointer,
            DragEventAggragator dragEventAggragator)
        {
            _grid = grid;
            _camController = camController;
            _camPointer = camPointer;
            _dragEventAggragator = dragEventAggragator;

        }

        public void Initialize()
        {
            _dragEventAggragator.StartDragRequested += OnStartDragRequested;
            _dragEventAggragator.EndDragRequested += OnEndDragRequested;
            _dragEventAggragator.DragRequested += OnDragRequested;
        }

        public void LateDispose()
        {
            _dragEventAggragator.StartDragRequested -= OnStartDragRequested;
            _dragEventAggragator.EndDragRequested -= OnEndDragRequested;
            _dragEventAggragator.DragRequested -= OnDragRequested;
        }
        public void OnStartDragRequested(PlacementFacade placeableFacade)
        {
            if (placeableFacade.Selectable.IsSelected)
            {
                StartDrag(placeableFacade);
            }

        }
        public void OnEndDragRequested()
        {
            if (_dragPlacement == null)
                return;
            EndDrag();
        }
        public void OnDragRequested()
        {
            if (_dragPlacement == null)
                return;
            Drag();
        }



        private void StartDrag(PlacementFacade placeableFacade)
        {
            _dragPlacement = placeableFacade;
            var placement = _dragPlacement.Placeable;

            _placementStartPosition = placement.Position;

            _camController.SetActive(false);
            _offsetPosition = CalcaulateOffsetPosition(placement);
            _grid.Remove(placement);

            placeableFacade.Draggable.StartDrag();
        }
        private void EndDrag()
        {
            _camController.SetActive(true);
            _dragPlacement = null;
        }
        private void Drag()
        {
            if (_camPointer.RaycastPointer(out Vector3 pointerPoint))
            {
                pointerPoint -= _offsetPosition;

                _grid.GetIndexes(pointerPoint, out int row, out int column);

                var placement = _dragPlacement.Placeable;

                //Keep the object on the grid
                row = Mathf.Clamp(row, 0, _grid.GetRowsAmount() - placement.Width);
                column = Mathf.Clamp(column, 0, _grid.GetColumnsAmount() - placement.Height);

                var snappedPosition = _grid.GetWorldPosition(row, column);
                placement.Position = snappedPosition;

                var canPlace = _grid.CanPlace(placement);
                _dragPlacement.Draggable.OnDrag(canPlace);
            }
        }

        private Vector3 CalcaulateOffsetPosition(IPlaceable placeable)
        {
            if (_camPointer.RaycastPointer(out Vector3 pointerPoint))
                return pointerPoint - placeable.Position;
            return Vector3.zero;
        }

    }
}