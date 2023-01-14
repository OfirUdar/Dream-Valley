namespace Game.Map.Element.Building.TownHall
{
    public class AvailableElementsCounter : IAvailableElementsCounter
    {
        private readonly IAvailableElementsManager _availableElementsManager;
        private readonly IMapManager _mapManager;
        private readonly ILevelManager _levelManager;
        private readonly TownHallLevelsData _levelsData;

        public AvailableElementsCounter(IAvailableElementsManager availableElementsManager,
            IMapManager mapManager, ILevelManager levelManager,
            TownHallLevelsData levelsData)
        {
            _availableElementsManager = availableElementsManager;
            _mapManager = mapManager;
            _levelManager = levelManager;
            _levelsData = levelsData;

            _availableElementsManager.Setup(this);
        }
        public int GetCurrentAmountElements(MapElementSO element)
        {
            var currentLevel = _levelsData[_levelManager.CurrentIndexLevel];

            var currentAmount = _mapManager.GetElementInstancesAmount(element);
            foreach (var level in _levelsData.DataLevels)
            {
                var availableElementAmount = level.ElementsAvailableList.Find(e => e.ElementData == element).Amount;
                currentAmount -= availableElementAmount;

                if (level == currentLevel)
                    break;
            }

            return currentAmount;
        }

        public int GetMaxAmountElement(MapElementSO element)
        {
            return 100;
        }
    }
}

