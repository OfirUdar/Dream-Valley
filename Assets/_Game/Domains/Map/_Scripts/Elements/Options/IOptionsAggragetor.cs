using System;

namespace Game.Map
{
    public interface IOptionsAggragetor
    {
        public event Action RefreshRequested;
        public void RequestRefresh();
    }
}