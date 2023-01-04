using Zenject;

namespace Game.Map.Element.Building.TownHall
{
    public class TownHallCapacityContainer : IResourceCapacityContainer,IInitializable,ILateDisposable
    {
        [Inject] private readonly ILevelManager _levelManager;
        [Inject] private readonly IResourcesCapacityManager _resourcesCapacityManager;
        [Inject] private readonly TownHallLevelsData _townHallData;

        public int GetCapacity()
        {
            var townHallDataCurrentLevel = _townHallData.DataLevels[_levelManager.CurrentIndexLevel];
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

