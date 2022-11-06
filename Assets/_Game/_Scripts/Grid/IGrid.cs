using UnityEngine;

namespace Game
{
    public interface IGrid : IPlacer
    {
        public bool IsEmpty(int row, int column);
        public bool IsEmpty(Vector3 worldPosition);

        #region GET
        public int GetRowsAmount();
        public int GetColumnsAmount();
        public void GetIndexes(Vector3 worldPosition, out int row, out int column);
        public Vector2Int GetIndexes(Vector3 worldPosition);
        public IMapElement GetValue(int row, int column);
        public IMapElement GetValue(Vector3 worldPosition);
        public Vector3 GetWorldPosition(int row, int column);
        public Vector3 WorldPositionToGridPosition(Vector3 worldPosition);

        #endregion

        #region SET
        public bool SetValue(int row, int column, IMapElement value);
        public bool SetValue(Vector3 worldPosition, IMapElement value);
        #endregion

    }

    public interface IPlacer
    {
        public void Place(IMapElement cell);
        public void Place(int row, int column, int width, int height, IMapElement cell);
        public void Place(Vector3 worldPosition, int width, int height, IMapElement cell);

        public void Remove(int row, int column, int width, int height);
        public void Remove(Vector3 worldPosition, int width, int height);
        public void Remove(IMapElement cell);

        public bool CanPlace(IMapElement cell);
        public bool CanPlace(int row, int column, int width, int height);
        public bool CanPlace(Vector3 worldPosition, int width, int height);
    }
}

