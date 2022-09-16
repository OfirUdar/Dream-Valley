using UnityEngine;
using Zenject;

namespace Game
{
    public class PlaceHandler : ITickable
    {
        private readonly IGrid<int> _grid;
        private readonly BuildingSO _building;

        public PlaceHandler(IGrid<int> grid,BuildingSO building)
        {
            _grid = grid;
            _building = building;
        }
        public void Tick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var Plane = new Plane(Vector3.up, Vector3.forward);

                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Plane.Raycast(ray, out float point))
                {
                    var hitPoint = ray.GetPoint(point);
                    PlaceObject(hitPoint, _building.BuildData);
                } 
            }
            if (Input.GetMouseButtonDown(1))
            {
                var Plane = new Plane(Vector3.up, Vector3.forward);

                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Plane.Raycast(ray, out float point))
                {
                    var value = _grid.GetValue
                    (ray.GetPoint(point));
                    Debug.Log(value);
                }
            }
        }

        public bool CanPlace(int row, int column, int width, int height)
        {
            for (int r = row; r < row + width; r++)
            {
                for (int c = column; c < column + height; c++)
                {
                    if (!_grid.IsEmpty(r, c))
                        return false;
                }
            }
            return true;
        }


        public void RemoveObject(int row, int column, BuildData buildData)
        {
            for (int r = row; r < row + buildData.Width; r++)
            {
                for (int c = column; c < column + buildData.Height; c++)
                {
                    _grid.SetValue(r, c, 0);
                }
            }
        }

        public void PlaceObject(int row, int column, BuildData buildData)
        {
            if(!CanPlace(row,column,buildData.Width,buildData.Height))
            {
                Debug.Log("Cannot place");
                return;
            }

            for (int r = row; r < row + buildData.Width; r++)
            {
                for (int c = column; c < column + buildData.Height; c++)
                {
                    _grid.SetValue(r, c, 50);
                }
            }
        }
        public void PlaceObject(Vector3 worldPosition, BuildData buildData)
        {
            _grid.GetIndexes(worldPosition, out int row, out int column);
            PlaceObject(row, column, buildData);
        }
    }
}