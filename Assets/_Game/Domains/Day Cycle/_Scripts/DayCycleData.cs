using System;
using UnityEngine;

namespace Game.DayCycle
{
    [Serializable]
    public class DayCycleData
    {
        [field: SerializeField] public float DurationSeconds { get; private set; } = 120f;
        [field: SerializeField] public Gradient LightNightGradient { get; private set; }
        [field: SerializeField] public Gradient CameraNightGradient { get; private set; }
    }

}
