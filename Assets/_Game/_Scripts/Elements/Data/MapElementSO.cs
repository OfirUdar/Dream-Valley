using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "Building", menuName = "Game/Map/Elements/Building", order = 0)]
    public class MapElementSO : ScriptableObject
    {
        [field: SerializeField] public GameObject Pfb{ get; private set; }
        [field: SerializeField] public MapElementData Data { get; private set; }
    }
}