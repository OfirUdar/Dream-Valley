using UnityEngine;
using Zenject;

namespace Game.Map.Element.Building.TownHall
{
    public class UpgradeDisplayer : IUpgradeDisplayer
    {
        [Inject] private readonly ILevelManager _levelManager;
        [Inject] private readonly TownHallLevelsData _levelsData;
        [Inject] private readonly IMapElement _mapElement;
        [Inject] private readonly IBuildingStateMachine _buildingStateMachine;
        [Inject] private readonly IResourcesInventory _resourcesInventory;
        [Inject] private readonly IOptionsAggragetor _optionsUI;
        [Inject] private readonly UITownHallUpgradeDisplay _prefab;

        public void Display()
        {
            var nextElementLevel = _levelManager.NextLevel;

            var currentLevelData = _levelsData[_levelManager.CurrentIndexLevel];
            var nextLevelData = _levelsData[_levelManager.CurrentIndexLevel + 1];

            var canPurchase = _resourcesInventory.CanSubtract(nextElementLevel.UpgradePrice.Resource, nextElementLevel.UpgradePrice.Amount);

            var popupDisplay = GameObject.Instantiate(_prefab);
            popupDisplay
                 .SetElement(_mapElement.Data)
                 .SetNextLevelData(nextElementLevel, canPurchase, StartUpgrade)
                 .SetNextLevelIndex(_levelManager.CurrentIndexLevel + 1)
                 .SetComparisonLevels(currentLevelData, nextLevelData)
                 .SetRequiredElements(currentLevelData, nextLevelData, _levelsData)
                 .Display();


        }

        private void StartUpgrade()
        {
            var nextElementLevel = _levelManager.NextLevel;

            _buildingStateMachine.ChangeState(StateType.Upgrade);

            _resourcesInventory.SubtractResource(nextElementLevel.UpgradePrice.Resource, nextElementLevel.UpgradePrice.Amount);

            _optionsUI.RequestRefresh();

        }

    }
}