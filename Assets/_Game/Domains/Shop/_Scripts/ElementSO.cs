using System;
using UnityEngine;

namespace Game.Shop
{
    [CreateAssetMenu(fileName = "Shop Element", menuName = "Game/Shop/Elements/Element", order = 0)]
    public class ElementSO : ScriptableObject
    {
        [field: SerializeField] public MapElementSO Element { get; private set; }
        [field: SerializeField] public ResourcePrice Price { get; private set; }

    }

    [Serializable]
    public class ResourcePrice
    {
        [field: SerializeField] public ResourceDataSO Resource { get; private set; }
        [field: SerializeField] public int Amount { get; private set; }

    }
}