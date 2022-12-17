using System;

namespace Game.Map.Element
{
    public class Eventor : IEventor
    {
        public event Action SpawnedSuccessfully;
        public event Action UpgradeRequested;

        public void NotifiySpawnedSuccessfully()
        {
            SpawnedSuccessfully?.Invoke();
        }

        public void RequestUpgrade()
        {
            UpgradeRequested?.Invoke();
        }
    }
}