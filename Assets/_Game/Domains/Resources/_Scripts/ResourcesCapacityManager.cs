using System;
using System.Collections.Generic;
using Zenject;

namespace Game.Resources
{
    public class ResourcesCapacityManager : IResourcesCapacityManager, IInitializable
    {
        public event Action<string, int> Changed; // int -> the capacity


        [Inject] private readonly IResourcesInventory _iventory;
        private readonly Dictionary<string, ResourceCapacity> _capacityDictionary
            = new Dictionary<string, ResourceCapacity>();



        public void Initialize()
        {
            foreach (var resourceGuid in _iventory.GetResources().Keys)
            {
                var resourceCapacity = new ResourceCapacity();
                _capacityDictionary.Add(resourceGuid, resourceCapacity);
            }
        }

        public void AddCapacityContainer(IResourceCapacityContainer resourceCapacityContainer)
        {
            foreach (var resourceGuid in _capacityDictionary.Keys)
            {
                AddCapacityContainer(resourceGuid, resourceCapacityContainer);
            }
        }
        public void AddCapacityContainer(string resourceGuid, IResourceCapacityContainer resourceCapacityContainer)
        {
            if (_capacityDictionary.ContainsKey(resourceGuid))
            {
                _capacityDictionary[resourceGuid].AddCapacity(resourceCapacityContainer);
            }
            else
            {
                var resourceCapacity = new ResourceCapacity();
                resourceCapacity.AddCapacity(resourceCapacityContainer);
                _capacityDictionary.Add(resourceGuid, resourceCapacity);
            }

            Changed?.Invoke(resourceGuid, _capacityDictionary[resourceGuid].Capacity);
        }
        public int GetCapacity(string resourceGuid)
        {
            return _capacityDictionary[resourceGuid].Capacity;
        }
        public void MarkDirty(string resourceGuid)
        {
            _capacityDictionary[resourceGuid].MarkDirty();

            Changed?.Invoke(resourceGuid, _capacityDictionary[resourceGuid].Capacity);
        }
        public void MarkDirty()
        {
            foreach (var resourceGuid in _capacityDictionary.Keys)
            {
                _capacityDictionary[resourceGuid].MarkDirty();
                Changed?.Invoke(resourceGuid, _capacityDictionary[resourceGuid].Capacity);
            }

        }


    }
}
