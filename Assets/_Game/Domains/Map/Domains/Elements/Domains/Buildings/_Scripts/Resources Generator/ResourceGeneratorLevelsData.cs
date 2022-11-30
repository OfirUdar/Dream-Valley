using System.Collections.Generic;
using UnityEngine;

namespace Game.Map.Element.Building.Resources
{
    [CreateAssetMenu(fileName = "Resource Generator Levels", menuName = "Game/Map/Elements/Resources/New Resource Generator Levels", order = 0)]
    public class ResourceGeneratorLevelsData : ScriptableObject
    {
        [field: SerializeField] public ResourceDataSO Resource { get; private set; }
        [field: SerializeField] public List<ResourceGeneratorData> DataLevels { get; private set; }

        public ResourceGeneratorData this[int index] { get { return DataLevels[index]; } set { DataLevels[index] = value; } }

    }
}