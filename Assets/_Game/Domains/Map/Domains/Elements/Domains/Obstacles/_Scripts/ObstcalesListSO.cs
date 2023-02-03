using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Map.Element.Obstcales
{
    [CreateAssetMenu(fileName = "Obstacles List", menuName = "Game/Map/Elements/Obstcales/New Obstacles List")]
    public class ObstcalesListSO : ScriptableObject
    {
        [field: SerializeField] public List<ObstacleDataSO> ObstaclesList { get; private set; }
    }
}

