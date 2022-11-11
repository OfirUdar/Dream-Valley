namespace Game.Map
{
    public interface ISelectable
    {
        public bool IsSelected { get; }
        public void Select();
        public void Unselect();
    }


}
