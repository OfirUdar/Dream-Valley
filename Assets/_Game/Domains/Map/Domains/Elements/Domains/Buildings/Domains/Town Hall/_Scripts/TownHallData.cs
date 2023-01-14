using System;
using System.Collections.Generic;

namespace Game.Map.Element.Building.TownHall
{
    [Serializable]
    public class TownHallData
    {
        public int Workers;
        public int StorageCapacity;
        public List<AvailableElement> ElementsAvailableList = new List<AvailableElement>();
    }
    [Serializable]
    public class AvailableElement
    {
        public MapElementSO ElementData;
        public int Amount;
    }
}
