using System;
using System.Collections.Generic;
using System.IO;
using Udar;
using UnityEngine;
using Zenject;

namespace Game.Map
{

    public class MapSaver : IMapSaver
    {
        private readonly IMapGrid _grid;
        private readonly IElementSpawner _elementSpawner;
        private readonly ILoadManager _loadManager;


        [Inject(Id = "Initalize")] private readonly ElementsListSO _initElementList;
        [Inject] private readonly ElementsListSO _elementList;


        public MapSaver(IMapGrid grid,
            IElementSpawner elementSpawner,
            ILoadManager loadManager)
        {
            _grid = grid;
            _elementSpawner = elementSpawner;
            _loadManager = loadManager;
        }


        public void SaveElement(IMapElement element)
        {
            var elementPath = Path.Combine(Application.persistentDataPath,
                SaveLoadKeys.GetElementPath(element.Data.GUID,
                element.SaveData.InstanceGUID));

            var finalElementPath = Path.Combine(elementPath, "data");
            if (!Directory.Exists(elementPath))
                Directory.CreateDirectory(elementPath);

            element.SaveData.Position = element.Position;
            File.WriteAllText(finalElementPath, JsonUtility.ToJson(element.SaveData));
        }

        public void DeleteElement(IMapElement element)
        {
            var elementPath = Path.Combine(Application.persistentDataPath,
                SaveLoadKeys.GetElementPath(element.Data.GUID,
                element.SaveData.InstanceGUID));

            if (Directory.Exists(elementPath))
                Directory.Delete(elementPath);
        }



        public Dictionary<MapElementSO, int> LoadAll()
        {
            var allMapElements = new Dictionary<MapElementSO, int>();
            var elementTypesPathList = _loadManager.LoadFoldersPaths(SaveLoadKeys.Map);
            if (elementTypesPathList.Length > 0)
                IterateOnElementTypes(elementTypesPathList, allMapElements);
            else
                LoadDefaultMap(allMapElements);

            return allMapElements;
        }

        private void LoadDefaultMap(Dictionary<MapElementSO, int> allMapElements)
        {
            foreach (var element in _initElementList.ElementsList)
            {
                var mapElementInstance = _elementSpawner.SpawnDefault(element.Pfb);

                mapElementInstance.Position = element.DefaultPosition;

                _grid.Place(mapElementInstance);

                SaveElement(mapElementInstance);

                if (allMapElements.ContainsKey(element))
                    allMapElements[element]++;
                else
                    allMapElements.Add(element, 1);
            }
        }

        private void IterateOnElementTypes(string[] elementTypesPathList, Dictionary<MapElementSO, int> allMapElements)
        {
            foreach (var typePath in elementTypesPathList)
            {
                string elementTypeGUID = new DirectoryInfo(Path.GetFileName(typePath)).Name;
                var finalTypePath = Path.Combine(SaveLoadKeys.Map, elementTypeGUID);

                IterateOnInstances(elementTypeGUID, finalTypePath, allMapElements);
            }
        }
        private void IterateOnInstances(string elementTypeGUID, string finalTypePath, Dictionary<MapElementSO, int> allMapElements)
        {
            var elementInstancesPathList = _loadManager.LoadFoldersPaths(finalTypePath);
            var elementData = _elementList.GetByGUID(elementTypeGUID);

            if (elementInstancesPathList.Length > 0)
                allMapElements.Add(elementData, 0);

            foreach (var instancePath in elementInstancesPathList)
            {
                var data = File.ReadAllText(Path.Combine(instancePath, "data"));
                var elementSaveData = JsonUtility.FromJson<MapElementSaveData>(data);

                CreateInstance(elementTypeGUID, elementSaveData);

                allMapElements[elementData]++;
            }
        }
        private IMapElement CreateInstance(string elementTypeGUID, MapElementSaveData elementData)
        {
            try
            {
                var mapElementDataPrefab = _elementList.GetByGUID(elementTypeGUID).Pfb;
                var mapElementInstance = _elementSpawner.Spawn(mapElementDataPrefab);

                mapElementInstance.SaveData.InstanceGUID = elementData.InstanceGUID;
                mapElementInstance.Position = elementData.Position;

                _grid.Place(mapElementInstance);

                return mapElementInstance;
            }
            catch (Exception exe)
            {
                Debug.LogException(exe);
                return null;
            }

        }


    }

}
