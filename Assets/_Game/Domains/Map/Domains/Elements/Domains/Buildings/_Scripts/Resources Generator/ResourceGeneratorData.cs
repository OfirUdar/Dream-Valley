using System;
using Udar;
using UnityEngine;

namespace Game.Map.Element.Building.Resources
{
    [Serializable]
    public class ResourceGeneratorData
    {
        [field: SerializeField, ReadOnly] public float TimeInMinute { get; private set; } = 10f;
        [field: SerializeField] public int AmountPerTime { get; private set; }
        [field: SerializeField] public int Capacity { get; private set; }
        [field: SerializeField] public int XpCollectPerAmount { get; private set; } //when collects the resources - get xp's
    
        public TimeSpan GetTimeSpan()
        {
            return TimeSpan.FromMinutes(TimeInMinute);
        }
    }
}