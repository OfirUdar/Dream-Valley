using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Map.Grid
{
    public class MapGrid : IMapGrid
    {

        private readonly IMapElement[,] _cells;

        private readonly int _rows;
        private readonly int _columns;
        private readonly float _cellSize;
        private readonly Vector3 _offset;

        public IMapElement[,] Cells => _cells;

        public event Action<IMapElement> ElementChanged;
        public event Action<IMapElement> ElementRemoved;

        public MapGrid(GridSettingsSO settings)
        {
            _cells = new IMapElement[settings.Rows, settings.Columns];

            _rows = settings.Rows;
            _columns = settings.Columns;
            _cellSize = settings.CellSize;

            _offset = new Vector3(_rows, 0, _columns) * _cellSize / 2f;
        }


        public bool IsEmpty(int row, int column)
        {
            return _cells[row, column] == null;
        }
        public bool IsEmpty(Vector3 worldPosition)
        {
            GetIndexes(worldPosition, out int row, out int column);
            return IsEmpty(row, column);
        }

        public bool CanPlace(int row, int column, int width, int height, IMapElement element)
        {
            for (int r = row; r < row + width; r++)
            {
                for (int c = column; c < column + height; c++)
                {
                    if (!IsOnRange(r, c))
                        return false;

                    if (!IsEmpty(r, c) && _cells[r, c] != element)
                        return false;
                }
            }
            return true;
        }
        public bool CanPlace(Vector3 worldPosition, int width, int height, IMapElement element)
        {
            GetIndexes(worldPosition, out int row, out int column);
            return CanPlace(row, column, width, height, element);
        }
        public bool CanPlace(IMapElement element)
        {
            return CanPlace(element.Position, element.Width, element.Height, element);
        }

        public void Place(int row, int column, int width, int height, IMapElement cell)
        {
            for (int r = row; r < row + width; r++)
            {
                for (int c = column; c < column + height; c++)
                {
                    SetValue(r, c, cell);
                }
            }
            ElementChanged?.Invoke(cell);
        }
        public void Place(Vector3 worldPosition, int width, int height, IMapElement cell)
        {
            GetIndexes(worldPosition, out int row, out int column);
            Place(row, column, width, height, cell);
        }
        public void Place(IMapElement cell)
        {
            Place(cell.Position, cell.Width, cell.Height, cell);
        }

        public void Remove(int row, int column, int width, int height)
        {
            for (int r = row; r < row + width; r++)
            {
                for (int c = column; c < column + height; c++)
                {
                    SetValue(r, c, null);
                }
            }
        }
        public void Remove(Vector3 worldPosition, int width, int height)
        {
            GetIndexes(worldPosition, out int row, out int column);
            Remove(row, column, width, height);
        }
        public void Remove(IMapElement element)
        {
            Remove(element.Position, element.Width, element.Height);

            ElementRemoved?.Invoke(element);
        }

        #region GET
        public int GetRowsAmount()
        {
            return _rows;
        }
        public int GetColumnsAmount()
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
        public IMapElement GetValue(int row, int column)
        {
            if (IsOnRange(row, column))
                return _cells[row, column];

            return default;
        }
        public IMapElement GetValue(Vector3 worldPosition)
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
        public bool SetValue(int row, int column, IMapElement value)
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
        public bool SetValue(Vector3 worldPosition, IMapElement value)
        {
            GetIndexes(worldPosition, out int row, out int column);
            return SetValue(row, column, value);
        }

        #endregion

        private bool IsOnRange(int row, int column)
        {
            return row >= 0 && column >= 0 && row < _rows && column < _columns;
        }

        private List<Vector2Int> GetGridEmptyCells()
        {
            var emptyCellsList = new List<Vector2Int>();

            for (int r = 0; r < _cells.GetLength(0); r++)
            {
                for (int c = 0; c < _cells.GetLength(1); c++)
                {
                    if (IsEmpty(r, c))
                        emptyCellsList.Add(new Vector2Int(r, c));
                }
            }

            return emptyCellsList;
        }

        //public bool FindRandomAvailablePlace(int width, int height, out Vector3 foundPosition)
        //{
        //    var emptyCellsList = GetGridEmptyCells();

        //    if (emptyCellsList.Count == 0)
        //    {
        //        foundPosition = Vector3.zero;
        //        return false;
        //    }

        //    var randomIndexCell = UnityEngine.Random.Range(0, emptyCellsList.Count);
        //    var randomCell = emptyCellsList[randomIndexCell];

        //    if (CanPlace(randomCell.x, randomCell.y, width, height, null))
        //    {
        //        foundPosition = GetWorldPosition(randomCell.x, randomCell.y);
        //        return true;
        //    }


        //    foundPosition = Vector3.zero;
        //    return false;
        //}
        
        public bool FindRandomAvailablePlace(int width, int height, out Vector3 foundPosition)
        {
            var foundPositionsList = new List<Vector3>();

            var emptyCellsList = GetGridEmptyCells();

            for(int i=0;i<emptyCellsList.Count;i++)
            {
                var row = emptyCellsList[i].x;
                var column = emptyCellsList[i].y;
                if (CanPlace(row, column, width, height, null))
                {
                    foundPosition = GetWorldPosition(row, column);
                    foundPositionsList.Add(foundPosition);
                }
            }         

            if(foundPositionsList.Count==0)
            {
                foundPosition = Vector3.zero;
                return false;
            }
            else
            {
                var randomIndex = UnityEngine.Random.Range(0, foundPositionsList.Count);
                foundPosition = foundPositionsList[randomIndex];
                return true;
            }
            
        }
        public bool FindAvailablePlace(int width, int height, out Vector3 foundPosition)
        {
            var emptyCellsList = GetGridEmptyCells();

            var middleIndex = emptyCellsList.Count / 2;
            for (int i = middleIndex; i < emptyCellsList.Count; i++)
            {
                var row = emptyCellsList[i].x;
                var column = emptyCellsList[i].y;
                if (CanPlace(row, column, width, height, null))
                {
                    foundPosition = GetWorldPosition(row, column);
                    return true;
                }
            }

            for (int i = 0; i < middleIndex; i++)
            {
                var row = emptyCellsList[i].x;
                var column = emptyCellsList[i].y;
                if (CanPlace(row, column, width, height, null))
                {
                    foundPosition = GetWorldPosition(row, column);
                    return true;
                }
            }


            foundPosition = Vector3.zero;
            return false;
        }
    }
}

