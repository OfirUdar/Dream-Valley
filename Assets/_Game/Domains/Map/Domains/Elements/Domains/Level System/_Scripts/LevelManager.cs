﻿using Udar;
using UnityEngine;
using Zenject;

namespace Game.Map.Element.LevelSystem
{
    public class LevelManager : ILevelManager, ILoadable, ISaveable, IInitializable
    {
        public string Path => SaveLoadKeys.GetElementLevelPath(_mapElement.Data.GUID, _mapElement.SaveData.InstanceGUID);
        public int CurrentLevel => _levelSaveData.CurrentLevel;

        private LevelSaveData _levelSaveData = new LevelSaveData();

        private readonly IMapElement _mapElement;
        private readonly ISaveManager _saveManager;
        private readonly ILoadManager _loadManager;
        private readonly ILevelsElementVisualHandler _elementVisualHandler;

        public LevelManager(IMapElement mapElement,
            ISaveManager saveManager,
            ILoadManager loadManager,
            ILevelsElementVisualHandler elementVisualHandler)
        {
            _mapElement = mapElement;
            _saveManager = saveManager;
            _loadManager = loadManager;
            _elementVisualHandler = elementVisualHandler;
        }

        public void Initialize()
        {
            _loadManager.TryLoad(this);
        }

        public string GetSerialized()
        {
            return JsonUtility.ToJson(_levelSaveData);
        }

        public void SetSerialized(string data)
        {
            var levelSaveData = JsonUtility.FromJson<LevelSaveData>(data);
            _levelSaveData = levelSaveData;

            _elementVisualHandler.Refresh(CurrentLevel);
        }

        public void UpgradgeLevelUp()
        {
            _levelSaveData.CurrentLevel++;
            _elementVisualHandler.Refresh(CurrentLevel);

            _saveManager.Save(this);
        }

    }
}