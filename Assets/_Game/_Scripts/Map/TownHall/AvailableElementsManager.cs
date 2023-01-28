namespace Game
{
    public class AvailableElementsManager : IAvailableElementsManager, IAvailableElementsCounter
    {
        private IAvailableElementsCounter _availableElementsCounter;

        public void Setup(IAvailableElementsCounter counter)
        {
            _availableElementsCounter = counter;
        }

        public int GetCurrentAmountElements(MapElementSO element)
        {
            if (_availableElementsCounter == null)
                return 0;

            return _availableElementsCounter.GetCurrentAmountElements(element);
        }

        public int GetMaxAmountElement(MapElementSO element)
        {
            if (_availableElementsCounter == null)
                return 0;

            return _availableElementsCounter.GetMaxAmountElement(element);
        }
    }
}