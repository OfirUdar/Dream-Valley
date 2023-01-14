using System.Collections.Generic;
using Zenject;

namespace Game.Map
{
    public class MapManager : IMapManager, IInitializable, ILateDisposable
    {
        private readonly IMapSaver _mapSaver;
        private readonly IMapGrid _grid;
        private readonly IElementSpawner _elementSpawner;


        private Dictionary<MapElementSO, int> _mapElementsDictionary
            = new Dictionary<MapElementSO, int>();

        public MapManager(IMapSaver mapSaver, IMapGrid grid, IElementSpawner elementSpawner)
        {
            _mapSaver = mapSaver;
            _grid = grid;
            _elementSpawner = elementSpawner;
            _mapElementsDictionary = _mapSaver.LoadAll();
        }

        public int GetElementInstancesAmount(MapElementSO mapElementData)
        {
            if (_mapElementsDictionary.ContainsKey(mapElementData))
                return _mapElementsDictionary[mapElementData];
            return 0;
        }

        public void Initialize()
        {
            _grid.ElementChanged += OnElementChanged;
            _grid.ElementRemoved += OnElementRemoved;
            _elementSpawner.NewSpawned += OnElementSpawned;
        }


        public void LateDispose()
        {
            _grid.ElementChanged -= OnElementChanged;
            _grid.ElementRemoved -= OnElementRemoved;
            _elementSpawner.NewSpawned -= OnElementSpawned;
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

        private void OnElementSpawned(IMapElement element)
        {
            if (_mapElementsDictionary.ContainsKey(element.Data))
                _mapElementsDictionary[element.Data]++;
            else
                _mapElementsDictionary.Add(element.Data, 1);
        }
    }

    public interface IMapManager
    {
        /// <summary>
        /// Returns the amount of elements instances from the same type
        /// </summary>
        /// <returns></returns>
        public int GetElementInstancesAmount(MapElementSO mapElementData);
    }

}
