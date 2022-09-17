using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "Placement List", menuName = "Placements/Placements List", order = 0)]
    public class PlacementListSO : ScriptableObject
    {
        [field: SerializeField] public List<BuildingSO> PlacementsList { get; private set; } 
            = new List<BuildingSO>();

    }
}