using System;
using UnityEngine;

namespace Game.Map
{
    public interface IElementSpawner
    {
        public event Action<IMapElement> NewSpawned;
        public IMapElement Spawn(GameObject gameObject);
        public IMapElement SpawnDefault(GameObject gameObject);
    }

}