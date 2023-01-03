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

            _resourcesInventory.Initialized += OnInitalized;
            _resourcesInventory.ResourceChanged += OnResourceChanged;
            _resourcesCapacityManager.Changed += OnResourceCapacityChanged;
        }
        private void OnDestroy()
        {
            _resourcesInventory.Initialized -= OnInitalized;
            _resourcesInventory.ResourceChanged -= OnResourceChanged;
            _resourcesCapacityManager.Changed -= OnResourceCapacityChanged;
        }


        private void OnInitalized()
        {
            foreach (var resource in _resourcesInventory.GetResources())
            {
                AddResourceUIElement(resource.Key, resource.Value);
            }
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
        private void OnResourceCapacityChanged(ResourceDataSO resource, int totalCapacity)
        {
            if (_resourceUIDictionary.TryGetValue(resource, out ResourceUI resourceUI))
            {
                resourceUI.SetCapacity(totalCapacity);
            }
            else
            {
                AddResourceUIElement(resource.GUID, 0, false);
            }
        }



    }
}
