namespace Game.Map
{
    public interface IMapElement : IPlaceable, ISelectable, IDraggable
    {
        public void Destroy();
    }


}