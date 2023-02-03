using System;
using Udar;
using UnityEngine;
using Zenject;

namespace Game
{
    public class ResourcesInventory : IResourcesInventory, ISaveable, ILoadable
    {
        public SerializeDictionary<string, int> Resources = new SerializeDictionary<string, int>();

        [Inject] private readonly IResourcesCapacityManager _resourcesCapacityManager;

        private readonly ISaveManager _saveManager;

        public event Action<ResourceDataSO, int> ResourceChanged;
        public event Action<ResourceDataSO, int, Vector3> ResourceChangedWithPosition;
        public event Action<ResourceDataSO, bool> StorageFullChanged;

        public ResourcesInventory(ISaveManager saveManager, ILoadManager loadManager, InitResourceDataListSO initResourceList)
        {
            _saveManager = saveManager;

            bool hasLoaded = loadManager.TryLoad(this);

            if (!hasLoaded)
            {
                foreach (var resourceInit in initResourceList.Resources)
                {
                    AddResourceWithEmptyCapacity(resourceInit.ResourceDataSO, resourceInit.Amount);
                }
            }
        }

        public SerializeDictionary<string, int> GetResources()
        {
            return Resources;
        }

        private void AddResourceWithEmptyCapacity(ResourceDataSO resource, int amount)
        {
            if (!Resources.ContainsKey(resource.GUID))
                Resources.Add(resource.GUID, amount);
            else
                Resources[resource.GUID] += amount;

            var totalAmount = Resources[resource.GUID];
            Resources[resource.GUID] = totalAmount;

            _saveManager.Save(this);

            ResourceChanged?.Invoke(resource, totalAmount);
        }

 
        private int AddResourceWithoutNotify(ResourceDataSO resource, int amount)
        {
            if (!Resources.ContainsKey(resource.GUID))
                Resources.Add(resource.GUID, amount);
            else
                Resources[resource.GUID] += amount;

            var storageCapacity = _resourcesCapacityManager.GetCapacity(resource.GUID);
            var totalAmount = Mathf.Min(Resources[resource.GUID], storageCapacity);
            Resources[resource.GUID] = totalAmount;

            _saveManager.Save(this);

            if (totalAmount == storageCapacity)
                StorageFullChanged?.Invoke(resource, true);

            return totalAmount;
        }
        public void AddResource(ResourceDataSO resource, int amount)
        {
            if (IsStorageFull(resource))
                return;

            var totalAmount = AddResourceWithoutNotify(resource, amount);
            ResourceChanged?.Invoke(resource, totalAmount);
        }
        public void AddResource(ResourceDataSO resource, int amount, Vector3 position)
        {
            if (IsStorageFull(resource))
                return;

            var totalAmount = AddResourceWithoutNotify(resource, amount);

            ResourceChangedWithPosition?.Invoke(resource, totalAmount, position);
        }
        public void SubtractResource(ResourceDataSO resource, int amount)
        {
            Resources[resource.GUID] -= amount;

#if UNITY_EDITOR
            if (Resources[resource.GUID] < 0)
                Debug.LogError($"Error subtract {amount} to {resource.Name}beyond the zero");
#endif
            var totalAmount = Resources[resource.GUID];

            _saveManager.Save(this);

            StorageFullChanged?.Invoke(resource, false);

            ResourceChanged?.Invoke(resource, totalAmount);
        }
        public bool CanSubtract(ResourceDataSO resource, int amount)
        {
            if (Resources.ContainsKey(resource.GUID))
                return (Resources[resource.GUID] - amount >= 0);

            return false;
        }
        public bool IsStorageFull(ResourceDataSO resource)
        {
            var capacity = _resourcesCapacityManager.GetCapacity(resource.GUID);
            return Resources[resource.GUID] == capacity;
        }

        #region Save&Load

        public string Path => SaveLoadKeys.Resources;


        public string GetSerialized()
        {
            return JsonUtility.ToJson(this);
        }

        public void SetSerialized(string data)
        {
            var resourcesInventory = JsonUtility.FromJson<ResourcesInventory>(data);
            Resources = resourcesInventory.GetResources();
        }

        #endregion
    }

}
