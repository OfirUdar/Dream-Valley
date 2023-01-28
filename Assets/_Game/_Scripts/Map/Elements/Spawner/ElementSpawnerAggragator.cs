using System;
using UnityEngine;

namespace Game
{
    public class ElementSpawnerAggragator
    {
        public event Action<GameObject, Action, Action> SpawnNewRequested;
        public void SpawnNewAndPlace(GameObject gameObject, Action cancelCallback, Action successCallback)
        {
            SpawnNewRequested?.Invoke(gameObject, cancelCallback, successCallback);
        }
    }

}