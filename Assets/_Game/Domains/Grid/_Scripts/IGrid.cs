using UnityEngine;

namespace Game
{
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

