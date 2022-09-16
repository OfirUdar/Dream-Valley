using UnityEngine;

namespace Game
{
    public class Grid<TCell> : IGrid<TCell>
    {
        private readonly TCell[,] _cells;

        private readonly int _rows;
        private readonly int _columns;
        private readonly float _cellSize;
        private readonly Vector3 _offset;


        public Grid(int rows, int columns, float cellSize)
        {
            _cells = new TCell[rows, columns];

            _rows = rows;
            _columns = columns;
            _cellSize = cellSize;

            _offset = new Vector3(rows, 0, columns) * _cellSize / 2f;
        }


        public bool IsEmpty(int row, int column)
        {
            if (!IsOnRange(row, column))
                return false;

            return _cells[row, column].Equals(default(TCell));
        }
        public bool IsEmpty(Vector3 worldPosition)
        {
            GetIndexes(worldPosition, out int row, out int column);
            return IsEmpty(row, column);
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
        public TCell GetValue(int row, int column)
        {
            if (IsOnRange(row, column))
                return _cells[row, column];

            return default;
        }
        public TCell GetValue(Vector3 worldPosition)
        {
            GetIndexes(worldPosition, out int row, out int column);
            return _cells[row, column];
        }
        public Vector3 GetWorldPosition(int row, int column)
        {
            return new Vector3(row, 0, column) * _cellSize - _offset;
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
        public bool SetValue(int row, int column, TCell value)
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
        public bool SetValue(Vector3 worldPosition, TCell value)
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
    public interface IGrid<TCell>
    {
        public bool IsEmpty(int row, int column);
        public bool IsEmpty(Vector3 worldPosition);

        #region GET
        public int GetRows();
        public int GetColumns();
        public void GetIndexes(Vector3 worldPosition, out int row, out int column);
        public Vector2Int GetIndexes(Vector3 worldPosition);
        public TCell GetValue(int row, int column);
        public TCell GetValue(Vector3 worldPosition);
        public Vector3 GetWorldPosition(int row, int column);
        #endregion

        #region SET
        public bool SetValue(int row, int column, TCell value);
        public bool SetValue(Vector3 worldPosition, TCell value);
        #endregion
    }
}

