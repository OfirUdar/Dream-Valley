using System;
using Udar;
using UnityEngine;

namespace Game
{
    public class ResourcesInventory : ISaveable, ILoadable
    {
        public SerializeDictionary<string, int> Resources = new SerializeDictionary<string, int>();


        public event Action<ResourceDataSO, int> ResourceChanged;

        public void AddResource(ResourceDataSO resource, int amount)
        {
            if (!Resources.ContainsKey(resource.GUID))
                Resources.Add(resource.GUID, amount);
            else
                Resources[resource.GUID] += amount;

            var totalAmount = Resources[resource.GUID];
            ResourceChanged?.Invoke(resource, totalAmount);
        }
        public void SubtratResource(ResourceDataSO resource, int amount)
        {
            Resources[resource.GUID] -= amount;

         #if UNITY_EDITOR
            if (Resources[resource.GUID] < 0)
                UnityEngine.Debug.Log($"Error subtract {amount} to {resource.Name}beyond the zero");
         #endif
            var totalAmount = Resources[resource.GUID];
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
            Resources = resourcesInventory.Resources;
        }

        #endregion
    }

}
