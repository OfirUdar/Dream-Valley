using UnityEngine;
using Zenject;

namespace Game.Map.Element.Building.Resources
{
    public class InfoDisplayer : IInfoDisplayer
    {
        [Inject] private readonly ILevelManager _levelManager;
        [Inject] private readonly ResourceGeneratorLevelsData _levelsData;
        [Inject] private readonly IMapElement _mapElement;
        [Inject] private readonly UIResourceInfoDisplay _prefab;


        public void Display()
        {
            var currentIndexLevel = _levelManager.CurrentIndexLevel;
            if (currentIndexLevel < 0)
                currentIndexLevel = 0;

            var currentLevelData = _levelsData[currentIndexLevel];
            GameObject.Instantiate(_prefab)
                .Display(_mapElement.Data,
                _levelsData.Resource,
                currentLevelData,
               currentIndexLevel);
        }

    }

}
