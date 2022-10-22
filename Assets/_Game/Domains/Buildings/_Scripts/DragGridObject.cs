using Game.Camera;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Game
{
    public class DragGridObject : MonoBehaviour
    {
        [SerializeField] private Transform _dragTransform;
        [Space]
        [SerializeField] private PlaceGridObject _placeGridObject;

        private IPlaceable _placeable;
        private IGrid _grid;
        private CamPointerUtility _camPointer;
        private CameraController _camController;

        private int _width;
        private int _height;

        private Vector3 _dragOffsetPosition;
        private bool _isDragging;
        private bool _isSelected;

        [Inject]
        public void Init(IGrid grid, CamPointerUtility camPointer, CameraController cameraController)
        {
            _grid = grid;
            _camPointer = camPointer;
            _camController = cameraController;

            _placeable = _dragTransform.GetComponent<IPlaceable>();
            _width = _placeable.Width;
            _height = _placeable.Height;
        }


        //private void OnMouseDown()
        //{
        //    if (_isSelected && !_isDragging)
        //        StartDrag();
        //}
        //private void OnMouseUp()
        //{
        //    if (_isDragging)
        //        StopDrag();
        //}
        //private void OnMouseDrag()
        //{
        //    if (_isDragging)
        //        SnapToPointer();
        //}


        public void StartDrag()
        {
            _placeGridObject.StartEditPlacing();

            _camPointer.RaycastPointer(out Vector3 pointerPoint);
            _dragOffsetPosition = pointerPoint - _dragTransform.position;

            _camController.SetActive(false);

            _isDragging = true;
        }
        public void StopDrag()
        {
            if (_placeGridObject.IsAlreadyPlaced)
                _placeGridObject.TryPlace();

            _camController.SetActive(true);

            _isDragging = false;
        }
        public void SnapToPointer()
        {
            if (_camPointer.RaycastPointer(out Vector3 pointerPoint))
            {
                pointerPoint -= _dragOffsetPosition;

                _grid.GetIndexes(pointerPoint, out int row, out int column);

                //Keep the object on the grid
                row = Mathf.Clamp(row, 0, _grid.GetRowsAmount() - _width);
                column = Mathf.Clamp(column, 0, _grid.GetColumnsAmount() - _height);

                var snappedPosition = _grid.GetWorldPosition(row, column);
                _dragTransform.position = snappedPosition;

                _placeGridObject.ValidatePlace(row, column);
            }
        }

        public void OnSelectionChanged(bool isSelected)
        {
            _isSelected = isSelected;
        }

    }


}
