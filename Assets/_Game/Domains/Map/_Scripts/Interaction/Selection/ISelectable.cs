namespace Game.Map
{
    public interface ISelectable
    {
        public bool IsSelected { get; }
        public bool Select();
        public void Unselect();

    }


}
