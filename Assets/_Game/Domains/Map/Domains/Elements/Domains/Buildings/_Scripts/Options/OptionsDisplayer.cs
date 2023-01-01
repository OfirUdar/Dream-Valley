using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Map.Element.Building
{
    public class OptionsDisplayer : IOptionsDisplayer
    {
        [Inject] private readonly ElementOptionsSO _elementOptionsSO;
        [Inject] private readonly IBuildingStateMachine _buildingStateMachine;
        [Inject] private readonly IMapElement _mapElement;
        [Inject] private readonly ILevelManager _levelManager;

        public string GetDisplayText()
        {
            return $"{_mapElement.Data.Name} (level {_levelManager.CurrentIndexLevel + 1})";
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

            if (_buildingStateMachine.GetCurrentState() == StateType.Upgrade)
                options -= ElementOption.Upgrade;
            return _elementOptionsSO.GetPrefabsByOptions(options);
        }
    }
}