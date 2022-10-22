using System;

namespace Game
{
    public class SelectionEventAggragator
    {
        public event Action<ISelectable> SelectRequested;
        public event Action<ISelectable> UnselectRequested;

        public void RequestSelect(ISelectable selectable)
        {
            SelectRequested?.Invoke(selectable);
        }
        public void Unselect(ISelectable selectable)
        {
            UnselectRequested?.Invoke(selectable);
        }
    }

}
