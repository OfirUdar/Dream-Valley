using System;

namespace Game
{
    public interface IResourcesCapacityManager
    {
        public event Action<ResourceDataSO, int> Changed; // int -> the capacity

        public int GetCapacity(ResourceDataSO resource);
        /// <summary>
        /// Adding to spesific resource
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="resourceCapacity"></param>
        public void AddCapacityContainer(ResourceDataSO resource, IResourceCapacityContainer resourceCapacity);
        /// <summary>
        /// Adding to all the resources capacity
        /// </summary>
        /// <param name="resourceCapacity"></param>
        public void AddCapacityContainer(IResourceCapacityContainer resourceCapacity);
        public void MarkDirty(ResourceDataSO resource);
    }

    public interface IResourceCapacityContainer
    {
        public int GetCapacity();
    }
}
