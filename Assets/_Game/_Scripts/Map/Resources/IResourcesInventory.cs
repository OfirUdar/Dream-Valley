using System;
using Udar;
using UnityEngine;

namespace Game
{
    public interface IResourcesInventory
    {

        public event Action<ResourceDataSO, int> ResourceChanged;
        public event Action<ResourceDataSO, int, Vector3> ResourceChangedWithPosition;
        public event Action<ResourceDataSO, bool> StorageFullChanged;

        public SerializeDictionary<string, int> GetResources();
        public void AddResource(ResourceDataSO resource, int amount);
        public void AddResource(ResourceDataSO resource, int amount, Vector3 position);
        public void SubtractResource(ResourceDataSO resource, int amount);
        public bool CanSubtract(ResourceDataSO resource, int amount);
        public bool IsStorageFull(ResourceDataSO resource);

    }

}
