using System;

namespace Game.Map
{
    public interface ISelectionManager
    {
        public event Action<ISelectable> SelectionChanged;

        public void RequestSelect(ISelectable selectable);
        public void RequestUnselect();
        public void Lock(bool isLock);

    }

}