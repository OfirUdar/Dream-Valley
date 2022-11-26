namespace Game.Map.Element
{
    public class Selector : ISelectable
    {
        private readonly ISelectVisual _selectVisual;

        public bool IsSelected { get; private set; }

        public Selector(ISelectVisual selectVisual)
        {
            _selectVisual = selectVisual;
        }
        public bool Select()
        {
            IsSelected = true;
            _selectVisual.Select();

            return true;
        }

        public void Unselect()
        {
            IsSelected = false;
            _selectVisual.Unselect();
        }


    }
}