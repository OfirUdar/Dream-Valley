using System;
using System.Collections.Generic;

namespace Game.Resources
{
    public class ResourcesCapacityManager : IResourcesCapacityManager
    {
        public event Action<ResourceDataSO, int> Changed; // int -> the capacity


        private readonly Dictionary<ResourceDataSO, ResourceCapacity> _capacityDictionary
            = new Dictionary<ResourceDataSO, ResourceCapacity>();

        public void AddCapacityContainer(IResourceCapacityContainer resourceCapacityContainer)
        {
            foreach (var resource in _capacityDictionary.Keys)
            {
                AddCapacityContainer(resource, resourceCapacityContainer);
            }
        }
        public void AddCapacityContainer(ResourceDataSO resource, IResourceCapacityContainer resourceCapacityContainer)
        {
            if (_capacityDictionary.ContainsKey(resource))
            {
                _capacityDictionary[resource].AddCapacity(resourceCapacityContainer);
            }
            else
            {
                var resourceCapacity = new ResourceCapacity();
                resourceCapacity.AddCapacity(resourceCapacityContainer);
            }

            Changed?.Invoke(resource, _capacityDictionary[resource].Capacity);
        }
        public int GetCapacity(ResourceDataSO resource)
        {
            return _capacityDictionary[resource].Capacity;
        }
        public void MarkDirty(ResourceDataSO resource)
        {
            _capacityDictionary[resource].MarkDirty();

            Changed?.Invoke(resource, _capacityDictionary[resource].Capacity);
        }


    }
}
