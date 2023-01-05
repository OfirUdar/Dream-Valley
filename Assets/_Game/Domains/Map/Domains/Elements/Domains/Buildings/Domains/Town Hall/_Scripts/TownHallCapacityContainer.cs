using Zenject;

namespace Game.Map.Element.Building.TownHall
{
    public class TownHallCapacityContainer : IResourceCapacityContainer, IInitializable, ILateDisposable
    {
        [Inject] private readonly ILevelManager _levelManager;
        [Inject] private readonly IResourcesCapacityManager _resourcesCapacityManager;
        [Inject] private readonly TownHallLevelsData _townHallData;

        public int GetCapacity()
        {
            var currentIndexLevel = _levelManager.CurrentIndexLevel;
            if (currentIndexLevel < 0)
                currentIndexLevel = 0;
            var townHallDataCurrentLevel = _townHallData.DataLevels[currentIndexLevel];
            return townHallDataCurrentLevel.StorageCapacity;
        }

        public void Initialize()
        {
            _resourcesCapacityManager.AddCapacityContainer(this);
            _levelManager.LevelUp += OnLevelUp;
        }
        public void LateDispose()
        {
            _levelManager.LevelUp -= OnLevelUp;
        }

        private void OnLevelUp()
        {
            _resourcesCapacityManager.MarkDirty();
        }



    }
}

