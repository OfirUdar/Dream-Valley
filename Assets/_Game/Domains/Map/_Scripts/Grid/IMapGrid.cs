using System;

namespace Game.Map
{
    public interface IMapGrid : IGrid<IMapElement>
    {
        public event Action<IMapElement> ElementChanged;
        public event Action<IMapElement> ElementRemoved;
    }
}

