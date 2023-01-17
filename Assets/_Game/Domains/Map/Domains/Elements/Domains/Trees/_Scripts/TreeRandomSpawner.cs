using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Map.Element.Trees
{
    public class TreeRandomSpawner : IInitializable
    {
        [Inject] private readonly TreesListSO _treesListSO;
        [Inject] private readonly IElementSpawner _elementSpawner;
        [Inject] private readonly IMapGrid _grid;

        public void Initialize()
        {
            var emptyCellsList = GetGridEmptyCells();
            int randomAmount = Random.Range(0, 2);

            for (int i = 0; i < randomAmount && emptyCellsList.Count > 0; i++)
            {
                var randomIndexTree = Random.Range(0, _treesListSO.TreesList.Count);
                var randomTree = _treesListSO.TreesList[randomIndexTree];
                var treeInstance = _elementSpawner.Spawn(randomTree.Pfb);

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

