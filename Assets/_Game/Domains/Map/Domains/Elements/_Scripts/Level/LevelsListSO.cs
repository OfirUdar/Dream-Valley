using System.Collections.Generic;
using UnityEngine;

namespace Game.Map.Element
{
    [CreateAssetMenu(fileName = "Levels List", menuName = "Game/Map/Elements/Levels List", order = 0)]
    public class LevelsListSO : ScriptableObject
    {
        public List<Level> Levels = new List<Level>();
    }

}

