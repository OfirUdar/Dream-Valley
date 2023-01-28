using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class ResourcePrice
    {
        [field: SerializeField] public ResourceDataSO Resource { get; private set; }
        [field: SerializeField] public int Amount { get; private set; }

    }
}