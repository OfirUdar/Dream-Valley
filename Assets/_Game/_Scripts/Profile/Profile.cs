using System;
using Udar;

namespace Game
{
    public class Profile  
    {
        public ResourcesInventory ResourcesInventory = new ResourcesInventory();

        //Level
        //Current XP

        public Profile(ILoadManager loadManager)
        {
            loadManager.TryLoad(ResourcesInventory);
        }

    }

    [Serializable]
    public class Level
    {
        public int Number;
        public float XPCapacity;
        // public float CurrentXP;
    }

}
