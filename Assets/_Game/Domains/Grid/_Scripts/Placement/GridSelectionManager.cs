namespace Game
{
    public class GridSelectionManager: ISelectionManager
    {
        private ISelectable _currentSelected;

        public bool TrySelect(ISelectable selectable)
        {
            _currentSelected?.Unselect();
            _currentSelected = selectable;
            _currentSelected.Select();

            return true;
        }

        public void Unselect(ISelectable selectable)
        {
            selectable.Unselect();
            _currentSelected = null;
        }
    }
    public interface ISelectionManager
    {
        public bool TrySelect(ISelectable selectable);
        public void Unselect(ISelectable selectable);
    }
}