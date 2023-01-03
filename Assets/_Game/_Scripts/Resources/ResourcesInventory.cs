using System;
using Udar;
using UnityEngine;
using Zenject;

namespace Game
{
    public class ResourcesInventory : IResourcesInventory, IInitializable, ISaveable, ILoadable
    {
        public SerializeDictionary<string, int> Resources = new SerializeDictionary<string, int>();

        private readonly ISaveManager _saveManager;
        public event Action<ResourceDataSO, int> ResourceChanged;
        public event Action Initialized;

        public ResourcesInventory(ISaveManager saveManager, ILoadManager loadManager, InitResourceDataListSO initResourceList)
        {
            _saveManager = saveManager;

            bool hasLoaded = loadManager.TryLoad(this);

            if (!hasLoaded)
            {
                foreach (var resourceInit in initResourceList.Resources)
                {
                    AddResource(resourceInit.ResourceDataSO, resourceInit.Amount);
                }
            }
        }
        public void Initialize()
        {
            Initialized?.Invoke();
        }

        public SerializeDictionary<string, int> GetResources()
        {
            return Resources;
        }

        public void AddResource(ResourceDataSO resource, int amount)
        {
            if (!Resources.ContainsKey(resource.GUID))
                Resources.Add(resource.GUID, amount);
            else
                Resources[resource.GUID] += amount;

            var totalAmount = Resources[resource.GUID];

            _saveManager.Save(this);

            ResourceChanged?.Invoke(resource, totalAmount);
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

            ResourceChanged?.Invoke(resource, totalAmount);
        }
        public bool CanSubtract(ResourceDataSO resource, int amount)
        {
            if (Resources.ContainsKey(resource.GUID))
                return (Resources[resource.GUID] - amount >= 0);

            return false;
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
    public interface IResourcesInventory
    {

        public event Action<ResourceDataSO, int> ResourceChanged;
        public event Action Initialized;

        public SerializeDictionary<string, int> GetResources();
        public void AddResource(ResourceDataSO resource, int amount);
        public void SubtractResource(ResourceDataSO resource, int amount);
        public bool CanSubtract(ResourceDataSO resource, int amount);

    }

}
