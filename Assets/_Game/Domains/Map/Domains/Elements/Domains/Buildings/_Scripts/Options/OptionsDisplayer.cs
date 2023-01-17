using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Map.Element.Building
{
    public class OptionsDisplayer : IOptionsDisplayer
    {
        [Inject] private readonly ElementOptionsSO _elementOptionsSO;
        [InjectOptional] private readonly IBuildingStateMachine _buildingStateMachine;
        [InjectOptional] private readonly ILevelManager _levelManager;
        [Inject] private readonly IMapElement _mapElement;

        public string GetDisplayText()
        {
            if (_levelManager != null)
                return $"{_mapElement.Data.Name} (level {_levelManager.CurrentIndexLevel + 1})";
            else
                return _mapElement.Data.Name;
        }

        public void Show(Transform container)
        {
            var optionsButtonsPfb = GetOptionsByState();

            foreach (var pfb in optionsButtonsPfb)
            {
                var optionButton = GameObject.Instantiate(pfb, container, false);
                optionButton.Setup(_mapElement);
            }

        }

        private List<OptionButton> GetOptionsByState()
        {
            var options = _mapElement.Data.Options;

            if (_levelManager != null && !_levelManager.HasNext())
            {
                options -= ElementOption.Upgrade;
            }
            if (_buildingStateMachine != null && _buildingStateMachine.GetCurrentState() == StateType.Upgrade)
            {
                options -= ElementOption.Upgrade;
            }


            return _elementOptionsSO.GetPrefabsByOptions(options);
        }
    }
}