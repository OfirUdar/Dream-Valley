using System;
using UnityEngine;

namespace Game.Map.Element.Building.Resources
{
    [Serializable]
    public class ResourceGeneratorData
    {
        //[SerializeField, ReadOnly] private float _timeInMinute = 3f;
        private const float _TIME_IN_MINTUE = 1f;
        public float TimeInMinute => _TIME_IN_MINTUE;
        [field: SerializeField] public int AmountPerTime { get; private set; }
        [field: SerializeField] public int Capacity { get; private set; }
        [field: SerializeField] public int XpCollectPerAmount { get; private set; } //when collects the resources - get xp's

        public TimeSpan GetTimeSpan()
        {
            return TimeSpan.FromMinutes(TimeInMinute);
        }
    }
}