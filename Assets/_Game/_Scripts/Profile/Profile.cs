using System;
using Udar;

namespace Game
{
    public class Profile
    {
        public ResourcesInventory ResourcesInventory = new ResourcesInventory();

        //Level
        //Current XP

        public Profile(ILoadManager loadManager, InitResourceDataListSO initResourceList)
        {
            bool hasLoaded = loadManager.TryLoad(ResourcesInventory);
            
            if(!hasLoaded)
            {
                foreach (var resourceInit in initResourceList.Resources)
                {
                    ResourcesInventory.AddResource(resourceInit.ResourceDataSO, resourceInit.Amount);
                }
            }
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
