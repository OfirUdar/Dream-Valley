using UnityEngine;
using Zenject;

namespace Game.Map.Element.Building.Resources
{
    public class UpgradeDisplayer : IUpgradeDisplayer
    {
        [Inject] private readonly ILevelManager _levelManager;
        [Inject] private readonly ResourceGeneratorLevelsData _resourceLevels;
        [Inject] private readonly IMapElement _mapElement;
        [Inject] private readonly IBuildingStateMachine _buildingStateMachine;
        [Inject] private readonly Profile _profile;
        [Inject] private readonly UIResourceUpgradeDisplay _prefab;
        [Inject] private readonly IOptionsAggragetor _optionsUI;

        public void Display()
        {
            var nextElementLevel = _levelManager.NextLevel;

            var currentLevelData = _resourceLevels[_levelManager.CurrentIndexLevel];
            var nextLevelData = _resourceLevels[_levelManager.CurrentIndexLevel + 1];

            var canPurchase = _profile.ResourcesInventory.CanSubtract(nextElementLevel.UpgradePrice.Resource, nextElementLevel.UpgradePrice.Amount);

            var popupDisplay = GameObject.Instantiate(_prefab);
            popupDisplay
                .SetElement(_mapElement.Data)
                .SetNextLevelData(nextElementLevel, canPurchase, StartUpgrade)
                .SetNextLevelIndex(_levelManager.CurrentIndexLevel + 1)
                .SetResourcesData(_resourceLevels.Resource, currentLevelData, nextLevelData)
                .Display();
        }

        private void StartUpgrade()
        {
            var nextElementLevel = _levelManager.NextLevel;

            _buildingStateMachine.ChangeState(StateType.Upgrade);

            _profile.ResourcesInventory.SubtractResource(nextElementLevel.UpgradePrice.Resource, nextElementLevel.UpgradePrice.Amount);

            _optionsUI.RequestRefresh();

        }

    }

}
