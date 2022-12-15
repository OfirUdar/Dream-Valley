using System;
using Udar;
using UnityEngine;

namespace Game.Map.Element.Building.Resources
{
    [Serializable]
    public class ResourceGeneratorData
    {
        [SerializeField, ReadOnly] private float _timeInMinute = 10f;
        public float TimeInMinute => _timeInMinute;
        [field: SerializeField] public int AmountPerTime { get; private set; }
        [field: SerializeField] public int Capacity { get; private set; }
        [field: SerializeField] public int XpCollectPerAmount { get; private set; } //when collects the resources - get xp's

        public TimeSpan GetTimeSpan()
        {
            // return TimeSpan.FromMinutes(TimeInMinute);
            return TimeSpan.FromSeconds(TimeInMinute*3);
        }
    }
}