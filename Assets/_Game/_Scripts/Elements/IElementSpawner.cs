using System;
using UnityEngine;

namespace Game
{
    public interface IElementSpawner
    {
        public void Spawn(GameObject gameObject);
        public void SpawnNewAndPlace(GameObject gameObject,Action cancelCallback,Action successCallback);
    }
}