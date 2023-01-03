using System.Collections.Generic;

namespace Game.Resources
{
    public class ResourceCapacity
    {
        private int _capacity;
        private bool _isDirty;
        public int Capacity
        {
            get
            {
                if (_isDirty)
                    _capacity = CalculateTotalCapacity();
                return _capacity;
            }
        }

        private readonly List<IResourceCapacityContainer> _capacityList = new List<IResourceCapacityContainer>();

        private int CalculateTotalCapacity()
        {
            var amount = 0;
            for (int i = 0; i < _capacityList.Count; i++)
            {
                amount += _capacityList[i].GetCapacity();
            }
            _isDirty = false;
            return amount;
        }

        public void AddCapacity(IResourceCapacityContainer resourceCapacity)
        {
            _capacityList.Add(resourceCapacity);
            _isDirty = true;
        }
        public void MarkDirty()
        {
            _isDirty = true;
        }

    }
}
