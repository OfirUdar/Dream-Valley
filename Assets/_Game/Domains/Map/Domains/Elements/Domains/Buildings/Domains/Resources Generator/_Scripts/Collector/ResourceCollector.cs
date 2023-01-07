using System;
using Udar;
using UnityEngine;
using Zenject;

namespace Game.Map.Element.Building.Resources
{
    public class ResourceCollector : IResourceCollector, ISaveable
    {
        [Inject] private readonly IMapElement _mapElement;

        [Inject] private readonly ResourceGeneratorLevelsData _generatorData;
        [Inject] private readonly ILevelManager _levelManager;
        [Inject] private readonly IResourcesInventory _resourcesInventory;

        [Inject] private readonly ISaveManager _saveManager;

        private int _collectAmount;
        public event Action<bool> CollectableChanged;

        public void AddAmountToStorage()
        {
            if (_collectAmount == 0) // if its already shown dont show it again
                CollectableChanged?.Invoke(true);

            var addAmount = _generatorData[_levelManager.CurrentIndexLevel].AmountPerTime;
            var maxAmountCapcity = _generatorData[_levelManager.CurrentIndexLevel].Capacity;
            _collectAmount = Mathf.Min(_collectAmount + addAmount, maxAmountCapcity);

            _saveManager.Save(this);
        }

        public void Collect()
        {
            if (_collectAmount == 0)
                return;

            _resourcesInventory.AddResource(_generatorData.Resource, _collectAmount);

            _collectAmount = 0;

            CollectableChanged?.Invoke(false);

            _saveManager.Save(this);
        }

        public bool IsStorageFull()
        {
            var maxAmountCapcity = _generatorData[_levelManager.CurrentIndexLevel].Capacity;

            return (_collectAmount == maxAmountCapcity);
        }

        public ResourceDataSO GetResource()
        {
            return _generatorData.Resource;
        }

        public void Exit()
        {
            CollectableChanged?.Invoke(false);
        }



        #region Save&Load
        public string Path => SaveLoadKeys.GetResourceGeneratorCollectorPath(_mapElement.Data.GUID, _mapElement.SaveData.InstanceGUID);

        public string GetSerialized()
        {
            var collectAmountData = new CollectAmountData()
            {
                Amount = _collectAmount,
            };
            return JsonUtility.ToJson(collectAmountData);
        }

        public void SetSerialized(string data)
        {
            var collectAmountData = JsonUtility.FromJson<CollectAmountData>(data);

            _collectAmount = collectAmountData.Amount;
            CollectableChanged?.Invoke(_collectAmount > 0);
        }
        #endregion
    }
}