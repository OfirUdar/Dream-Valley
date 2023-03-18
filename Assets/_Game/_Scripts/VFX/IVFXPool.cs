using UnityEngine;

namespace Game
{
    public interface IVFXPool
    {
        public GameObject Spawn(GameObject prefab, Vector3 position);
    }
}