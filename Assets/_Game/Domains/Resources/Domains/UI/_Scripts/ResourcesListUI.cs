using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Resources.UI
{
    public class ResourcesListUI : MonoBehaviour
    {
        [SerializeField] private ResourceDataListSO _resourcesList;
        [Space]
        [SerializeField] private Transform _container;
        [Space]
        [SerializeField] private ResourceUI _pfb;

        private IResourcesInventory _resourcesInventory;
        private IResourcesCapacityManager _resourcesCapacityManager;

        private readonly Dictionary<ResourceDataSO, ResourceUI> _resourceUIDictionary
            = new Dictionary<ResourceDataSO, ResourceUI>();

        [Inject]
        public void Init(IResourcesInventory resourcesInventory, IResourcesCapacityManager resourcesCapacityManager)
        {
            _resourcesInventory = resourcesInventory;
            _resourcesCapacityManager = resourcesCapacityManager;

            _resourcesInventory.ResourceChanged += OnResourceChanged;
            _resourcesCapacityManager.Changed += OnResourceCapacityChanged;

            foreach (var resource in _resourcesInventory.GetResources())
            {
                AddResourceUIElement(resource.Key, resource.Value);

                var capacity = _resourcesCapacityManager.GetCapacity(resource.Key);
                UpdateCapacityText(resource.Key, capacity);
            }
        }
       
        private void OnDestroy()
        {
            _resourcesInventory.ResourceChanged -= OnResourceChanged;
            _resourcesCapacityManager.Changed -= OnResourceCapacityChanged;
        }


        private void AddResourceUIElement(string guid, int amount, bool withTweenAmount = false)
        {
            var uiInstance = Instantiate(_pfb, _container, false);
            var resourceData = _resourcesList.GetByGUID(guid);

            uiInstance.Setup(resourceData.Sprite, amount, 100);

            if (withTweenAmount)
                uiInstance.SetTweenAmount(amount);

            _resourceUIDictionary.Add(resourceData, uiInstance);
        }
        private void UpdateCapacityText(string resourceGuid, int totalCapacity)
        {
            var resource = _resourcesList.GetByGUID(resourceGuid);

            if (_resourceUIDictionary.TryGetValue(resource, out ResourceUI resourceUI))
            {
                resourceUI.SetCapacity(totalCapacity);
            }
        }


        private void OnResourceChanged(ResourceDataSO resource, int amount)
        {
            if (_resourceUIDictionary.TryGetValue(resource, out ResourceUI resourceUI))
            {
                resourceUI.SetTweenAmount(amount);
            }
            else
            {
                AddResourceUIElement(resource.GUID, amount, true);
            }

        }
        private void OnResourceCapacityChanged(string resourceGuid, int totalCapacity)
        {
            UpdateCapacityText(resourceGuid, totalCapacity);
        }

       
    }
}
