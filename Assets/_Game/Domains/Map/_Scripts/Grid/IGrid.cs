using UnityEngine;

namespace Game.Map
{
    public interface IGrid<T> : IPlacer<T> where T : IPlaceable
    {
        public T[,] Cells { get; }

        public bool IsEmpty(int row, int column);
        public bool IsEmpty(Vector3 worldPosition);


        #region GET
        public int GetRowsAmount();
        public int GetColumnsAmount();
        public void GetIndexes(Vector3 worldPosition, out int row, out int column);
        public Vector2Int GetIndexes(Vector3 worldPosition);
        public T GetValue(int row, int column);
        public T GetValue(Vector3 worldPosition);
        public Vector3 GetWorldPosition(int row, int column);
        public Vector3 WorldPositionToGridPosition(Vector3 worldPosition);
        #endregion

        #region SET
        public bool SetValue(int row, int column, T value);
        public bool SetValue(Vector3 worldPosition, T value);
        #endregion


    }

    public interface IPlacer<T> where T : IPlaceable
    {
        public void Place(T cell);
        public void Place(int row, int column, int width, int height, T cell);
        public void Place(Vector3 worldPosition, int width, int height, T cell);

        public void Remove(int row, int column, int width, int height);
        public void Remove(Vector3 worldPosition, int width, int height);
        public void Remove(T cell);

        public bool CanPlace(T cell);
        public bool CanPlace(int row, int column, int width, int height, T cell);
        public bool CanPlace(Vector3 worldPosition, int width, int height, T cell);

        public bool FindRandomAvailablePlace(int width, int height, out Vector3 foundPosition);
        public bool FindAvailablePlace(int width, int height, out Vector3 foundPosition);

    }
}

