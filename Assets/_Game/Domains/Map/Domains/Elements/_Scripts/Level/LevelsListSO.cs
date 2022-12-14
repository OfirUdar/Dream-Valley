using System.Collections.Generic;
using UnityEngine;

namespace Game.Map.Element
{
    /// <summary>
    /// Levels List for each element that manage the stats of upgrading the element
    /// </summary>
    
    [CreateAssetMenu(fileName = "Levels List", menuName = "Game/Map/Elements/Levels List", order = 0)]
    public class LevelsListSO : ScriptableObject
    {
        public List<Level> Levels = new List<Level>();
    }

}

