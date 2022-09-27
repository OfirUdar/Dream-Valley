using UnityEngine;

namespace Game
{
    public interface IGrid : IPlaceManager
    {
        public bool IsEmpty(int row, int column);
        public bool IsEmpty(Vector3 worldPosition);

        #region GET
        public int GetRows();
        public int GetColumns();
        public void GetIndexes(Vector3 worldPosition, out int row, out int column);
        public Vector2Int GetIndexes(Vector3 worldPosition);
        public IPlaceable GetValue(int row, int column);
        public IPlaceable GetValue(Vector3 worldPosition);
        public Vector3 GetWorldPosition(int row, int column);
        public Vector3 WorldPositionToGridPosition(Vector3 worldPosition);

        #endregion

        #region SET
        public bool SetValue(int row, int column, IPlaceable value);
        public bool SetValue(Vector3 worldPosition, IPlaceable value);
        #endregion


    }

    public interface IPlaceManager
    {
        public void PlaceObject(int row, int column, int width, int height, IPlaceable cell);
        public void PlaceObject(Vector3 worldPosition, int width, int height, IPlaceable cell);
        public void RemoveObject(int row, int column, int width, int height);
        public void RemoveObject(Vector3 worldPosition, int width, int height);
        public bool CanPlace(int row, int column, int width, int height);
        public bool CanPlace(Vector3 worldPosition, int width, int height);
    }
}

