using System.Collections.Generic;
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
            var currentLevelData = _levelsData[_levelManager.CurrentLevel];
            GameObject.Instantiate(_prefab).Display(_mapElement.Data, _levelsData.Resource, currentLevelData);
        }

    }
    public class UpgradeDisplayer : IUpgradeDisplayer
    {
        [Inject] private readonly ILevelManager _levelManager;
        [Inject] private readonly LevelsListSO _elementLevels;
        [Inject] private readonly ResourceGeneratorLevelsData _resourceLevels;
        [Inject] private readonly IMapElement _mapElement;
        [Inject] private readonly IBuildingStateMachine _buildingStateMachine;
        [Inject] private readonly UIResourceUpgradeDisplay _prefab;
        [Inject] private readonly Profile _profile;


        public void Display()
        {
            var currentElementLevel = _elementLevels[_levelManager.CurrentLevel + 1];

            var currentLevelData = _resourceLevels[_levelManager.CurrentLevel];
            var nextLevelData = _resourceLevels[_levelManager.CurrentLevel + 1];
            GameObject.Instantiate(_prefab)
                .Display(_mapElement.Data,
                _resourceLevels.Resource,
                currentLevelData,
                nextLevelData,
                currentElementLevel,
                StartUpgrade);
        }

        private void StartUpgrade()
        {
            var nextElementLevel = _elementLevels[_levelManager.CurrentLevel + 1];

            var canPurchase = _profile.ResourcesInventory.CanSubtract(nextElementLevel.UpgradePrice.Resource, nextElementLevel.UpgradePrice.Amount);

            if (canPurchase)
            {
                _buildingStateMachine.ChangeState(StateType.Upgrade);
                _profile.ResourcesInventory.SubtratResource(nextElementLevel.UpgradePrice.Resource, nextElementLevel.UpgradePrice.Amount);
            }
        }

    }

}
