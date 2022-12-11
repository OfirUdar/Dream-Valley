namespace Game.Map
{
    public interface IMapSaver
    {
        public void LoadAll();
        public void SaveElement(IMapElement element);
        public void DeleteElement(IMapElement element);
    }
}
