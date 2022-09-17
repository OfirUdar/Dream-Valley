using Udar;
using UnityEngine;
using Zenject;

namespace Game
{
    public class PlaceSnapping : ITickable
    {
        private readonly IUserInput _input;
        private readonly IGrid<Transform> _grid;
        private readonly PlaceHandler<Transform> _placeHandler;
        private readonly PlacementBehaviour.Factory _placementFactory;

        private Transform _ghost;
        private BuildingSO _placementSO;

        public PlaceSnapping(IUserInput userInput,
            IGrid<Transform> grid,
            PlaceHandler<Transform> placeHandler,
            PlacementBehaviour.Factory placementFactory)
        {
            _input = userInput;
            _grid = grid;
            _placeHandler = placeHandler;
            _placementFactory = placementFactory;
        }

        public void SetTransform(BuildingSO buildData)
        {
            _placementSO = buildData;
            _ghost = _placementFactory.Create(_placementSO.Pfb).transform;
        }

        public void Tick()
        {
            if (_ghost == null)
                return;

            SnapHandler();

            if (_input.IsPointerDown())
            {

                var pos = _ghost.position;

                if (_placeHandler.CanPlace(pos, _placementSO.Data.Width, _placementSO.Data.Height))
                {
                    var ob = _placementFactory.Create(_placementSO.Pfb).transform;
                    _placeHandler.PlaceObject(pos, _placementSO.Data.Width, _placementSO.Data.Height, ob);
                    ob.transform.position = pos;
                }
                else
                {
                    Debug.Log("Cannot");
                }



            }
        }

        private void SnapHandler()
        {
            var plane = new Plane(Vector3.up, Vector3.forward);

            var ray = CameraUtils.Cam.ScreenPointToRay(_input.GetPointerPosition());
            var buildData = _placementSO.Data;

            if (plane.Raycast(ray, out float point))
            {
                var hitPoint = ray.GetPoint(point);
                _grid.GetIndexes(hitPoint, out int row, out int column);

                //Substruct some offset in order to center the pointer
                row -= buildData.Width / 2;
                column -= buildData.Height / 2;

                //Keep the object on the grid
                row = Mathf.Clamp(row, 0, _grid.GetRows() - buildData.Width);
                column = Mathf.Clamp(column, 0, _grid.GetColumns() - buildData.Height);

                _ghost.position = _grid.GetWorldPosition(row, column);

            }
        }
    }
}