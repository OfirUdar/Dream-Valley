using UnityEngine;
using Zenject;

namespace Game
{
    public class PlaceSnapping : ITickable
    {
        private readonly IGrid<Transform> _grid;
        private Transform _transform;
        private BuildData _buildData;

        public PlaceSnapping(IGrid<Transform> grid)
        {
            _grid = grid;
        }

        public void SetTransform(Transform transform, BuildData buildData)
        {
            _transform = transform;
            _buildData = buildData;
        }

        public void Tick()
        {
            if (_transform == null)
                return;

            var Plane = new Plane(Vector3.up, Vector3.forward);

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Plane.Raycast(ray, out float point))
            {
                var hitPoint = ray.GetPoint(point);
                _grid.GetIndexes(hitPoint, out int row, out int column);
                row -= _buildData.Width / 2;
                column -= _buildData.Height / 2;

                row = Mathf.Clamp(row, 0, _grid.GetRows() - _buildData.Width);
                column = Mathf.Clamp(column, 0, _grid.GetColumns() - _buildData.Height);

                _transform.position = _grid.GetWorldPosition(row, column);

            }
        }
    }
}