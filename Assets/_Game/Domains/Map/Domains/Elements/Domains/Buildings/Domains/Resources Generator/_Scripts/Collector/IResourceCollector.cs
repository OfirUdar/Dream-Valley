using System;
using Udar;
using UnityEngine;

namespace Game.Map.Element.Building.Resources
{
    public interface IResourceCollector : ILoadable
    {
        public event Action<bool> CollectableChanged;

        public void AddAmountToStorage();
        public void Collect();
        public void Collect(Vector3 worldPosition);
        public bool IsStorageFull();
        public void Exit();
        public ResourceDataSO GetResource();
    }
}