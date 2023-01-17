using System;
using UnityEngine;

namespace Game.Map
{
    [Serializable]
    public class VFXData
    {
        [field: SerializeField] public VFXType Type { get; private set; }
        [field: SerializeField] public GameObject EffectPfb { get; private set; }

    }
}