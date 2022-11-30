using System;
using UnityEngine;

namespace Game.Map.Element
{
    [Serializable]
    public class Level
    {
        public ResourcePrice UpgradePrice;
        public Duration UpgradeDuration;
        public int UpgradeAccomplishedXP;
        public GameObject Pfb;

        [Serializable]
        public class Duration
        {
            public int Hours;
            public int Mintues;
            public int Seconds;

            public TimeSpan GetTimeSpan()
            {
                return new TimeSpan(hours: Hours, minutes: Mintues, seconds: Seconds);
            }
        }
    }
}

