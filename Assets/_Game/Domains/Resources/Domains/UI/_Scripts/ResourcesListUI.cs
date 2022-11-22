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

        private Profile _profile;

        private readonly Dictionary<ResourceDataSO, ResourceUI> _resourceUIDictionary
            = new Dictionary<ResourceDataSO, ResourceUI>();

        [Inject]
        public void Init(Profile profile)
        {
            _profile = profile;

            foreach (var resource in _profile.ResourcesInventory.Resources)
            {
                AddResourceUIElement(resource.Key,resource.Value);
            }

            _profile.ResourcesInventory.ResourcesChanged += OnResourcesChanged;
        }

       

        private void OnDestroy()
        {
            _profile.ResourcesInventory.ResourcesChanged -= OnResourcesChanged;
        }
        private void AddResourceUIElement(string guid, int amount)
        {
            var uiInstance = Instantiate(_pfb, _container, false);
            var resourceData = _resourcesList.GetByGUID(guid);

            uiInstance.Setup(resourceData.Sprite, amount);
            _resourceUIDictionary.Add(resourceData, uiInstance);
        }

        private void OnResourcesChanged(ResourceDataSO data, int amount)
        {
            if (_resourceUIDictionary.TryGetValue(data, out ResourceUI resourceUI))
            {
                resourceUI.SetTweenAmount(amount);
            }
            else
            {
                AddResourceUIElement(data.GUID, amount);
            }

        }


    }
}
