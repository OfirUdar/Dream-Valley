using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Map.Element.Obstcales
{
    public class ObstcaleRandomSpawner : IInitializable
    {
        [Inject] private readonly ObstcalesListSO _obstcalesListSO;
        [Inject] private readonly IElementSpawner _elementSpawner;
        [Inject] private readonly IMapGrid _grid;

        public void Initialize()
        {
            var emptyCellsList = GetGridEmptyCells();
            int randomAmount = Random.Range(0, 2);

            for (int i = 0; i < randomAmount && emptyCellsList.Count > 0; i++)
            {
                var randomIndexObstcale = Random.Range(0, _obstcalesListSO.ObstaclesList.Count);
                var randomObstacle = _obstcalesListSO.ObstaclesList[randomIndexObstcale];
                var treeInstance = _elementSpawner.Spawn(randomObstacle.Element.Pfb);

                var randomIndexCell = Random.Range(0, emptyCellsList.Count);
                var randomCell = emptyCellsList[randomIndexCell];

                var worldPosition = _grid.GetWorldPosition(randomCell.x, randomCell.y);
                treeInstance.Position = worldPosition;
                _grid.Place(treeInstance);

                emptyCellsList.Remove(randomCell);
            }
        }

        private List<Vector2Int> GetGridEmptyCells()
        {
            var emptyCellsList = new List<Vector2Int>();

            for (int i = 0; i < _grid.Cells.GetLength(0); i++)
            {
                for (int j = 0; j < _grid.Cells.GetLength(1); j++)
                {
                    if (_grid.IsEmpty(i, j))
                        emptyCellsList.Add(new Vector2Int(i, j));
                }
            }

            return emptyCellsList;
        }
    }
}

