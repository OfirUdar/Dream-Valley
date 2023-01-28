using UnityEngine;
using Zenject;

namespace Game.Map.Element.Obstcales
{
    public class ObstcalesRandomSpawner : IInitializable
    {
        [Inject] private readonly ObstcalesListSO _obstcalesListSO;
        [Inject] private readonly IElementSpawner _elementSpawner;
        [Inject] private readonly IMapGrid _grid;

        public void Initialize()
        {        
            SpawnObstacles();
        }

        private void SpawnObstacles()
        {
            int randomAmount = Random.Range(0, 2);

            for (int i = 0; i < randomAmount; i++)
            {
                var randomIndexObstcale = Random.Range(0, _obstcalesListSO.ObstaclesList.Count);
                var randomObstacle = _obstcalesListSO.ObstaclesList[randomIndexObstcale];

                if (_grid.FindRandomAvailablePlace(randomObstacle.Element.Width, randomObstacle.Element.Height, out Vector3 worldPosition))
                {
                    var obstacleInstance = _elementSpawner.Spawn(randomObstacle.Element.Pfb);

                    obstacleInstance.Position = worldPosition;
                    _grid.Place(obstacleInstance);
                }
            }
        }

    }
}

