using UnityEngine;

namespace Game
{
    public interface IElementSpawner
    {
        public void Spawn(GameObject gameObject, bool isNew = true);
    }
}