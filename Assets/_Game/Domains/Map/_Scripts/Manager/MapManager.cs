using Zenject;

namespace Game.Map
{
    public class MapManager : IInitializable, ILateDisposable
    {
        private readonly IMapSaver _mapSaver;
        private readonly IMapGrid _grid;

        public MapManager(IMapSaver mapSaver,IMapGrid grid)
        {
            _mapSaver = mapSaver;
            _grid = grid;
            _mapSaver.LoadAll();
        }

        public void Initialize()
        {

            _grid.ElementChanged += OnElementChanged;
            _grid.ElementRemoved += OnElementRemoved;
        }
        public void LateDispose()
        {
            _grid.ElementChanged -= OnElementChanged;
            _grid.ElementRemoved -= OnElementRemoved;
        }

        private void OnElementChanged(IMapElement element)
        {
            if (element == null)
                return;

            _mapSaver.SaveElement(element);
        }
        private void OnElementRemoved(IMapElement element)
        {
            _mapSaver.DeleteElement(element);
        }
       
    }

}
