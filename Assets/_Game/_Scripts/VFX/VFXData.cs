using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class VFXData
    {
        [field: SerializeField] public GameObject EffectPfb { get; private set; }

    }
}