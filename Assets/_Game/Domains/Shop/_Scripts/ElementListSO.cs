using System.Collections.Generic;
using UnityEngine;

namespace Game.Shop
{
    [CreateAssetMenu(fileName = "Shop Element List", menuName = "Game/Shop/Elements/Elements List", order = 0)]
    public class ElementListSO : ScriptableObject
    {
        [field: SerializeField] public List<ElementSO> Elements { get; private set; }

    }
}