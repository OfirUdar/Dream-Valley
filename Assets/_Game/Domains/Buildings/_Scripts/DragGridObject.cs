using Game.Camera;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Game
{
    public class DragGridObject : MonoBehaviour
    {
        [SerializeField] private Transform _dragTransform;
        [Space]
        [SerializeField] private EditPlaceVisual _editPlaceVisual;

        private IPlaceable _placeable;
        private IGrid _grid;
        private CamPointerUtility _camPointer;
        private CameraController _camController;

        private int _width;
        private int _height;


        private Vector3 _dragOffsetPosition;
        private Vector3 _beforePosition;
        private bool _isDragging;
        private bool _isPlacing;
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


        private void OnMouseDown()
        {
            if (_isSelected && !_isDragging)
                StartDrag();
        }
        private void OnMouseUp()
        {
            if (_isDragging)
                StopDrag();
        }
        private void OnMouseDrag()
        {
            if (_isDragging)
                SnapHandler();
        }


        public void StartDrag()
        {
            StartEditPlacing();

            _camPointer.RaycastPointer(out Vector3 hitPoint);
            _dragOffsetPosition = hitPoint - _dragTransform.position;

            _camController.SetActive(false);

            _isDragging = true;
        }
        public void StopDrag()
        {
            TryPlace();

            _camController.SetActive(true);

            _isDragging = false;
        }

        public void StartEditPlacing()
        {
            _beforePosition = _dragTransform.position;
            _grid.RemoveObject(_beforePosition, _width, _height);

            _editPlaceVisual.SetEditingVisual(true);

            _isPlacing = true;
        }
        public void StopEditPlacing()
        {
            if (!TryPlace())
            {
                _grid.PlaceObject(_beforePosition, _width, _height, _placeable);
                _dragTransform.transform.position = _beforePosition;
                _editPlaceVisual.SetEditingVisual(false);
                _isPlacing = false;
            }
        }
        public bool TryPlace()
        {
            if (_grid.CanPlace(_dragTransform.position, _width, _height))
            {
                _grid.PlaceObject(_dragTransform.position, _width, _height, _placeable);
                _editPlaceVisual.SetEditingVisual(false);
                _isPlacing = false;

                return true;
            }
            return false;
        }


        private void SnapHandler()
        {
            if (_camPointer.RaycastPointer(out Vector3 hitPoint))
            {
                hitPoint -= _dragOffsetPosition;

                _grid.GetIndexes(hitPoint, out int row, out int column);

                //Keep the object on the grid
                row = Mathf.Clamp(row, 0, _grid.GetRows() - _width);
                column = Mathf.Clamp(column, 0, _grid.GetColumns() - _height);


                if (_grid.CanPlace(row, column, _width, _height))
                    _editPlaceVisual.SetPlaceAvailbility(true);
                else
                    _editPlaceVisual.SetPlaceAvailbility(false);


                var snappedPosition = _grid.GetWorldPosition(row, column);

                _dragTransform.position = snappedPosition;
            }
        }


        public void OnSelectionChanged(bool isSelected)
        {
            _isSelected = isSelected;
            if (_isPlacing && !_isSelected)
                StopEditPlacing();
        }

    }


}
