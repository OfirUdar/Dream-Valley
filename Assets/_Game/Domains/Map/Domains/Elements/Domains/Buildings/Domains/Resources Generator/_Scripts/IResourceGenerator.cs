using System;

namespace Game.Map.Element.Building.Resources
{
    public interface IResourceGenerator
    {
        public event Action<bool> CollectableChanged; //bool -> canCollect?
        public ResourceDataSO GetResource();
        public void Collect();
    }
}