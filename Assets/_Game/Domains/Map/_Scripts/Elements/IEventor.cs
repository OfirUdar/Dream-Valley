using System;

namespace Game.Map
{
    public interface IEventor 
    {
        public event Action SpawnedSuccessfully;
        public event Action UpgradeRequested;

        public void NotifiySpawnedSuccessfully();
        public void RequestUpgrade();

    }
}