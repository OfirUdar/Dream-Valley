using UnityEngine;

namespace Game
{
    public class PlaceHandler<TCell>
    {
        private readonly IGrid<TCell> _grid;

        public PlaceHandler(IGrid<TCell> grid)
        {
            _grid = grid;
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
        public bool CanPlace(Vector3 worldPosition, int width, int height)
        {
            _grid.GetIndexes(worldPosition, out int row, out int column);
            return CanPlace(row, column, width, height);
        }
        public void RemoveObject(int row, int column, int width, int height)
        {
            for (int r = row; r < row + width; r++)
            {
                for (int c = column; c < column + height; c++)
                {
                    _grid.SetValue(r, c, default(TCell));
                }
            }
        }
        public void PlaceObject(int row, int column, int width, int height, TCell cell)
        {
            for (int r = row; r < row + width; r++)
            {
                for (int c = column; c < column + height; c++)
                {
                    _grid.SetValue(r, c, cell);
                }
            }
        }
        public void PlaceObject(Vector3 worldPosition, int width, int height, TCell cell)
        {
            _grid.GetIndexes(worldPosition, out int row, out int column);
            PlaceObject(row, column, width, height, cell);
        }

        public bool TryPlace(int row, int column, int width, int height, TCell cell)
        {
            if (!CanPlace(row, column, width, height))
                return false;

            PlaceObject(row, column, width, height, cell);
            return true;
        }
        public bool TryPlace(Vector3 worldPosition, int width, int height, TCell cell)
        {
            _grid.GetIndexes(worldPosition, out int row, out int column);
            return TryPlace(row, column, width, height, cell);
        }
    }
}