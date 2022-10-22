using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class BuildingsManager : IPlacer
    {
        private readonly IGrid _grid;

        private readonly List<IPlaceable> _buildingsList = new List<IPlaceable>();

        public BuildingsManager(IGrid grid)
        {
            _grid = grid;
        }

        public bool CanPlace(IPlaceable cell)
        {
            return _grid.CanPlace(cell);
        }

        public bool CanPlace(int row, int column, int width, int height)
        {
            return _grid.CanPlace(row, column, width, height);
        }

        public bool CanPlace(Vector3 worldPosition, int width, int height)
        {
            return _grid.CanPlace(worldPosition, width, height);
        }

        public void Place(IPlaceable cell)
        {
            if (!_buildingsList.Contains(cell))
                _buildingsList.Add(cell);

            _grid.Place(cell);
        }
        public void Place(Vector3 worldPosition, int width, int height, IPlaceable cell)
        {
            if (!_buildingsList.Contains(cell))
                _buildingsList.Add(cell);

            _grid.Place(worldPosition, width, height, cell);
        }
        public void Place(int row, int column, int width, int height, IPlaceable cell)
        {
            if (!_buildingsList.Contains(cell))
                _buildingsList.Add(cell);

            _grid.Place(row, column, width, height, cell);
        }


        public void Remove(IPlaceable cell)
        {
            if (_buildingsList.Contains(cell))
                _buildingsList.Remove(cell);

            _grid.Remove(cell);
        }
        public void Remove(Vector3 worldPosition, int width, int height)
        {
            _grid.GetIndexes(worldPosition, out int row, out int column);
            Remove(row, column, width, height);
        }

        public void Remove(int row, int column, int width, int height)
        {
            var cell = _grid.GetValue(row, column);
            if (_buildingsList.Contains(cell))
                _buildingsList.Remove(cell);

            _grid.Remove(cell);
        }
    }
}