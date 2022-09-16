using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "Building", menuName = "Buildings/Building", order = 0)]
    public class BuildingSO : ScriptableObject
    {
        [field: SerializeField] public GameObject Pfb{ get; private set; }
        [field: SerializeField] public BuildData BuildData { get; private set; }
    }
}