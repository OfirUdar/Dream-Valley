using System;
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



        public void LoadAll()
        {
            var elementTypesPathList = _loadManager.LoadFoldersPaths(SaveLoadKeys.Map);
            if (elementTypesPathList.Length > 0)
                IterateOnElementTypes(elementTypesPathList);
            else
                LoadDefaultMap();
        }

        private void LoadDefaultMap()
        {
            foreach (var element in _initElementList.ElementsList)
            {
                var mapElementInstance = _elementSpawner.Spawn(element.Pfb);

                mapElementInstance.Position = element.DefaultPosition;

                _grid.Place(mapElementInstance);

                SaveElement(mapElementInstance);
            }
        }

        private void IterateOnElementTypes(string[] elementTypesPathList)
        {
            foreach (var typePath in elementTypesPathList)
            {
                string elementTypeGUID = new DirectoryInfo(Path.GetFileName(typePath)).Name;
                var finalTypePath = Path.Combine(SaveLoadKeys.Map, elementTypeGUID);

                IterateOnInstances(elementTypeGUID, finalTypePath);
            }
        }
        private void IterateOnInstances(string elementTypeGUID, string finalTypePath)
        {
            var elementInstancesPathList = _loadManager.LoadFoldersPaths(finalTypePath);

            foreach (var instancePath in elementInstancesPathList)
            {
                var data = File.ReadAllText(Path.Combine(instancePath, "data"));
                var elementData = JsonUtility.FromJson<MapElementSaveData>(data);

                CreateInstance(elementTypeGUID, elementData);
            }
        }
        private void CreateInstance(string elementTypeGUID, MapElementSaveData elementData)
        {
            try
            {
                var mapElementDataPrefab = _elementList.GetByGUID(elementTypeGUID).Pfb;
                var mapElementInstance = _elementSpawner.Spawn(mapElementDataPrefab);

                mapElementInstance.SaveData.InstanceGUID = elementData.InstanceGUID;
                mapElementInstance.Position = elementData.Position;

                _grid.Place(mapElementInstance);
            }
            catch (Exception exe)
            {
                Debug.LogException(exe);
            }

        }


    }

}
