using System;
using System.Collections.Generic;
using Udar;
using UnityEngine;
using Zenject;

namespace Game.Map
{
    public class MapManager : IInitializable, ILateDisposable, ISaveable, ILoadable
    {
        public string Path => SaveLoadKeys.MapElements;

        private readonly IMapGrid _grid;
        private readonly IElementSpawner _elementSpawner;
        private readonly ElementsListSO _elementsList;


        private readonly ISaveManager _saveManager;
        private readonly ILoadManager _loadManager;


        private readonly List<IMapElement> _mapElements = new List<IMapElement>();

        public MapManager(IMapGrid grid,
            IElementSpawner elementSpawner,
            ElementsListSO elementsListSO,
            ISaveManager saveManager,
            ILoadManager loadManager)
        {
            _grid = grid;
            _elementSpawner = elementSpawner;
            _elementsList = elementsListSO;
            _saveManager = saveManager;
            _loadManager = loadManager;
        }

        public void Initialize()
        {
            _loadManager.TryLoad(this);
            _elementSpawner.NewSpawned += OnNewElementSpawned;
            _grid.ElementChanged += OnElementChanged;
            _grid.ElementRemoved += OnElementRemoved;
        }
   

        public void LateDispose()
        {
            _elementSpawner.NewSpawned -= OnNewElementSpawned;
            _grid.ElementChanged -= OnElementChanged;
            _grid.ElementRemoved -= OnElementRemoved;
        }

        public string GetSerialized()
        {
            var elementsData = new List<ElementSaveData>();

            foreach (var element in _mapElements)
            {

                if (element is MapElement mapElement)
                {
                    var elementData = new ElementSaveData()
                    {
                        GUID = mapElement.Data.GUID,
                        GridPosition = _grid.GetIndexes(mapElement.Position),
                    };
                    elementsData.Add(elementData);

                }

            }

            var mapSaveData = new MapSaveData()
            {
                ElementsList = elementsData,
            };

            return JsonUtility.ToJson(mapSaveData);
        }

        public void SetSerialized(string data)
        {
            var mapSaveData = JsonUtility.FromJson<MapSaveData>(data);

            foreach (var elementData in mapSaveData.ElementsList)
            {
                var mapElementDataPrefab = _elementsList.GetByGUID(elementData.GUID).Pfb;
                var mapElemenetInstance = _elementSpawner.Spawn(mapElementDataPrefab);

                var indexes = elementData.GridPosition;

                mapElemenetInstance.Position = _grid.GetWorldPosition(indexes.x, indexes.y);
                _grid.SetValue(indexes.x, indexes.y, mapElemenetInstance);

                _mapElements.Add(mapElemenetInstance);
            }

        }


        private void OnNewElementSpawned(IMapElement mapElement)
        {
            _mapElements.Add(mapElement);

            _saveManager.Save(this);
        }

        private void OnElementChanged(IMapElement element)
        {
            _saveManager.Save(this);
        }
        private void OnElementRemoved(IMapElement element)
        {
            _mapElements.Remove(element);

            _saveManager.Save(this);

        }



        [Serializable]
        public struct ElementSaveData
        {
            public string GUID;
            public Vector2Int GridPosition;
        }
        [Serializable]
        public class MapSaveData
        {
            public List<ElementSaveData> ElementsList = new List<ElementSaveData>();
        }
    }

}
