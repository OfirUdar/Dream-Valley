using System;

namespace Game
{
    public class ElementSpawnerAggragator
    {
        public event Action<MapElementSO, Action, Action> SpawnNewRequested;
        public void SpawnNewAndPlace(MapElementSO mapElementSO, Action cancelCallback, Action successCallback)
        {
            SpawnNewRequested?.Invoke(mapElementSO, cancelCallback, successCallback);
        }
    }

}