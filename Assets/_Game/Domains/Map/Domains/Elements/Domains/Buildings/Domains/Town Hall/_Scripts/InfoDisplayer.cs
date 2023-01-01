using UnityEngine;
using Zenject;

namespace Game.Map.Element.Building.TownHall
{
    public class InfoDisplayer : IInfoDisplayer
    {
        [Inject] private readonly ILevelManager _levelManager;
        [Inject] private readonly TownHallLevelsData _levelsData;
        [Inject] private readonly IMapElement _mapElement;
        [Inject] private readonly UITownHallInfoDisplay _prefab;


        public void Display()
        {
            var currentIndexLevel = _levelManager.CurrentIndexLevel;
            if (currentIndexLevel < 0)
                currentIndexLevel = 0;

            var currentLevelData = _levelsData[currentIndexLevel];
            GameObject.Instantiate(_prefab)
                .Display(_mapElement.Data,
                currentLevelData,
               currentIndexLevel);
        }


    }
}