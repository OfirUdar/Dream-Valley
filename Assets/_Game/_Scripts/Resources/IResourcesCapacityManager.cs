using System;

namespace Game
{
    public interface IResourcesCapacityManager
    {
        public event Action<string, int> Changed; // int -> the capacity

        public int GetCapacity(string resourceGuid);
        /// <summary>
        /// Adding to spesific resource
        /// </summary>
        /// <param name="resourceGuid"></param>
        /// <param name="resourceCapacity"></param>
        public void AddCapacityContainer(string resourceGuid, IResourceCapacityContainer resourceCapacity);
        /// <summary>
        /// Adding to all the resources capacity
        /// </summary>
        /// <param name="resourceCapacity"></param>
        public void AddCapacityContainer(IResourceCapacityContainer resourceCapacity);
        public void MarkDirty(string resourceGuid);
        public void MarkDirty();
    }

    public interface IResourceCapacityContainer
    {
        public int GetCapacity();
    }
}
