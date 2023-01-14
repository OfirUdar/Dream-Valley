using System.Collections.Generic;

namespace Game.Map
{
    public interface IMapSaver
    {
        public Dictionary<MapElementSO, int> LoadAll();
        public void SaveElement(IMapElement element);
        public void DeleteElement(IMapElement element);
    }
}
