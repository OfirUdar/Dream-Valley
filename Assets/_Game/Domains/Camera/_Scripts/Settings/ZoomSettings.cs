using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class ZoomSettings
    {
        [field: SerializeField] public float Speed { get; private set; } = 20f;
        [field: SerializeField] public float Min { get; private set; } = 3f;
        [field: SerializeField] public float Max { get; private set; } = 7f;
    }



}

