using UnityEngine;
using Zenject;

namespace Game
{
    public class PlaceGridObject : MonoBehaviour
    {
        [SerializeField] private Transform _dragTransform;
        [Space]
        [SerializeField] private EditPlaceVisual _editPlaceVisual;

        public bool IsAlreadyPlaced { get; private set; }

        private IPlaceable _placeable;
        private IGrid _grid;

        private int _width;
        private int _height;

        private Vector3 _beforePosition;
        private bool _isPlacing;


        [Inject]
        public void Init(IGrid grid)
        {
            _grid = grid;

            _placeable = _dragTransform.GetComponent<IPlaceable>();
            _width = _placeable.Width;
            _height = _placeable.Height;
        }

        public void StartEditPlacing()
        {
            _beforePosition = _dragTransform.position;

            if (IsAlreadyPlaced)
                _grid.Remove(_beforePosition, _width, _height);

            _editPlaceVisual.ChangeToEditMode(IsAlreadyPlaced);

            _isPlacing = true;
        }
        public void StopEditPlacing()
        {
            if (!IsAlreadyPlaced)
            {
                Destroy();
                return;
            }

            if (!TryPlace())
            {
                _grid.Place(_beforePosition, _width, _height, _placeable);
                _dragTransform.transform.position = _beforePosition;
                PlacedCompleted();

            }
        }
        public bool TryPlace()
        {
            if (_grid.CanPlace(_dragTransform.position, _width, _height))
            {
                _grid.Place(_dragTransform.position, _width, _height, _placeable);
                PlacedCompleted();
                return true;
            }
            return false;
        }
        public void ValidatePlace(int row, int column)
        {
            if (_grid.CanPlace(row, column, _width, _height))
                _editPlaceVisual.SetPlaceAvailbility(true);
            else
                _editPlaceVisual.SetPlaceAvailbility(false);
        }
        public void Place()
        {
            if (_grid.CanPlace(_dragTransform.position, _width, _height))
            {
                _grid.Place(_dragTransform.position, _width, _height, _placeable);
                PlacedCompleted();
            }
        }
        public void Destroy()
        {
            Destroy(_dragTransform.gameObject);
        }

        public void ChangeSelection(bool isSelected)
        {
            if (_isPlacing && !isSelected)
                StopEditPlacing();
        }

        private void PlacedCompleted()
        {
            _isPlacing = false;
            IsAlreadyPlaced = true;
            _editPlaceVisual.ChangeToIdleMode();
        }
    }
}