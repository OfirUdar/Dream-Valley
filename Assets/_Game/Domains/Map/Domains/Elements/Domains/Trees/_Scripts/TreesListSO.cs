using System.Collections.Generic;
using UnityEngine;

namespace Game.Map.Element.Trees
{
    [CreateAssetMenu(fileName = "Tree List", menuName = "Game/Map/Elements/New Tree List")]
    public class TreesListSO : ScriptableObject
    {
        [field: SerializeField] public List<MapElementSO> TreesList { get; private set; }
    }
}

