using System;

namespace Game.Map
{
    public class OptionsAggragetor : IOptionsAggragetor
    {
        public event Action RefreshRequested;

        public void RequestRefresh()
        {
            RefreshRequested?.Invoke();
        }
    }
}