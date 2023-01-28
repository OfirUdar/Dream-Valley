namespace Game
{
    public interface IAvailableElementsCounter
    {
        public int GetCurrentAmountElements(MapElementSO element);
        public int GetMaxAmountElement(MapElementSO element);
    }
}