using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "Building", menuName = "Placements/Building", order = 0)]
    public class BuildingSO : ScriptableObject
    {
        [field: SerializeField] public GameObject Pfb{ get; private set; }
        [field: SerializeField] public PlacementData Data { get; private set; }
    }
}