using System.Collections.Generic;
using UnityEngine;

namespace Game.Map.Element.Building.TownHall
{
    [CreateAssetMenu(fileName = "TownHall Levels", menuName = "Game/Map/Elements/Buildings/Town Hall Levels", order = 0)]
    public class TownHallLevelsData : ScriptableObject
    {
        [field: SerializeField] public List<TownHallData> DataLevels { get; private set; }


        public TownHallData this[int index] { get { return DataLevels[index]; } set { DataLevels[index] = value; } }

    }
}
