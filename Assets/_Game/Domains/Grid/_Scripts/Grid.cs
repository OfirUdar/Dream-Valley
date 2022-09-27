using UnityEngine;

namespace Game
{
    public class Grid : IGrid
    {
        private readonly IPlaceable[,] _cells;

        private readonly int _rows;
        private readonly int _columns;
        private readonly float _cellSize;
        private readonly Vector3 _offset;


        public Grid(GridSettings settings)
        {
            _cells = new IPlaceable[settings.Rows, settings.Columns];

            _rows = settings.Rows;
            _columns = settings.Columns;
            _cellSize = settings.CellSize;

            _offset = new Vector3(_rows, 0, _columns) * _cellSize / 2f;
        }


        public bool IsEmpty(int row, int column)
        {
            if (!IsOnRange(row, column))
                return false;

            return _cells[row, column]==null;
        }
        public bool IsEmpty(Vector3 worldPosition)
        {
            GetIndexes(worldPosition, out int row, out int column);
            return IsEmpty(row, column);
        }

        public bool CanPlace(int row, int column, int width, int height)
        {
            for (int r = row; r < row + width; r++)
            {
                for (int c = column; c < column + height; c++)
                {
                    if (!IsEmpty(r, c))
                        return false;
                }
            }
            return true;
        }
        public bool CanPlace(Vector3 worldPosition, int width, int height)
        {
            GetIndexes(worldPosition, out int row, out int column);
            return CanPlace(row, column, width, height);
        }
        public void PlaceObject(int row, int column, int width, int height, IPlaceable cell)
        {
            for (int r = row; r < row + width; r++)
            {
                for (int c = column; c < column + height; c++)
                {
                   SetValue(r, c, cell);
                }
            }
        }
        public void PlaceObject(Vector3 worldPosition, int width, int height, IPlaceable cell)
        {
            GetIndexes(worldPosition, out int row, out int column);
            PlaceObject(row, column, width, height, cell);
        }
        public void RemoveObject(int row, int column, int width, int height)
        {
            for (int r = row; r < row + width; r++)
            {
                for (int c = column; c < column + height; c++)
                {
                    SetValue(r, c, null);
                }
            }
        }

        public void RemoveObject(Vector3 worldPosition, int width, int height)
        {
            GetIndexes(worldPosition, out int row, out int column);
            RemoveObject(row, column, width, height);
        }
        #region GET
        public int GetRows()
        {
            return _rows;
        }
        public int GetColumns()
        {
            return _columns;
        }
        public void GetIndexes(Vector3 worldPosition, out int row, out int column)
        {
            row = Mathf.FloorToInt((worldPosition.x + _offset.x) / _cellSize);
            column = Mathf.FloorToInt((worldPosition.z + _offset.z) / _cellSize);
        }
        public Vector2Int GetIndexes(Vector3 worldPosition)
        {
            GetIndexes(worldPosition, out int row, out int column);
            return new Vector2Int(row, column);
        }
        public IPlaceable GetValue(int row, int column)
        {
            if (IsOnRange(row, column))
                return _cells[row, column];

            return default;
        }
        public IPlaceable GetValue(Vector3 worldPosition)
        {
            GetIndexes(worldPosition, out int row, out int column);
            return _cells[row, column];
        }
        public Vector3 GetWorldPosition(int row, int column)
        {
            return new Vector3(row, 0, column) * _cellSize - _offset;
        }

        public Vector3 WorldPositionToGridPosition(Vector3 worldPosition)
        {
            GetIndexes(worldPosition, out int row, out int column);
            return GetWorldPosition(row, column);
        }
        #endregion

        #region SET

        /// <summary>
        /// Setting value by indexes, returns true if set was sucessed
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetValue(int row, int column, IPlaceable value)
        {
            if (IsOnRange(row, column))
            {
                _cells[row, column] = value;
                return true;
            }
            return false;
        }
        /// <summary>
        /// Setting value by worldPosition, returns true if set was sucessed
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetValue(Vector3 worldPosition, IPlaceable value)
        {
            GetIndexes(worldPosition, out int row, out int column);
            Debug.Log($"row= {row} column= {column}");
            return SetValue(row, column, value);
        }

        #endregion

        private bool IsOnRange(int row, int column)
        {
            return row >= 0 && column >= 0 && row < _rows && column < _columns;
        }

       
    }
}

