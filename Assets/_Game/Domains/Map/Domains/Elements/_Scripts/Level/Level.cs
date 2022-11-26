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
            public float Hours;
            public float Mintues;
            public float Seconds;
        }
    }
}

