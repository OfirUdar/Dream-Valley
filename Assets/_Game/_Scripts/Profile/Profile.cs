using Udar;

namespace Game
{
    public class Profile
    {
        public ResourcesInventory ResourcesInventory = new ResourcesInventory();

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

}
