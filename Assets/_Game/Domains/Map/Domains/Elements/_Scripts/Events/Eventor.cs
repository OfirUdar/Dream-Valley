using System;

namespace Game.Map.Element
{
    public class Eventor
    {
        public event Action SpawnedSuccessfully;

        public void NotifiySpawnedSuccessfully()
        {
            SpawnedSuccessfully?.Invoke();
        }
    }
}