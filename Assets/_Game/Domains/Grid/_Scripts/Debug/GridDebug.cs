using UnityEngine;

namespace Game
{
    public class GridDebug
    {

        public GridDebug(IGrid grid)
        {
            var rows = grid.GetRowsAmount();
            var columns = grid.GetColumnsAmount();

            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < columns; y++)
                {
                    Debug.DrawLine(grid.GetWorldPosition(x, y), grid.GetWorldPosition(x, y + 1), Color.white, 100f);
                    Debug.DrawLine(grid.GetWorldPosition(x, y), grid.GetWorldPosition(x + 1, y), Color.white, 100f);
                }
            }
            Debug.DrawLine(grid.GetWorldPosition(0, columns), grid.GetWorldPosition(rows, columns), Color.white, 100f);
            Debug.DrawLine(grid.GetWorldPosition(rows, 0), grid.GetWorldPosition(rows, columns), Color.white, 100f);

        }

    }
}