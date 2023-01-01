using Udar;
using UnityEngine;
using Zenject;

namespace Game.Map.Element.LevelSystem
{
    public class LevelManager : ILevelManager, ILoadable, ISaveable, IInitializable
    {
        public int CurrentIndexLevel => _levelSaveData.CurrentLevel;
        public Level CurrentLevel => _levels[CurrentIndexLevel];
        public Level NextLevel => _levels[CurrentIndexLevel + 1];


        private readonly IMapElement _mapElement;
        private readonly LevelsListSO _levels;
        private readonly ISaveManager _saveManager;
        private readonly ILoadManager _loadManager;
        private readonly ILevelsElementVisualHandler _elementVisualHandler;


        private LevelSaveData _levelSaveData = new LevelSaveData();

        public LevelManager(IMapElement mapElement,
            LevelsListSO levels,
            ISaveManager saveManager,
            ILoadManager loadManager,
            ILevelsElementVisualHandler elementVisualHandler)
        {
            _mapElement = mapElement;
            _levels = levels;
            _saveManager = saveManager;
            _loadManager = loadManager;
            _elementVisualHandler = elementVisualHandler;
        }

        public void Initialize()
        {
            _loadManager.TryLoad(this);
        }


        public void UpgradgeLevelUp()
        {
            _levelSaveData.CurrentLevel++;
            _elementVisualHandler.Refresh(CurrentLevel);

            _saveManager.Save(this);
        }

        public Level GetLevel(int index)
        {
            return _levels[index];
        }


        #region Save&Load
        public string Path => SaveLoadKeys.GetElementLevelPath(_mapElement.Data.GUID, _mapElement.SaveData.InstanceGUID);

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

        #endregion
    }
}
