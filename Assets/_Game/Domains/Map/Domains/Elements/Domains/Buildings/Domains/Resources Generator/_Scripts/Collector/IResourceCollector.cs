using System;
using Udar;

namespace Game.Map.Element.Building.Resources
{
    public interface IResourceCollector : ILoadable
    {
        public event Action<bool> CollectableChanged;

        public void AddAmountToStorage();
        public void Collect();
        public bool IsStorageFull();
        public void Exit();
        public ResourceDataSO GetResource();
    }
}